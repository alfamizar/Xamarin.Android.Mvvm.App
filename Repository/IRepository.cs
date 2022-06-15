using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Models;

namespace Xamarin.Android.Mvvm.App.Repository
{
    public interface IRepository
    {
        Task<List<User>> GetUsersList(string gender, int count);
    }
}
