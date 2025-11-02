using UnityEngine;
using UnityEngine.Networking;

internal class Net
{
	// Replaced deprecated WWW with UnityWebRequest.
	// We keep the same polling-style API (update checks completion) so existing code paths work.
	public static UnityWebRequest www;
	public static AsyncOperation wwwOp;

	public static Command h;

	public static void update()
	{
		if (www != null && (wwwOp != null ? wwwOp.isDone : true))
		{
			string str = string.Empty;

			bool success = true;
#if UNITY_2020_1_OR_NEWER
			success = www.result == UnityWebRequest.Result.Success;
#else
			// Older Unity versions use isNetworkError/isHttpError
			success = !(www.isHttpError || www.isNetworkError);
#endif

			if (success)
			{
				if (www.downloadHandler != null)
					str = www.downloadHandler.text;
			}
			else
			{
				// fallback to provide error text similar to WWW.error
				try
				{
					str = www.error;
				}
				catch
				{
					str = "";
				}
			}

			www.Dispose();
			www = null;
			wwwOp = null;

			if (h != null)
				h.perform(str);
		}
	}

	public static void connectHTTP(string link, Command h)
	{
		if (www != null)
			Cout.LogError("GET HTTP BUSY");

		UnityWebRequest req = UnityWebRequest.Get(link);
		www = req;
		wwwOp = req.SendWebRequest();
		Net.h = h;
	}

	public static void connectHTTP2(string link, Command h)
	{
		Net.h = h;
		if (link != null)
			h.perform(link);
	}
}
