using PasswordGenerator.ViewModel;

namespace PasswordGenerator;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}

