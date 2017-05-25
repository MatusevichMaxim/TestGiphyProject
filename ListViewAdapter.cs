using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimpleList
{
    class ListViewAdapter : BaseAdapter<ListElement>
    {
        public List<ListElement> mItems;
        private Context context;

        public ListViewAdapter(Context context, List<ListElement> items)
        {
            mItems = items;
            this.context = context;
        }

        public override int Count
        {
            get { return mItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ListElement this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View user = convertView;
            if (user == null)
            {
                user = LayoutInflater.From(context).Inflate(Resource.Layout.UserItem, null, false);
            }

            TextView txtName = user.FindViewById<TextView>(Resource.Id.textView1);
            txtName.Text = mItems[position].Name;

            return user;
        }
    }
}