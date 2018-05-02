using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PackageCollector
{
    internal class PackageListBuilder
    {
        public string PackagesRoot { get; set; }

        public IEnumerable<string> RelevantCultures { get; set; }

        public IEnumerable<string> PackageNames { get; set; }

        public IEnumerable<string> VersionSuffixes { get; set; }

        public Task<IDictionary<string, string>> BuildAsync()
        {
            string packagesRoot = GetValidatedPackagesRoot();
            return Task<IDictionary<string, string>>.Run(() =>
            {
                IDictionary<string, string> packageMap = new ConcurrentDictionary<string, string>();
                foreach (string rawPackageName in this.PackageNames)
                {
                    foreach (string cultureInfo in this.RelevantCultures)
                    {
                        string code = cultureInfo.Split('/')[1];
                        if (code != string.Empty)
                        {
                            code = $".{code}";
                        }

                        string[] packageFileNames = VersionSuffixes.Select(v => $"{rawPackageName}{code}.{v}.nupkg").ToArray();
                        foreach (var packageName in packageFileNames)
                        {
                            string package = Path.Combine(this.PackagesRoot, packageName);
                            if (File.Exists(package))
                            {
                                packageMap[packageName] = package;
                            }
                        }
                    }
                }

                if (this.PackageNames.Any(expectedPackagePrefix => !packageMap.Keys.Any(item => item.StartsWith(expectedPackagePrefix))))
                {
                    System.Console.WriteLine("Not all packages were found");
                    throw new Exception("Not all packages were found");
                }

                return packageMap;
            });
        }

        private string GetValidatedPackagesRoot()
        {
            if (!Directory.Exists(this.PackagesRoot))
            {
                throw new DirectoryNotFoundException($"Directory {this.PackagesRoot} doesn't exist");
            }

            return this.PackagesRoot;
        }
    }
}
