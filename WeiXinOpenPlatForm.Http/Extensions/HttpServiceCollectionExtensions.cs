using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WeiXinOpenPlatForm.Http.Settings;

namespace WeiXinOpenPlatForm.Http.Extensions
{
    /// <summary>
    /// 提供 <see cref="IServiceCollection"/> 用于 <see cref="HttpClient"/> 的扩展方法
    /// </summary>
    public static class HttpServiceCollectionExtensions
    {
        /// <summary>
        /// 根据应用程序配置注册 <see cref="HttpClient"/>
        /// </summary>
        /// <param name="services">当前 <see cref="IServiceCollection"/></param>
        /// <param name="configuration">应用程序配置</param>
        /// <return>操作后的 <see cref="IServiceCollection"/></return>
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
             var settings = configuration.GetSection(nameof(HttpClientSettings))?.Get<HttpClientSettings>();
            if (settings == null)
            {
                return services;
            }

            // 注册 HTTP 处理程序 
            var handlers = AddDelegatingHandlers(services, settings);

            // 注册 HTTP 错误处理政策
            var registry = AddHttpErrorPolicies(services, settings);

            // 注册 HTTP 客户端
            AddHttpClients(services, settings, handlers, registry);

            return services;
        }

        private static Dictionary<string, Type> AddDelegatingHandlers(IServiceCollection services, HttpClientSettings section)
        {
            var handlers = new Dictionary<string, Type>();
            if (section.DelegatingHandlers?.Count > 0)
            {
                foreach (var handler in section.DelegatingHandlers.Where(e => !string.IsNullOrEmpty(e.Value)))
                {
                    var handlerType = Type.GetType(handler.Value);
                    if (handlerType != null && typeof(DelegatingHandler).IsAssignableFrom(handlerType))
                    {
                        services.AddTransient(handlerType);
                        handlers.Add(handler.Key, handlerType);
                    }
                }
            }
            return handlers;
        }

        private static IPolicyRegistry<string> AddHttpErrorPolicies(IServiceCollection services, HttpClientSettings section)
        {
            var registry = services.AddPolicyRegistry();
            if (section.RetryPolicies?.Count > 0)
            {
                foreach (var policy in section.RetryPolicies.Where(e => e.Value != null))
                {
                    if (!registry.ContainsKey(policy.Key))
                    {
                        registry.Add(policy.Key,
                            HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(policy.Value.RetryCount));
                    }
                }
            }
            if (section.WaitAndRetryPolicies?.Count > 0)
            {
                foreach (var policy in section.WaitAndRetryPolicies.Where(e => e.Value?.SleepDurations?.Length > 0))
                {
                    if (!registry.ContainsKey(policy.Key))
                    {
                        registry.Add(policy.Key,
                            HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(
                                policy.Value.SleepDurations.Select(e => TimeSpan.FromMilliseconds(e))));
                    }
                }
            }
            if (section.CircuitBreakerPolicies?.Count > 0)
            {
                foreach (var policy in section.CircuitBreakerPolicies.Where(e => e.Value != null))
                {
                    if (!registry.ContainsKey(policy.Key))
                    {
                        registry.Add(policy.Key,
                            HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(
                                policy.Value.HandledEventsAllowedBeforeBreaking,
                                TimeSpan.FromMilliseconds(policy.Value.DurationOfBreak)));
                    }
                }
            }
            return registry;
        }

        private static void AddHttpClients(IServiceCollection services,
            HttpClientSettings section, Dictionary<string, Type> handlers, IPolicyRegistry<string> registry)
        {
            if (string.IsNullOrEmpty(section.PriorityType) ||
                section.BaseAddresses == null || section.BaseAddresses.Count == 0 ||
                section.Settings == null || section.Settings.Count == 0)
            {
                return;
            }

            var priorityType = Type.GetType(section.PriorityType);
            if (priorityType == null)
            {
                return;
            }

            foreach (var setting in section.Settings.Where(e => //Enum.TryParse(priorityType, e.Priority, out var priority) &&
                section.BaseAddresses.ContainsKey(e.BaseAddress)))
            {
                // 注册 HTTP 客户
                var builder = services.AddHttpClient(setting.Priority,
                    c => { c.BaseAddress = new Uri(section.BaseAddresses[setting.BaseAddress]); });
                // 设置 HTTP 处理程序
                if (setting.DelegatingHandlers?.Count > 0)
                {
                    foreach (var handler in setting.DelegatingHandlers.Where(handlers.ContainsKey))
                    {
                        builder.AddHttpMessageHandler(provider =>
                            (DelegatingHandler)provider.GetRequiredService(handlers[handler]));
                    }
                }
                // 设置 HTTP 处理程序生存期
                if (setting.HandlerLifetime.HasValue)
                {
                    builder.SetHandlerLifetime(TimeSpan.FromMinutes(setting.HandlerLifetime.Value));
                }
                // 设置 HTTP 错误处理政策
                if (setting.ErrorPolicies?.Count > 0)
                {
                    foreach (var policy in setting.ErrorPolicies.Where(registry.ContainsKey))
                    {
                        builder.AddPolicyHandlerFromRegistry(policy);
                    }
                }
            }
        }
    }
}
