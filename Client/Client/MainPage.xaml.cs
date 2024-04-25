using Client.NetCore.Api;

namespace Client;

public partial class MainPage : ContentPage
{

    public MainPage()
    {

        InitializeComponent();

    }

    private async void AuthUser(object sender, EventArgs e)
    {
        try
        {


            if (Preferences.ContainsKey("token"))
            {
                await Shell.Current.GoToAsync("//ProfilePage");
                return;
            }
            var token = await ApiManager.AuthUser(new Models.User
            {
                Id = int.Parse(IdEntry.Text),
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

    private void OpenRegister(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }
}

