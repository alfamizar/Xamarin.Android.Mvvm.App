using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Models;

namespace Xamarin.Android.Mvvm.App.Repository.WebService
{
    public interface IMobileService
    {
        Task<UsersResponse> GetUsers(string gender, int count);

        Task<UsersResponse> GetUser();
    }
}
