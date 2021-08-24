using System.Collections.Generic;
using System.IO;
using sharprun.Models;

namespace sharprun
{
    public static class AppEntryLoader
    {
        private const string APP_ENTRY_DIR = "/usr/share/applications";
        
        public static List<AppEntry> LoadEntriesFromDisk()
        {
            var appEntries = new List<AppEntry>();
            var foundEntryPaths = Directory.GetFiles(APP_ENTRY_DIR, "*.desktop");

            return appEntries;
        }

        private static AppEntry LoadEntryFromDisk(string filePath)
        {
            
            return new AppEntry() { };
        }
    }
}