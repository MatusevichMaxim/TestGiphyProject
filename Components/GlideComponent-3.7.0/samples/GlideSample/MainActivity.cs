using Android.App;
using Android.Widget;
using Android.OS;
using Com.Bumptech.Glide;

namespace GlideSample
{
	[Activity(Label = "GlideSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		Button button;
		ImageView img1, img2, img3, img4;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			button = FindViewById<Button>(Resource.Id.button1);

			img1 = FindViewById<ImageView>(Resource.Id.imageView1);
			img2 = FindViewById<ImageView>(Resource.Id.imageView2);
			img3 = FindViewById<ImageView>(Resource.Id.imageView3);
			img4 = FindViewById<ImageView>(Resource.Id.imageView4);

			button.Click += (obj, e) =>
			{
				ShowImage();
			};
		}

		void ShowImage()
		{
			base.OnResume();

			//basic
			Glide.With(this).Load("http://ketquaviet.vn/app/img/logo-kqv.png").DontAnimate().Into(img1);

			//transform
			Glide.With(this).Load("http://ketquaviet.vn/app/img/logo-kqv.png")
				 .Transform(new CircleTransform(this)).Into(img2);

			//animation
			var anim = new Android.Views
								  .Animations.ScaleAnimation(0, 1, 0, 1,
												  Android.Views.Animations.Dimension.RelativeToSelf, 0.5f,
												 Android.Views.Animations.Dimension.RelativeToSelf, 0.5f);
			anim.Duration = 2000;
			anim.RepeatCount = 0;
			Glide.With(this).Load("http://ketquaviet.vn/app/img/logo-kqv.png")
				 .Animate(anim).Into(img3);

			//support gif
			Glide.With(this).Load("http://ketquaviet.vn/uploads/web--landingkqv1.0.1.gif").FitCenter().Into(img4);
		}
	}
}


