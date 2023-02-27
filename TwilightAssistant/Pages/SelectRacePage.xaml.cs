using TwilightAssistant.ViewModels;

namespace TwilightAssistant.Pages;

public partial class SelectRacePage : ContentPage
{
	public SelectRacePage(SelectRaceViewModel srvm)
	{
		InitializeComponent();
		BindingContext = srvm;
	}

	//This page needs to be updated when it appears as we will be going to and from selection screens to assign Races.
	protected override void OnAppearing()
	{ 
		base.OnAppearing();
		//Initialize the page
        InitializeComponent();
		//Re-assign the binding context to a SelectRaceViewModel object.
        SelectRaceViewModel selectRaceViewModel = (SelectRaceViewModel)BindingContext;
		//Call the UpdateGamePlayers method to update the GamePlayers list. This will then update the UI through INotifyPropertyChanged.
		selectRaceViewModel.UpdateGamePlayers(Path.Combine(FileSystem.Current.AppDataDirectory, "gameplayers.json"));
    }
}