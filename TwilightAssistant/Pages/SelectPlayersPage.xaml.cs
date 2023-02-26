using TwilightAssistant.ViewModels;

namespace TwilightAssistant.Pages;

public partial class SelectPlayersPage : ContentPage
{
	public SelectPlayersPage(SelectPlayersViewModel spvm)
	{
		InitializeComponent();

		BindingContext = spvm;
	}
}