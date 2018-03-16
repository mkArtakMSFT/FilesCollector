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

        public IEnumerable<string> LanguageFolders { get; set; }

        public IEnumerable<string> PackageNames { get; set; }

        public IEnumerable<string> VersionSuffixes { get; set; }

        public Task<IDictionary<string, string>> BuildAsync()
        {
            string packagesRoot = GetValidatedPackagesRoot();
            return Task<IDictionary<string, string>>.Run(() =>
            {
                IDictionary<string, string> packageMap = new ConcurrentDictionary<string, string>();
                foreach (string languageFolder in this.LanguageFolders.Select(item => Path.Combine(this.PackagesRoot, item)))
                {
                    foreach (string rawPackageName in this.PackageNames)
                    {
                        string[] packageFileNames = VersionSuffixes.Select(item => $"{rawPackageName}.*{item}.nupkg").ToArray();
                        foreach (var packageName in packageFileNames)
                        {
                            IEnumerable<string> matchingPackages = Directory.EnumerateFiles(languageFolder, packageName, SearchOption.TopDirectoryOnly);
                            string matchingPackage = matchingPackages.OrderBy(item => item.Length).FirstOrDefault();
                            if (matchingPackage != null)
                            {
                                packageMap[Path.GetFileName(matchingPackage)] = matchingPackage;
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
