package md5b5b44d40b7a0be757057b6f26c542490;


public class FirebaseService
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("SimpleList.FirebaseService, SimpleList, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FirebaseService.class, __md_methods);
	}


	public FirebaseService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FirebaseService.class)
			mono.android.TypeManager.Activate ("SimpleList.FirebaseService, SimpleList, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
