using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Request;
using Xamarin.Android.Mvvm.App.Models;
using View = Android.Views.View;

namespace Xamarin.Android.Mvvm.App.Adapters
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
            Glide.With(holder.ItemView.Context)
                .Load(_usersList[position].Picture.Large)
                .Apply(RequestOptions.CircleCropTransform())
                .Into((holder as UserViewHolder).UserPhotoImageView);
            (holder as UserViewHolder).UserFirstNameTextView.Text = _usersList[position].Name.First;
            (holder as UserViewHolder).UserLastNameTextView.Text = _usersList[position].Name.Last;
            (holder as UserViewHolder).UserAgeTextView.Text = _usersList[position].Dob.Age.ToString();
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
