using System.Diagnostics;

namespace sharprun.Models
{
    public class AppEntry
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
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
    }
}