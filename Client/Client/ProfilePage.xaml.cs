using Client.NetCore.Api;

namespace Client;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
        Shell.SetTabBarIsVisible(this, true);
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        InitializeComponent();

        LoadProfile();
    }
    async void LoadProfile()
    {
        try
        {
            var user = await ApiManager.GetUserInfo(Preferences.Get("token", "none"));
            UserName.Text = $"{user.Name} {user.LastName}";
            Avatar.Source = ApiManager.GetImage(user.Id);
        }
        catch(Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }
    public async void LoadImage(object sender, EventArgs e)
    {
        var photo = await PickPhoto(PickOptions.Images);
        using var stream = await photo.OpenReadAsync();
        byte[] bytes;
        using (var memoryStream = new MemoryStream())
        {
            stream.CopyTo(memoryStream);
            bytes = memoryStream.ToArray();
        }
        string base64 = Convert.ToBase64String(bytes);
        await ApiManager.LoadImage(Preferences.Get("token", ""), base64);
        LoadProfile();
    }
    public async Task<FileResult> PickPhoto(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            return result;
        }
        catch { }

        return null;
    }
}