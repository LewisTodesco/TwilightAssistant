using TwilightAssistant.Models;
using TwilightAssistant.ViewModels;
using Newtonsoft.Json;

namespace TwilightAssistant;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel mpvm)
	{
        //ViewModel
        BindingContext = mpvm; //Set the binding context to the view model so we can pull in our List<GamePlayer> GamePlayers.

        InitializeComponent();

    }

    //This page needs to be updated when it appears as we will be going to and from selection screens to assign Races.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Re-assign the binding context to a MainPageViewModel object.
        MainPageViewModel mainPageViewModel = (MainPageViewModel)BindingContext;
        //Update the information displayed on the main page.
        mainPageViewModel.UpdateMainPage();
        //Initialize the page
        InitializeComponent();
    }


}

