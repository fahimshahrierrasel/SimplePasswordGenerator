using CommunityToolkit.Mvvm.ComponentModel;
using PasswordGenerator.Data;
using PasswordGenerator.Model;
using System.Collections.ObjectModel;

namespace PasswordGenerator.ViewModel
{
    public partial class HistoryViewModel : ObservableObject
    {
        private readonly HistoryDatabase _db;

        public HistoryViewModel(HistoryDatabase db)
        {
            _db = db;
        }

        [ObservableProperty]
        ObservableCollection<PasswordHistoryItem> histories = new();


        public async Task LoadHistoriesAsync()
        {
            var table = await _db.GetTableAsync<PasswordHistoryItem>();
            var histories = await table.OrderByDescending(t => t.CopiedAt).ToListAsync();
            if (histories is not null && histories.Any())
            {
                Histories = new ObservableCollection<PasswordHistoryItem>();
                foreach (var history in histories)
                {
                    Histories.Add(history);
                }
            }
        }

        public async Task InsertHistoryAsync(PasswordHistoryItem history)
        {
            if(history is not null)
            {
                await _db.InsertItemAsync(history);
            }
        }
    }
}
