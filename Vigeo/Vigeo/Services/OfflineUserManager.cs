using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Vigeo.Models;
using Xamarin.Forms;

namespace Vigeo
{
	public class OfflineUserManager : ContentPage
	{
		/*
		private IMobileServiceSyncTable<UserModel> todoTable = App.CloudService.GetSyncTable<TodoItem>(); // offline sync

		private async Task InitLocalStoreAsync()
		{
			if (!App.MobileService.SyncContext.IsInitialized)
			{
				var store = new MobileServiceSQLiteStore("localstore.db");
				store.DefineTable<TodoItem>();
				await App.MobileService.SyncContext.InitializeAsync(store);
			}

			await SyncAsync();
		}

		private async Task SyncAsync()
		{
			await App.MobileService.SyncContext.PushAsync();
			await todoTable.PullAsync("todoItems", todoTable.CreateQuery());
		}


		public OfflineUserManager()
		{
			var store = new MobileServiceSQLiteStore("localstore.db");
			store.DefineTable<UserModel>();

			//Initializes the SyncContext using the default IMobileServiceSyncHandler.
			this.client.SyncContext.InitializeAsync(store);

			this.todoTable = client.GetSyncTable<UserModel>();
		}

		public async Task SyncAsync()
		{
			ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

			try
			{
				await this.client.SyncContext.PushAsync();

				await this.todoTable.PullAsync(
					"allTodoItems",
					this.todoTable.CreateQuery());
			}
			catch (MobileServicePushFailedException exc)
			{
				if (exc.PushResult != null)
				{
					syncErrors = exc.PushResult.Errors;
				}
			}

			// Simple error/conflict handling. 
			if (syncErrors != null)
			{
				foreach (var error in syncErrors)
				{
					if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
					{
						//Update failed, reverting to server's copy.
						await error.CancelAndUpdateItemAsync(error.Result);
					}
					else
					{
						// Discard local change.
						await error.CancelAndDiscardItemAsync();
					}

					Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.",
						error.TableName, error.Item["id"]);
				}
			}
		}
		*/
	}
}

