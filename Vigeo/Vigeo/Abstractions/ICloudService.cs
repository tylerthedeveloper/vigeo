using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Vigeo.Models;

namespace Vigeo.Abstractions
{
    public interface ICloudService
    {
        //Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;

        ICloudTable<T> GetTable<T>() where T : TableData;

        Task LoginAsync();

		Task<UserModel> GetUserGraph();

        //Task<MobileServiceUser> LoginAsync();

    }
}
