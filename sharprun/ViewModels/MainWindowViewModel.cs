using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using sharprun.Models;

namespace sharprun.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _search;
        private int _selectedIndex;
        private AppEntryViewModel? _selectedAppEntry;
        
        public ObservableCollection<AppEntryViewModel> AppEntries { get; } = new();
        public ObservableCollection<AppEntryViewModel> SearchResults { get; } = new();

        public string? Search
        {
            get => _search;
            set => this.RaiseAndSetIfChanged(ref _search, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        public AppEntryViewModel? SelectedAppEntry
        {
            get => _selectedAppEntry;
            set => this.RaiseAndSetIfChanged(ref _selectedAppEntry, value);
        }

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.Search)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoSearch!);
        }
        
        private void DoSearch(string searchQuery)
        {
            SearchResults.Clear();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                AppEntries.ToList().ForEach(SearchResults.Add);   
            }
            else
            {
                AppEntries.Where(entry =>
                        entry.Name.ToLower().StartsWith(searchQuery.ToLower())
                        || entry.FileName.ToLower().StartsWith(searchQuery.ToLower()))
                    .ToList()
                    .ForEach(SearchResults.Add);
            }
        }

        public async void LoadIcons()
        {
            foreach (var appEntry in AppEntries)
            {
                await appEntry.LoadIcon();
            }
        }

        public static void CloseApplication()
        {
            Environment.Exit(0);
        }

        public void SelectNext()
        {
            SelectedIndex = (SelectedIndex + 1) % SearchResults.Count;
        }

        public void SelectPrevious()
        {
            SelectedIndex = (SelectedIndex - 1 + SearchResults.Count) % SearchResults.Count;
        }

        public void RunSelectedApp()
        {
            SelectedAppEntry?.Run();
            CloseApplication();
        }
    }
}