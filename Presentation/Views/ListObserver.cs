using Android.Runtime;
using AndroidX.Lifecycle;
using System;
using Xamarin.Android.Mvvm.App.Data.DataSource.Models;

namespace Xamarin.Android.Mvvm.App.Presentation.Views
{
    class ListObserver : Java.Lang.Object, IObserver
    {
        public Action<JavaList<User>> Action { get; set; }

        public ListObserver(Action<JavaList<User>> action)
        {
            Action = action;
        }

        public void OnChanged(Java.Lang.Object p0)
        {
            Action?.Invoke((JavaList<User>)p0);
        }
    }
}