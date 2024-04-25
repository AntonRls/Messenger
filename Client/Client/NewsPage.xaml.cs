using Client.Models;
using Client.NetCore.Api;

namespace Client;

public partial class NewsPage : ContentPage
{
	public NewsPage()
	{
     
        Shell.SetTabBarIsVisible(this, true);
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        InitializeComponent();

        Load();
    }
    async void Load()
    {
        try
        {
            var users = await ApiManager.GetAllUsers();
            listView.ItemsSource = users;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var user = (User)e.Item;
        Navigation.PushAsync(new UserInfoPage(user));
    }
}