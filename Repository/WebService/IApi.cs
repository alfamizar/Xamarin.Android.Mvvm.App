using Refit;
using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Models;

namespace Xamarin.Android.Mvvm.App.Repository.WebService
{
    public interface IApi
    {
        [Get("/api/?gender={gender}&results={count}")]
        Task<UsersResponse> GetRandomUsers(string gender, int count);

        [Get("/api/")]
        Task<UsersResponse> GetRandomUser();
    }
}
