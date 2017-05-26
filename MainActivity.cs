using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Firebase.Iid;

namespace SimpleList
{
    [Activity(Label = "SimpleList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        const int USERS_COUNT = 5;
        ListView mListView;
        List<ListElement> list;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Toast.MakeText(this, GetString(Resource.String.google_app_id), ToastLength.Short).Show();

            if (!GetString(Resource.String.google_app_id).Equals("1:463204915779:android:c8bf0c7f75bb2b74"))
                throw new System.Exception("Invalid Json file");

            Task.Run(() => {
                var instanceId = FirebaseInstanceId.Instance;
                instanceId.DeleteInstanceId();
                Android.Util.Log.Debug("TAG", "{0} {1}", instanceId.Token, instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));

            });

            // load from json
            JSONData.Deserializing(this);

            mListView = FindViewById<ListView>(Resource.Id.listView1);

            list = new List<ListElement>();

            for (int i = 0; i < USERS_COUNT; i++)
            {
                list.Add(new ListElement() { ID = i, Name = JSONData.usersObj[i].UserName, Url = JSONData.usersObj[i].GifUrl });
            }

            SetAdapter(list);

            mListView.ItemClick += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(UserInfo));
                JSONData.currentUser = list[e.Position].ID;
                StartActivity(intent);
            };

            Button button = FindViewById<Button>(Resource.Id.sort_button);
            button.Click += (s, e) =>
            {
                Sort();
                SetAdapter(list);
            };
        }

        void Sort()
        {
            // sorting by name
            list.Sort(delegate (ListElement x, ListElement y)
            {
                if (x.Name == null && y.Name == null)
                    return 0;
                else if (x.Name == null)
                    return -1;
                else if (y.Name == null)
                    return 1;
                else return x.Name.CompareTo(y.Name);
            });
        }

        void SetAdapter(List<ListElement> usersList)
        {
            ListViewAdapter adapter = new ListViewAdapter(this, usersList);
            mListView.Adapter = adapter;
        }
    }
}

