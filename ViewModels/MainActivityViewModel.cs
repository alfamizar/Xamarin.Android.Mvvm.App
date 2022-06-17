﻿using AndroidX.Lifecycle;
using System.Threading.Tasks;
using Xamarin.Android.Mvvm.App.Models;
using Xamarin.Android.Mvvm.App.Repository;
using Android.Runtime;

namespace Xamarin.Android.Mvvm.App.ViewModels
{
    public class MainActivityUsersViewModel : AndroidBaseViewModel
    {
        private readonly IRepository _repository;

        private MutableLiveData data;

        public MainActivityUsersViewModel()
        {
            _repository = new WebRepository();
            data = new MutableLiveData();
        }

        public LiveData GetLiveData()
        {
            return data;
        }

        public async Task OnLoadUsersButtonClicked(string gender, int count)
        {
            if (IsBusy) return;

            IsBusy = true;

            var users = await _repository.GetUsersList(gender, count);

            JavaList<User> javaUsers = new JavaList<User> ();

            foreach (var user in users)
            {
                javaUsers.Add(user);
            }

            data.PostValue(javaUsers);

            IsBusy = false;
        }
    }
}
