namespace TwilightAssistant;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Register Routes
		Routing.RegisterRoute(nameof(PlayerProfilePage), typeof(PlayerProfilePage));
		Routing.RegisterRoute(nameof(SelectPlayersPage), typeof(SelectPlayersPage));
		Routing.RegisterRoute(nameof(SelectRacePage),typeof(SelectRacePage));
		Routing.RegisterRoute(nameof(GetRacePage), typeof(GetRacePage));
		Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
		Routing.RegisterRoute(nameof(GamePage3), typeof(GamePage3));
        //Routing.RegisterRoute(nameof(GamePage4), typeof(GamePage4));
        //Routing.RegisterRoute(nameof(GamePage5), typeof(GamePage5));
        //Routing.RegisterRoute(nameof(GamePage6), typeof(GamePage6));
        //Routing.RegisterRoute(nameof(GamePage7), typeof(GamePage7));
        //Routing.RegisterRoute(nameof(GamePage8), typeof(GamePage8));
        Routing.RegisterRoute(nameof(SelectWinnerPage), typeof(SelectWinnerPage));
		Routing.RegisterRoute(nameof(WinnerPage), typeof(WinnerPage));
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));	
	}
}
