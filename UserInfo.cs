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
using Felipecsl.GifImageViewLibrary;
using System.Net.Http;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using Com.Bumptech.Glide;

namespace SimpleList
{
    [Activity(Label = "UserInfo")]
    class UserInfo : Activity
    {
        GifImageView gifImageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserInfo);

            TextView uName = FindViewById<TextView>(Resource.Id.user_name_text);
            EditText uEmail = FindViewById<EditText>(Resource.Id.email_et);
            EditText uPhone = FindViewById<EditText>(Resource.Id.phone_et);
            AutoCompleteTextView uAddress = FindViewById<AutoCompleteTextView>(Resource.Id.address_et);
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImage);

            //GifStart();
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
        /*
        private async void GifStart()
        {
            var giphy = new Giphy("dc6zaTOxFJmzC");
            var gifresult = await giphy.RandomGif(new RandomParameter()
            {
                Tag = "american psycho"
            });

            
            try
            {
                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync("http://api.giphy.com/v1/gifs/random?api_key=dc6zaTOxFJmzC&tag=cat");

                    gifImageView.SetBytes(bytes);
                    gifImageView.StartAnimation();
                }
            }
            catch (Exception e)
            {
            
            }
        }
        */
    }
}