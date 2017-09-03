using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Vigeo.Abstractions;
using Vigeo.Models;
using Xamarin.Forms;

namespace Vigeo.ViewModels
{
	public class UsersViewModel : BaseViewModel
	{
		readonly ICloudTable<UserModel> table = App.CloudService.GetTable<UserModel>();

		public UserModel User { get; set; }

		public UsersViewModel(UserModel user = null)
		{
			if (user != null)
			{
				User = user;
				Debug.WriteLine("user = " + user);

			}

			else
			{
				Debug.WriteLine("user does not exist");
			}
		}


		async Task ExecuteSaveCommand()
		{

			try
			{
				if (User.Id == null)
				{
					await table.CreateItemAsync(User);
				}
				else
				{
					await table.UpdateItemAsync(User);
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[UserModel] Save error: {ex.Message}");
			}

		}
	}
}
