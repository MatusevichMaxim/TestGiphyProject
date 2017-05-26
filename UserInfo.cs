using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using Felipecsl.GifImageViewLibrary;
using System.Net.Http;
using Android.Views.InputMethods;

namespace SimpleList
{
    [Activity(Label = "UserInfo")]
    class UserInfo : Activity
    {
        GifImageView gifImage;
        string searchingText;
        string url;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserInfo);

            AutoCompleteTextView uAddress = FindViewById<AutoCompleteTextView>(Resource.Id.address_et);
            TextView uName = FindViewById<TextView>(Resource.Id.user_name_text);
            EditText uEmail = FindViewById<EditText>(Resource.Id.email_et);
            EditText uPhone = FindViewById<EditText>(Resource.Id.phone_et);
            EditText searchTextView = FindViewById<EditText>(Resource.Id.search_et);
            gifImage = FindViewById<GifImageView>(Resource.Id.gifImage);

            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);

            User user = JSONData.usersObj[JSONData.currentUser];

            uName.Text = "User name: " + user.UserName;
            uEmail.Text = user.UserEmail;
            uPhone.Text = user.UserPhone;
            uAddress.Text = user.UserAddress;

            GetGif(user.GifUrl);

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
                user.GifUrl = url;

                JSONData.Serializing(this);

                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            Button searchButton = FindViewById<Button>(Resource.Id.button_search);
            searchButton.Click += (s, e) =>
            {
                Toast.MakeText(this, "Searching...", ToastLength.Short).Show();
                searchingText = searchTextView.Text.ToString();
                searchingText.Replace(" ", "+");

                // hide keyboard
                imm.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                GetGif(null);
            };
        }
        
        async void GetGif(string url)
        {
            var giphy = new Giphy("dc6zaTOxFJmzC");
            var gifresult = await giphy.RandomGif(new RandomParameter()
            {
                Tag = searchingText
            });

            try
            {
                using (var client = new HttpClient())
                {
                    if (url == null) // refresh gif
                    {
                        this.url = "https://media.giphy.com/media/" + gifresult.Data.Id.ToString() + "/giphy.gif";
                        //Toast.MakeText(this, "1", ToastLength.Short).Show();
                    }
                    else // load from json
                    {
                        this.url = url;
                        //Toast.MakeText(this, "2", ToastLength.Short).Show();
                    }

                    var bytes = await client.GetByteArrayAsync(this.url);
                    gifImage.SetBytes(bytes);
                    gifImage.StartAnimation();
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Bad connection", ToastLength.Short).Show();
            }
        }
    }
}