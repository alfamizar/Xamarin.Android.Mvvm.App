using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Request;
using System.Collections.Generic;
using Xamarin.Android.Mvvm.App.Models;
using View = Android.Views.View;

namespace Xamarin.Android.Mvvm.App.Adapters
{
    public class UsersAdapter : RecyclerView.Adapter
    {
        public List<User> UsersList { get; set; }
        public IItemClickListener ItemClickListener;
        public override int ItemCount => UsersList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Glide.With(holder.ItemView.Context)
                .Load(UsersList[position].Picture.Large)
                .Apply(RequestOptions.CircleCropTransform())
                .Into((holder as UserViewHolder).UserPhotoImageView);
            (holder as UserViewHolder).UserFirstNameTextView.Text = UsersList[position].Name.First;
            (holder as UserViewHolder).UserLastNameTextView.Text = UsersList[position].Name.Last;
            (holder as UserViewHolder).UserAgeTextView.Text = UsersList[position].Dob.Age.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.UserCardView, parent, false);

            return new UserViewHolder(view, this);
        }

        public void SetOnClickListener(IItemClickListener itemClickListener)
        {
            this.ItemClickListener = itemClickListener;
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
                _itemsAdapter.ItemClickListener?.OnItemClicked(v, AbsoluteAdapterPosition);
            }
        }

        public interface IItemClickListener
        {
            void OnItemClicked(View view, int position);
        }
    }
}
