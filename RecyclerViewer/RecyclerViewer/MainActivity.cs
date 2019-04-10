using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace RecyclerViewer
{
	[Activity (Label = "RecyclerViewer", MainLauncher = true, Icon = "@drawable/icon", 
               Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
	public class MainActivity : Activity
	{

		RecyclerView mRecyclerView;

		RecyclerView.LayoutManager mLayoutManager;

		PhotoAlbumAdapter mAdapter;

        PhotoAlbum mPhotoAlbum;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            mPhotoAlbum = new PhotoAlbum();
			SetContentView (Resource.Layout.Main);
			mRecyclerView = FindViewById<RecyclerView> (Resource.Id.recyclerView);
            //mLayoutManager = new LinearLayoutManager (this);
            mLayoutManager = new GridLayoutManager(this, 3, GridLayoutManager.Horizontal, false);
            mRecyclerView.SetLayoutManager (mLayoutManager);
			mAdapter = new PhotoAlbumAdapter (mPhotoAlbum);
            mAdapter.ItemClick += OnItemClick;         
			mRecyclerView.SetAdapter (mAdapter);
            Button randomPickBtn = FindViewById<Button>(Resource.Id.randPickButton);
            randomPickBtn.Click += delegate
            {
                if (mPhotoAlbum != null)
                {
                    int idx = mPhotoAlbum.RandomSwap();
                    mAdapter.NotifyItemChanged(0);
                    mAdapter.NotifyItemChanged(idx);
                }
            };
		}


        void OnItemClick (object sender, int position)
        {
            int photoNum = position + 1;
            Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }
	}


    public class PhotoViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }

        public PhotoViewHolder (View itemView, Action<int> listener) 
            : base (itemView)
        {
            Image = itemView.FindViewById<ImageView> (Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView> (Resource.Id.textView);

            itemView.Click += (sender, e) => listener (base.LayoutPosition);
        }
    }

    public class PhotoAlbumAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public PhotoAlbum mPhotoAlbum;
        public PhotoAlbumAdapter (PhotoAlbum photoAlbum)
        {
            mPhotoAlbum = photoAlbum;
        }

        public override RecyclerView.ViewHolder 
            OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From (parent.Context).
                        Inflate (Resource.Layout.PhotoCardView, parent, false);

            PhotoViewHolder vh = new PhotoViewHolder (itemView, OnClick); 
            return vh;
        }
        public override void 
            OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
        {
            PhotoViewHolder vh = holder as PhotoViewHolder;

            vh.Image.SetImageResource (mPhotoAlbum[position].PhotoID);
            vh.Caption.Text = mPhotoAlbum[position].Caption;
        }
        public override int ItemCount
        {
            get { return mPhotoAlbum.NumPhotos; }
        }
        void OnClick (int position)
        {
            if (ItemClick != null)
                ItemClick (this, position);
        }
    }
}
