using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Request;
using Xamarin.Android.Mvvm.App.Data.DataSource.Models;
using View = Android.Views.View;

namespace Xamarin.Android.Mvvm.App.Presentation.Adapters
{
    public class UsersAdapter : RecyclerView.Adapter
    {
        private JavaList<User> _usersList;
        public IItemClickListener ItemClickListener;
        public override int ItemCount => _usersList.Count;

        public UsersAdapter(IItemClickListener itemClickListener)
        {
            _usersList = new JavaList<User>();
            ItemClickListener = itemClickListener;
        }

        public void SetData(JavaList<User> users)
        {
            _usersList = users;
            NotifyDataSetChanged();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var userViewHolder = holder as UserViewHolder;
            var user = _usersList[position];

            Glide.With(userViewHolder.ItemView.Context)
                .Load(user.Picture.Large)
                .Apply(RequestOptions.CircleCropTransform())
                .Into(userViewHolder.UserPhotoImageView);

            userViewHolder.UserFirstNameTextView.Text = user.Name.First;
            userViewHolder.UserLastNameTextView.Text = user.Name.Last;
            userViewHolder.UserAgeTextView.Text = user.Dob.Age.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.UserCardView, parent, false);

            return new UserViewHolder(view, this);
        }

        class UserViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
        {
            public readonly ImageView UserPhotoImageView;
            public readonly TextView UserFirstNameTextView;
            public readonly TextView UserLastNameTextView;
            public readonly TextView UserAgeTextView;
            private readonly UsersAdapter _itemsAdapter;

            public UserViewHolder(global::Android.Views.View itemView, UsersAdapter adapter) : base(itemView)
            {
                UserPhotoImageView = itemView.FindViewById<ImageView>(Resource.Id.UserPhotoImageView);
                UserFirstNameTextView = itemView.FindViewById<TextView>(Resource.Id.UserFirstNameTextView);
                UserLastNameTextView = itemView.FindViewById<TextView>(Resource.Id.UserLastNameTextView);
                UserAgeTextView = itemView.FindViewById<TextView>(Resource.Id.UserAgeTextView);
                itemView.SetOnClickListener(this);
                _itemsAdapter = adapter;
            }

            public void OnClick(View v)
            {
                _itemsAdapter.ItemClickListener?.ListItemOnClick(v, AbsoluteAdapterPosition);
            }
        }

        public interface IItemClickListener
        {
            void ListItemOnClick(View view, int position);
        }
    }
}
