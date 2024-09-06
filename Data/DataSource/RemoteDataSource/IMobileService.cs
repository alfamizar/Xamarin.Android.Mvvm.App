using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Data.DataSource.Models;

namespace Xamarin.Android.Mvvm.App.Data.DataSource.RemoteDataSource
{
    public interface IMobileService
    {
        Task<UsersResponse> GetUsers(string gender, int count);

        Task<UsersResponse> GetUser();
    }
}
