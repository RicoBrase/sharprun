using Avalonia.Media.Imaging;
using ReactiveUI;
using sharprun.Models;

namespace sharprun.ViewModels
{
    public class AppEntryViewModel : ViewModelBase
    {

        private readonly AppEntry _appEntry;
        private Bitmap? _icon;

        public string Name => _appEntry.Name;
        public string FileName => _appEntry.FileName;

        public Bitmap? Icon
        {
            get => _icon;
            private set => this.RaiseAndSetIfChanged(ref _icon, value);
        }
        
        public AppEntryViewModel(AppEntry appEntry)
        {
            _appEntry = appEntry;
        }

        public AppEntryViewModel()
        {
            _appEntry = new AppEntry
            {
                FileName = "sample.desktop",
                Name = "Sample",
                Icon = "sample.png"
            };
        }

        public void Run()
        {
            _appEntry.Run();
        }
    }
}