using TwilightAssistant.ViewModels;

namespace TwilightAssistant.Pages;

public partial class PlayerProfilePage : ContentPage
{
	public PlayerProfilePage(PlayerProfileViewModel ppvm)
	{
		InitializeComponent();
		BindingContext = ppvm;
	}
}