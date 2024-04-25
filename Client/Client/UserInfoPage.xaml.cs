using Client.Models;
using Client.NetCore.Api;

namespace Client;

public partial class UserInfoPage : ContentPage
{
    User user;
	public UserInfoPage(User user)
	{
		InitializeComponent();
        this.user = user;
        LoadProfile();
	}
    private void LoadProfile()
    {
        UserName.Text = user.FullName;
        Avatar.Source = user.Image;
    }
    private async void SendMessage(object sender, EventArgs e)
    {
        try
        {
            await ApiManager.SendMessage(Preferences.Get("token", ""), new MessageSendParams
            {
                Text = "Привет!",
                UserId = user.Id
            });
            await Navigation.PushAsync(new MessagePage(user.Id));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }
}