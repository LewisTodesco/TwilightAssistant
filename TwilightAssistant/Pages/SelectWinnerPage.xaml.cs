using TwilightAssistant.ViewModels;

namespace TwilightAssistant.Pages;

public partial class SelectWinnerPage : ContentPage
{
	public SelectWinnerPage(SelectWinnerViewModel swvm)
	{
        BindingContext = swvm;
        InitializeComponent();
	}

    
}