using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Android.Mvvm.App.Models;

namespace Xamarin.Android.Mvvm.App.Views
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