using VareskanningModels.SQL;

namespace Mobile_App;

public partial class App : Application
{
    public static User User;
    public static Product Product;
    public static Sale Sale;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
