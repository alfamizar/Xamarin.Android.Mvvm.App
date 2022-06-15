using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Models;
using Xamarin.Android.Mvvm.App.Repository;

namespace Xamarin.Android.Mvvm.App.ViewModels
{
    public class MainActivityUsersViewModel : AndroidBaseViewModel
    {
        private readonly IRepository _repository;
        public ObservableCollection<User> UsersList { get; private set; }

        public MainActivityUsersViewModel()
        {
            _repository = new WebRepository();
            UsersList = new ObservableCollection<User>();
        }

        public async Task OnLoadUsersButtonClicked(string gender, int count)
        {
            if (IsBusy) return;

            IsBusy = true;

            var users = await _repository.GetUsersList(gender, count);

            foreach (var user in users)
            {
                UsersList.Add(user);
            }

            IsBusy = false;
        }
    }
}
