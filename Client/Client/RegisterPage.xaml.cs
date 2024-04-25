
using Client.Models;
using Client.NetCore.Api;
using System.Threading;

namespace Client;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        Shell.SetNavBarIsVisible(this, false);
    }
	private void OpenLogin(object sender, EventArgs e)
	{
        Navigation.RemovePage(this);
    }
    private async void Register(object sender, EventArgs e)
    {
        try
        {
            var token = await ApiManager.RegisterUser(new User
            {
                LastName = LastNameEntry.Text,
                Name = NameEntry.Text,
                Password = PasswordEntry.Text
            });
            Preferences.Set("token", token);

            await Shell.Current.GoToAsync("//ProfilePage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }
}