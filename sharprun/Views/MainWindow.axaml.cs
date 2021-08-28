using System;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using sharprun.ViewModels;

namespace sharprun.Views
{
    public partial class MainWindow : Window
    {

        private TextBox? _searchInput;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _searchInput = this.FindControl<TextBox>("SearchInput");
        }

        private void Window_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (DataContext is not MainWindowViewModel viewModel) return;
            switch (e.Key)
            {
                case Key.Escape:
                    MainWindowViewModel.CloseApplication();
                    break;
                case Key.Down:
                    viewModel.SelectNext();
                    break;
                case Key.Up:
                    viewModel.SelectPrevious();
                    break;
                case Key.Enter:
                    viewModel.RunSelectedApp();
                    break;
            }
        }

        private void Window_OnOpened(object? sender, EventArgs e)
        {
            _searchInput?.Focus();
            if (DataContext is MainWindowViewModel viewModel)
            {
                AppEntryLoader.LoadEntriesFromDisk().ToList().ForEach(x => viewModel.AppEntries.Add(new AppEntryViewModel(x)));
                viewModel.Search = "";
            }
        }
    }
}