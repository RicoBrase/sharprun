using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace sharprun.Models
{
    public class AppEntry
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public string Exec { get; set; }
        public bool Hidden { get; set; }

        public void Run()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "/bin/sh",
                ArgumentList = { "-c", Exec },
                UseShellExecute = true
            });
        }

        public Stream? LoadIconBitmap()
        {
            string? firstAvailableIconPath = null;

            if (File.Exists(IconName)) return File.OpenRead(IconName);
            
            foreach (var dir in IconSearchPaths)
            {
                foreach (var fileExt in IconSearchExtensions)
                {
                    var possibleFilePath = $"{dir}/{IconName}.{fileExt}";
                    if(!File.Exists(possibleFilePath)) continue;
                    firstAvailableIconPath = possibleFilePath;
                    break;
                }
                
                if(!string.IsNullOrWhiteSpace(firstAvailableIconPath)) break;
            }

            return !string.IsNullOrWhiteSpace(firstAvailableIconPath) ? File.OpenRead(firstAvailableIconPath) : null;
        }

        private static IEnumerable<string> IconSearchPaths
        {
            get
            {
                // "/usr/share/icons/hicolor/24x24/apps"
                const string hicolorPath = "/usr/share/icons/hicolor";
                var hicolorDirectories = Directory.GetDirectories(hicolorPath);
                var paths = new List<string> { "/usr/share/pixmaps" };

                hicolorDirectories.Select(path => $"{path}/apps").ToList().ForEach(paths.Add);
                
                return paths;
            }
        }

        private static readonly string[] IconSearchExtensions = { "png" };
    }
}