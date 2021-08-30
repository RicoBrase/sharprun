using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Remote.Protocol.Input;
using sharprun.Models;

namespace sharprun
{
    public static class AppEntryLoader
    {
        private const string APP_ENTRY_DIR = "/usr/share/applications";
        
        public static AppEntry[] LoadEntriesFromDisk()
        {
            var foundEntryPaths = Directory.GetFiles(APP_ENTRY_DIR, "*.desktop");

            //return foundEntryPaths.Select(path => new AppEntry { Name = Path.GetFileName(path) }).ToList();

            return foundEntryPaths.Select(LoadEntryFromDisk).Where(entry => !entry.Hidden).ToArray();
        }

        private static AppEntry LoadEntryFromDisk(string filePath)
        {
            var fileLines = File.ReadAllLines(filePath);
            var currentLineIndex = 0;
            var startLineIndex = 0;
            var endLineIndex = fileLines.Length-1;

            while (currentLineIndex < fileLines.Length)
            {
                var line = fileLines[currentLineIndex];
                if (line.Trim().StartsWith("[Desktop Entry]"))
                {
                    startLineIndex = currentLineIndex;
                }else if (line.Trim().StartsWith("["))
                {
                    if(endLineIndex == fileLines.Length - 1) endLineIndex = currentLineIndex;
                }
                currentLineIndex++;
            }

            var appEntry = new AppEntry { FileName = Path.GetFileName(filePath)};
            
            for (var i = startLineIndex; i <= endLineIndex; i++)
            {
                if (!fileLines[i].Contains("=")) continue;
                
                var lineSegments = fileLines[i].Split("=");
                var key = lineSegments[0];
                var val = string.Join("=", lineSegments.Skip(1).ToArray());

                switch (key)
                {
                    case "Name":
                        appEntry.Name = val;
                        break;
                    case "Exec":
                        appEntry.Exec = val;
                        break;
                    case "Icon":
                        appEntry.IconName = val;
                        break;
                    case "NoDisplay":
                        appEntry.Hidden = bool.Parse(val);
                        break;
                }
            }
            
            return appEntry;
        }
    }
}