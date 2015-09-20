namespace Weikeren.Utility.PluginMvcWeb
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;
    using System.Xml.Linq;
    using Weikeren.Utility.PluginMvcWeb.Model;

    /// <summary>
    /// 插件加载器。
    /// </summary>
    public static class PluginLoader
    {
        /// <summary>
        /// 插件目录。
        /// </summary>
        private static readonly DirectoryInfo PluginFolder;

        /// <summary>
        /// 插件临时目录。
        /// </summary>
        private static readonly DirectoryInfo TempPluginFolder;

        /// <summary>
        /// 初始化。
        /// </summary>
        static PluginLoader()
        {
            PluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
            TempPluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/App_Data/Dependencies"));
        }

        /// <summary>
        /// 加载插件。
        /// </summary>
        public static IEnumerable<PluginDescriptor> Load()
        {
            List<PluginDescriptor> plugins = new List<PluginDescriptor>();

            ////程序集复制到临时目录。
            //FileCopyTo();

            //IEnumerable<Assembly> assemblies = null;

            //////加载 bin 目录下的所有程序集。
            ////assemblies = AppDomain.CurrentDomain.GetAssemblies();

            ////plugins.AddRange(GetAssemblies(assemblies));

            ////加载临时目录下的所有程序集。
            //assemblies = TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories).Select(x => Assembly.LoadFile(x.FullName));
            ////assemblies = TempPluginFolder.GetFiles("Weikeren.GroupBuy.Sell.dll", SearchOption.AllDirectories).Select(x => Assembly.LoadFile(x.FullName));

            //plugins.AddRange(GetAssemblies(assemblies));

            //return plugins;
            try
            {
                string pluginAssemblyMappingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PluginAssemblyMapping.xml");
                var pluginList = XElement.Load(pluginAssemblyMappingPath);

                var mappings = from m
                             in pluginList.Elements()
                               select new PluginAssemblyMapping()
                               {
                                   PluginPath = m.Element("PluginPath").Value,
                                   AssemblyName = m.Element("AssemblyName").Value,
                                   InimplementIPlugClassName = m.Element("ImplementIPlugClassName").Value
                               };
                var list = mappings.ToList();
                if (list != null && list.Count > 0)
                {
                    //var context = HttpContext.Current != null ? new HttpContextWrapper(HttpContext.Current) as HttpContextBase : (new FakeHttpContext("~/") as HttpContextBase);
                    foreach (var item in list)
                    {
                        //string path = context.Current.Server.MapPath(string.Format("{0}/{1}", item.PluginPath.Trim('/'), item.AssemblyName));
                        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, string.Format("{0}\\{1}", item.PluginPath.TrimStart('~').Trim('/').Trim('\\'), item.AssemblyName));

                        //var assembly = Assembly.LoadFile(path); //此方法不能释放dll，因为dll不能被覆盖
                        var assembly = loadAssembly(path);
                        var plugin = (IPlugin)assembly.CreateInstance(item.InimplementIPlugClassName);

                        if (plugin != null)
                        {
                            var desc = new PluginDescriptor(plugin, assembly, assembly.GetTypes());
                            plugins.Add(desc);
                        }


                    }
                }

                return plugins;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("加载PluginAssemblyMapping.xml失败。");
                return plugins;
            }
        }

        /// <summary>
        /// 此方法可释放DLL
        /// </summary>
        private static Assembly loadAssembly(string fileName)
        {
            MemoryStream memStream;
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                using (memStream = new MemoryStream())
                {
                    int res;
                    byte[] b = new byte[4096];
                    while ((res = stream.Read(b, 0, b.Length)) > 0)
                    {
                        memStream.Write(b, 0, b.Length);
                    }
                }
            }
            Assembly asm = Assembly.Load(memStream.ToArray());

            return asm;
        }

        /// <summary>
        /// 获得插件信息。
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static PluginDescriptor GetPluginInstance(Type pluginType, Assembly assembly)
        {
            try
            {
                if (pluginType != null)
                {
                    var plugin = (IPlugin)Activator.CreateInstance(pluginType);

                    if (plugin != null)
                    {
                        return new PluginDescriptor(plugin, assembly, assembly.GetTypes());
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
           
            return null;
        }

        /// <summary>
        /// 程序集复制到临时目录。
        /// </summary>
        private static void FileCopyTo()
        {
            Directory.CreateDirectory(PluginFolder.FullName);
            Directory.CreateDirectory(TempPluginFolder.FullName);

            //清理临时文件。
            foreach (var file in TempPluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {

                }

            }

            //复制插件进临时文件夹。
            foreach (var plugin in PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    var di = Directory.CreateDirectory(TempPluginFolder.FullName);
                    File.Copy(plugin.FullName, Path.Combine(di.FullName, plugin.Name), true);
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 根据程序集列表获得该列表下的所有插件信息。
        /// </summary>
        /// <param name="assemblies">程序集列表</param>
        /// <returns>插件信息集合。</returns>
        private static IEnumerable<PluginDescriptor> GetAssemblies(IEnumerable<Assembly> assemblies)
        {
            IList<PluginDescriptor> plugins = new List<PluginDescriptor>();

            foreach (var assembly in assemblies)
            {
                var pluginTypes = assembly.GetTypes().Where(type => type.GetInterface(typeof(IPlugin).Name) != null && type.IsClass && !type.IsAbstract);

                foreach (var pluginType in pluginTypes)
                {
                    var plugin = GetPluginInstance(pluginType, assembly);

                    if (plugin != null)
                    {
                        plugins.Add(plugin);
                    }
                }
            }

            return plugins;
        }
    }
}