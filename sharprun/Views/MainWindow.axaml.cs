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

        private TextBox? _commandInput;

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
            _commandInput = this.FindControl<TextBox>("CommandInput");
        }

        private void Window_OnKeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape when DataContext is not null:
                    ((MainWindowViewModel) DataContext).CloseApplication();
                    break;
                case Key.Down when DataContext is not null:
                    ((MainWindowViewModel) DataContext).SelectNext();
                    break;
                case Key.Up when DataContext is not null:
                    ((MainWindowViewModel) DataContext).SelectPrevious();
                    break;
            }
        }

        private void Window_OnOpened(object? sender, EventArgs e)
        {
            _commandInput?.Focus();
        }
    }
}