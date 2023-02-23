using TwilightAssistant.ViewModels;

namespace TwilightAssistant;

public partial class SelectWinnerPage : ContentPage
{
	public SelectWinnerPage(SelectWinnerViewModel swvm)
	{
        BindingContext = swvm;
        InitializeComponent();
	}
}