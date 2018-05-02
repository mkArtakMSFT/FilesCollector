using Microsoft.Extensions.Configuration;
using PackageCollector.Configuration;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace PackageCollector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Algorithm
            /*
             * Iterate over all the files to identify those to be collected
             * Start copying those asynchronously to the output folder
             * 
             * As packages can be duplicated accross multiple repos, we're going to approach packages in priority order.
             * This will allow us to simply not consider a package, if we have already covered it as part of processing a folder with a higher priority.
             * */

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            AppSettings appSettings = new AppSettings();
            var section = configuration.GetSection("AppSettings");
            appSettings = section.Get<AppSettings>();

            PackageListBuilder packageListBuilder = new PackageListBuilder();
            packageListBuilder.RelevantCultures = appSettings.RelevantCultures;
            packageListBuilder.PackageNames = appSettings.PackageNames;
            packageListBuilder.VersionSuffixes = appSettings.VersionSuffixes;
            packageListBuilder.PackagesRoot = appSettings.SourcePackagesRoot;

            var packagesList = packageListBuilder.BuildAsync().GetAwaiter().GetResult();

            var filesCollector = new FileCollector();
            filesCollector.CollectFilesAsync(packagesList.Values, appSettings.OutputFolder).GetAwaiter().GetResult();
        }
    }
}
