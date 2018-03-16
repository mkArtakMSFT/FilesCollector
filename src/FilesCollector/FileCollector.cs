using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PackageCollector
{
    internal class FileCollector
    {
        public async Task CollectFilesAsync(IEnumerable<string> files, string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            List<Task> copyTasks = new List<Task>();
            foreach (string file in files)
            {
                copyTasks.Add(
                    Task.Run(
                        () => File.Copy(file, Path.Combine(outputDirectory, Path.GetFileName(file)))));
            }

            await Task.WhenAll(copyTasks);
        }
    }
}
