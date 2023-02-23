using TwilightAssistant.ViewModels;

namespace TwilightAssistant;

public partial class SelectPlayersPage : ContentPage
{
	public SelectPlayersPage(SelectPlayersViewModel spvm)
	{
		InitializeComponent();

		BindingContext = spvm;
	}
}