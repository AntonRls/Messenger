using Client.Models;
using Client.NetCore.Api;

namespace Client;

public partial class DialogsPage : ContentPage
{
	public DialogsPage()
	{
        Shell.SetTabBarIsVisible(this, true);
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        InitializeComponent();

        LoadDialogs();
    }
    
    async void LoadDialogs()
    {
        try
        {
            var dialogs = await ApiManager.GetDialogs(Preferences.Get("token", ""));
            listView.ItemsSource = dialogs;
        }
        catch(Exception ex) 
        {
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }
    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var dialog = (Dialog)e.Item;
        await Navigation.PushAsync(new MessagePage(dialog.User.Id));
    }
}