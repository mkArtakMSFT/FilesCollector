using System;
using System.Collections.Generic;
using System.Text;

namespace PackageCollector.Configuration
{
    public class AppSettings
    {
        public string[] PackageNames { get; set; }

        public string[] RelevantCultures { get; set; }

        public string SourcePackagesRoot { get; set; }

        public string OutputFolder { get; set; }

        public string[] VersionSuffixes { get; set; }
    }
}
