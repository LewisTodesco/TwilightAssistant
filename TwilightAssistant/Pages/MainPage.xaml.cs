using TwilightAssistant.Models;
using TwilightAssistant.ViewModels;
using Newtonsoft.Json;

namespace TwilightAssistant.Pages;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel mpvm)
	{
        //ViewModel
        BindingContext = mpvm; //Set the binding context to the view model so we can pull in our List<GamePlayer> GamePlayers.

        InitializeComponent();

    }

    //This page needs to be updated when it appears for when the game ends
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Re-assign the binding context to a MainPageViewModel object.
        MainPageViewModel mainPageViewModel = (MainPageViewModel)BindingContext;
        //Update the information displayed on the main page.
        mainPageViewModel.UpdateMainPage(Path.Combine(FileSystem.Current.AppDataDirectory, "playerprofiles.json"), Path.Combine(FileSystem.Current.AppDataDirectory, "games.json"));
        //Initialize the page
        InitializeComponent();

        var navigationStack = Shell.Current.Navigation.NavigationStack;
    }


}

