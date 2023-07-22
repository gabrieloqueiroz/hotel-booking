using System.Diagnostics;
using System.Reflection;

namespace API.Configuration
{
    public static class ApiVersion
    {
        public static string Get()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.ProductVersion;
        }
    }
}
