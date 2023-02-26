using TwilightAssistant.ViewModels;

namespace TwilightAssistant.Pages;

public partial class GetRacePage : ContentPage
{
	public GetRacePage(GetRaceViewModel grvm)
	{
		InitializeComponent();
		BindingContext = grvm;
	}
}