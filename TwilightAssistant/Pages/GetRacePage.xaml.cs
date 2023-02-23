using TwilightAssistant.ViewModels;

namespace TwilightAssistant;

public partial class GetRacePage : ContentPage
{
	public GetRacePage(GetRaceViewModel grvm)
	{
		InitializeComponent();
		BindingContext = grvm;
	}
}