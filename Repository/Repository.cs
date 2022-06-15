using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Models;
using Xamarin.Android.Mvvm.App.Repository.WebService;

namespace Xamarin.Android.Mvvm.App.Repository
{
    public class WebRepository : IRepository
    {
        private readonly IMobileService _mobileService;

        public WebRepository()
        {
            _mobileService = MobileService.GetInstance();
        }

        public async Task<List<User>> GetUsersList(string gender, int count)
        {
            var response = await _mobileService.GetUsers(gender, count);
            var users = response.Users;
            return (List<User>)users; 
        }
    }
}


