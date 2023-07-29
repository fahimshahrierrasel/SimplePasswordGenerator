namespace PasswordGenerator;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
	}
}
