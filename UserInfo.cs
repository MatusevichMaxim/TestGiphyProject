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
using Newtonsoft.Json.Linq;
using Android.Content.Res;
using System.IO;
using Newtonsoft.Json;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using static Android.App.DownloadManager;

namespace SimpleList
{
    [Activity(Label = "UserInfo")]
    class UserInfo : Activity
    {
        ImageView glideImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserInfo);

            TextView uName = FindViewById<TextView>(Resource.Id.user_name_text);
            EditText uEmail = FindViewById<EditText>(Resource.Id.email_et);
            EditText uPhone = FindViewById<EditText>(Resource.Id.phone_et);
            AutoCompleteTextView uAddress = FindViewById<AutoCompleteTextView>(Resource.Id.address_et);

            glideImage = FindViewById<ImageView>(Resource.Id.gif_imageview);
            GetGif();

            User user = JSONData.usersObj[JSONData.currentUser];

            uName.Text = "User name: " + user.UserName;
            uEmail.Text = user.UserEmail;
            uPhone.Text = user.UserPhone;
            uAddress.Text = user.UserAddress;

            Button saveButton = FindViewById<Button>(Resource.Id.button_save);
            saveButton.Click += (s, e) =>
            {
                Toast.MakeText(this, $"Saving...", ToastLength.Short).Show();

                if (uEmail.Text != null)
                    user.UserEmail = uEmail.Text;
                if (uPhone.Text != null)
                    user.UserPhone = uPhone.Text;
                if (uAddress.Text != null)
                    user.UserAddress = uAddress.Text;

                JSONData.Serializing(this);

                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
        }

        async void GetGif()
        {
            var giphy = new Giphy("dc6zaTOxFJmzC");
            var gifresult = await giphy.RandomGif(new RandomParameter()
            {
                Tag = "american psycho"
            });
        }
    }
}