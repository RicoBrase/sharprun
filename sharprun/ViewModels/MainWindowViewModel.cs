using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace sharprun.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _command = "";
        private int _selectedApp;
        private int _selectedIndex = -1;
        private bool _selectionChanged;

        public string Command
        {
            get => _command;
            set => this.RaiseAndSetIfChanged(ref _command, value);
        }

        public int SelectedApp
        {
            get => _selectedApp;
            set
            {
                Command = Convert.ToString(value);
                this.RaiseAndSetIfChanged(ref _selectedApp, value);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        public List<int> Apps => Enumerable.Range(1, 10).ToList();

        public void CloseApplication()
        {
            Environment.Exit(0);
        }

        public void SelectNext()
        {
            if (!_selectionChanged)
            {
                _selectionChanged = true;
            }
            SelectedIndex = (SelectedIndex + 1) % Apps.Count;
        }

        public void SelectPrevious()
        {
            if (!_selectionChanged)
            {
                SelectedIndex = 0;
                _selectionChanged = true;
            }
            SelectedIndex = (SelectedIndex - 1 + Apps.Count) % Apps.Count;
        }
    }
}