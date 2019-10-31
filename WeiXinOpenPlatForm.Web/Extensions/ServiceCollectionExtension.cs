using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Service;

namespace WeiXinOpenPlatForm.Web.Extensions
{
    /// <summary>
    /// 提供<see cref="IServiceCollection"/> 扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注入应用服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region 依赖注入

            var baseType = typeof(IDenpendency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            var implementTypes = types.Where(x => x.IsClass).ToArray();
            var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                    services.AddTransient(interfaceType, implementType);
            }

            #endregion
            return services;
        }
    }
}
