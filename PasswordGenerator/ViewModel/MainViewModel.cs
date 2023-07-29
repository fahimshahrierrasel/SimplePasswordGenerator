using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly HistoryViewModel _historyVM;
        public MainViewModel(HistoryViewModel historyViewModel)
        {
            _historyVM = historyViewModel;
            ConfigChanged();
        }

        [ObservableProperty]
        string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTWXYZ";

        [ObservableProperty]
        bool isUsingUppercase = true;

        [ObservableProperty]
        string lowerCaseLetters = "abcdefghijklmnopqrstwxyz";

        [ObservableProperty]
        bool isUsingLowercase = true;

        [ObservableProperty]
        string numbers = "0123456789";

        [ObservableProperty]
        bool isUsingNumbers = true;

        [ObservableProperty]
        string symbols = "!@#$%^&*[{()}]_+=";

        [ObservableProperty]
        bool isUsingSymbols = false;

        [ObservableProperty]
        int length = 10;

        [ObservableProperty]
        string password = "";

        [RelayCommand]
        async Task CopyPassword()
        {
            await Clipboard.Default.SetTextAsync(Password);
            await _historyVM.InsertHistoryAsync(new Model.PasswordHistoryItem()
            {
                Password = Password,
                CopiedAt = DateTime.Now,
            });

            await Toast.Make("Password copied.", ToastDuration.Short, 16).Show();
        }

        [RelayCommand]
        void ConfigChanged()
        {
            var passwordSymbolSet = new List<string>();
            if (IsUsingUppercase)
            {
                passwordSymbolSet.Add(UpperCaseLetters);
            }

            if (IsUsingLowercase)
            {
                passwordSymbolSet.Add(LowerCaseLetters);
            }

            if (IsUsingNumbers)
            {
                passwordSymbolSet.Add(Numbers);
            }

            if (IsUsingSymbols)
            {
                passwordSymbolSet.Add(Symbols);
            }

            Password = GeneratePassword(passwordSymbolSet, Length);

        }

        [RelayCommand]
        async Task GoToHistory()
        {
            await Shell.Current.GoToAsync(nameof(HistoryPage));
        }

        string GeneratePassword(List<string> passwordSymbolSet, int length)
        {
            var password = new StringBuilder();
            var setCount = passwordSymbolSet.Count;
            for (int i = 0; i < length; i++)
            {
                var selectedSetIndex = RandomNumberGenerator.GetInt32(setCount);
                var selectedSet = passwordSymbolSet[selectedSetIndex];
                var selectedKeyIndex = RandomNumberGenerator.GetInt32(selectedSet.Length);
                var key = selectedSet.ElementAt(selectedKeyIndex);
                password.Append(key);
            }

            return password.ToString();
        }
    }
}
