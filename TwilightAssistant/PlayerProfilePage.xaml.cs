using TwilightAssistant.ViewModels;

namespace TwilightAssistant;

public partial class PlayerProfilePage : ContentPage
{
	public PlayerProfilePage(PlayerProfileViewModel ppvm)
	{
		InitializeComponent();
		BindingContext = ppvm;
	}
}