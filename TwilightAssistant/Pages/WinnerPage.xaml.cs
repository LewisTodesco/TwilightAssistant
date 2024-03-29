using TwilightAssistant.ViewModels;

namespace TwilightAssistant.Pages;

public partial class WinnerPage : ContentPage
{
	private readonly IDispatcher dispatcherProvider;
	public WinnerPage(WinnerPageViewModel wpvm, IDispatcher dispatcher)
	{
		BindingContext= wpvm;
		dispatcherProvider = dispatcher;
		InitializeComponent();
	}

	//Needed due to a bug with the poptorootasync navigation method.
	private async void GoHome(object sender, EventArgs e)
	{
        while (Navigation.NavigationStack.Count > 1)
        {
            Navigation.RemovePage(Navigation.NavigationStack[1]);
        }

        await dispatcherProvider.DispatchAsync(() => Shell.Current.GoToAsync(".."));
    }
}