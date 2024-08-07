using System.Reflection;
using System.Runtime.Loader;
using System.Xml.Linq;

///----------------------------------
///<copyright>FanClass</copyright>
///<author>zhuangjl</author>
///<email>3046524346@qq.com</email>
///<log date="2024-08-06">创建</log>
///---------------------------------
namespace FanClass.Services.Tools
{
    /// <summary>
    /// 反射工具类
    /// </summary>
    public static class AssemblyTool
    {
        /// <summary>
        /// 动态返回匹配func的所有程序集
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<Assembly> Load(Func<string, bool> func)
        {
            var assemblies = new List<Assembly>();
            var libraries = DependencyContext.Default!.RuntimeLibraries;
            var runtimeLibs = libraries.Where(l => func(l.Name));
            foreach (var lib in runtimeLibs)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                assemblies.Add(assembly);
            }
            return assemblies;
        }
    }
}