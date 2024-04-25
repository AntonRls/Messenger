using Client.Models;
using Client.NetCore.Api;
using System.Collections.ObjectModel;

namespace Client;

public partial class MessagePage : ContentPage
{
	int WithId;
	public MessagePage(int user_id)
	{
		InitializeComponent();

		WithId = user_id;
		listView.ItemsSource = messages;
		BindingContext = this;
		UpdateMessage();
	}
    public ObservableCollection<Message> messages { get; set; } = new ObservableCollection<Message>();

    async void LoadMessage()
	{
		var messagesNew = await ApiManager.GetMessages(Preferences.Get("token", ""), WithId);
		bool update = false;
		foreach(var message in messagesNew)
		{
			bool isAdd = true;
			foreach(var oldMessage  in messages)
			{
				if (oldMessage.MessageId == message.MessageId)
				{
					isAdd = false;
					break;
				}
			}
			if (isAdd)
			{
				messages.Add(message);
				update = true;
			}
		}
	
	
    }
	async void UpdateMessage()
	{
        while (true)
        {
            LoadMessage();
            await Task.Delay(500);
        }
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			await ApiManager.SendMessage(Preferences.Get("token", ""), new Models.MessageSendParams
			{
				Text = MessageEntry.Text,
				UserId = WithId
			});
			MessageEntry.Text = "";
			LoadMessage();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ошибка", ex.Message, "OK");
		}
    }
}