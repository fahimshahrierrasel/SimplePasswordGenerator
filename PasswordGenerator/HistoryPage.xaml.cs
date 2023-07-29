using PasswordGenerator.ViewModel;

namespace PasswordGenerator;

public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _vm;
    public HistoryPage(HistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadHistoriesAsync();
    }
}