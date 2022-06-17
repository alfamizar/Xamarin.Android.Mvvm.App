using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Android.Content.PM;
using AndroidX.Lifecycle;
using AndroidX.RecyclerView.Widget;
using System.ComponentModel;
using Debug = System.Diagnostics.Debug;
using Google.Android.Material.ProgressIndicator;
using static Xamarin.Android.Mvvm.App.Adapters.UsersAdapter;
using Xamarin.Android.Mvvm.App.Adapters;
using Xamarin.Android.Mvvm.App.ViewModels;
using Java.Lang;
using Xamarin.Android.Mvvm.App.Views;

namespace Xamarin.Android.Mvvm.App
{
    [Activity(Theme = "@style/Theme.MyApplication.NoActionBar", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : AppCompatActivity, IItemClickListener
    {
        private UsersAdapter _itemsAdapter;
        private RecyclerView _recyclerView;
        private FloatingActionButton _floatingActionButton;
        private CircularProgressIndicator _isBusyProgressBar;
        private MainActivityUsersViewModel _viewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ActivityMain);

            _viewModel = new ViewModelProvider(this)
                .Get(Class.FromType(typeof(MainActivityUsersViewModel))) as MainActivityUsersViewModel;

            _viewModel.PropertyChanged += UsersViewModelPropertyChanged;
           
            InitViews();

            _viewModel.GetLiveData().Observe(this, new ListObserver(_itemsAdapter.SetData));
        }

        private void InitViews()
        {
            _isBusyProgressBar = FindViewById<CircularProgressIndicator>(Resource.Id.IsBusyProgressBar);
            InitFab();
            InitRecycler();
        }

        private void InitFab()
        {
            _floatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.FetchUsersButton);
            _floatingActionButton.Click += async (o, s) =>
            {
                await _viewModel.OnLoadUsersButtonClicked("female", 25);
            };
        }

        private void InitRecycler()
        {
            _itemsAdapter = new UsersAdapter(this);
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.UsersRecyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            _recyclerView.SetAdapter(_itemsAdapter);
        }

        private void UsersViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsBusy":
                    if (_viewModel.IsBusy)
                    {
                        ChangeProgressBarVisibility(ViewStates.Visible);
                    }
                    else
                    {
                        ChangeProgressBarVisibility(ViewStates.Invisible);
                    }
                    Debug.WriteLine("IsBusyChanged");
                    break;
                default:
                    Debug.WriteLine("SomePropertyChanged");
                    break;
            }
        }

        private void ChangeProgressBarVisibility(ViewStates viewState)
        {
            _isBusyProgressBar.Visibility = viewState;
        }

        public void ListItemOnClick(global::Android.Views.View view, int position)
        {
            Debug.WriteLine($"Position {position} clicked");
        }
    }
}