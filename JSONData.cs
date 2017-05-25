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
using System.IO;
using Android.Content.Res;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SimpleList
{
    class JSONData
    {
        public static IList<User> usersObj;
        public static int currentUser;
        static IList<JToken> users;
        static AssetManager assets;
        static string jsonContent;
        static string jsonFileName = "document.json";

        public static User GetUserByID(int id)
        {
            return usersObj[id];
        }

        public static void Serializing(Context context)
        {
            jsonContent = JsonConvert.SerializeObject(usersObj, Formatting.Indented);
            //var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //var filename = Path.Combine(documents, "jsontable.json");
            //File.WriteAllText(filename, jsonContent);
            //Toast.MakeText(context, filename, ToastLength.Short).Show();
        }

        public static void Deserializing(Context context)
        {
            if (jsonContent == null)
            {
                assets = context.Assets;
                using (StreamReader sr = new StreamReader(assets.Open(jsonFileName)))
                {
                    jsonContent = sr.ReadToEnd();                    
                }

                JObject userInfo = JObject.Parse(jsonContent);

                users = userInfo["users"].Children().ToList();
                usersObj = new List<User>();

                foreach (JToken user in users)
                {
                    User userObj = user.ToObject<User>();
                    usersObj.Add(userObj);
                }
            }
        }
    }
}