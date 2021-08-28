using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace sharprun.Views
{
    public class AppEntryView : UserControl
    {
        public AppEntryView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}