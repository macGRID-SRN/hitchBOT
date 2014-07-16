package com.example.hitchbot;

import java.net.URI;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.TimeZone;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.util.Log;

public class PostGeneralUpdates {

	List<ErrorLog> errorLogQueue;
	List<HttpPostDb> imagePostQueue;
	List<HttpPostDb> imgurUploadQueue;
	DatabaseQueue dQ;
	String TAG  = "PostGeneralUpdates";
	
	public PostGeneralUpdates()
	{
		dQ = new DatabaseQueue(Config.context);
		this.errorLogQueue = dQ.errorLogUploadQueue();
		this.imgurUploadQueue = dQ.imgurUploadQueue();
		this.imagePostQueue = dQ.serverImageLinkUploadQueue();
	}
	
	public void sendErrorLog()
	{
		for(int i = 0; i < errorLogQueue.size() ; i ++)
		{
			Log.i(TAG, "error log " + String.valueOf(errorLogQueue.size()));
			if(isNetworkAvailable())
			{
				String url1 = "http://hitchbotapi.azurewebsites.net/api/Exception?HitchBotID=%s&Message=%s&TimeOccured=%s";
				String hitchBOT_ID = Config.HITCHBOT_ID;
				String exception = errorLogQueue.get(i).getErrorMessage();
				String timeStamp = Config.getUtcDate();
				HttpServerPost hSp = new HttpServerPost(String.format(url1,hitchBOT_ID, exception, timeStamp ), Config.context);
				hSp.execute(hSp);
				dQ.markAsUploadedToServer(errorLogQueue.get(i));
			}
	}
	}
	
	public void sendImagePosts()
	{
		for(int i = 0 ;  i < imagePostQueue.size(); i++)
		{
			Log.i(TAG, "image post " + String.valueOf(imagePostQueue.size()));

			if(isNetworkAvailable())
			{
			String url1 = imagePostQueue.get(i).getURI();						
			HttpServerPost  hSp = new HttpServerPost(url1, Config.context);
			hSp.execute(hSp);	
			//they are marked as uploaded because if they upload unsuccessfully, they will be
			//re-added in the queue from the server post class (probably a better way to do it)
			dQ.markAsUploadedToServer(imagePostQueue.get(i));
			}
		}
		
	}
	public void sendImgurUploads()
	{
		for(int i = 0 ;  i < imgurUploadQueue.size(); i++)
		{
			Log.i(TAG, "imgur " + String.valueOf(imgurUploadQueue.size()));

			if(isNetworkAvailable())
			{
			Uri mUri = Uri.parse(imgurUploadQueue.get(i).getURI());
			new UploadImageImgur(mUri, Config.context, Config.context).execute();
			//they are marked as uploaded because if they upload unsuccessfully, they will be
			//re-added in the queue from the server post class (probably a better way to do it)
			dQ.markAsUploadedToImgur(imgurUploadQueue.get(i));
			}
		}
	}
	

	public boolean isNetworkAvailable()
	{
		ConnectivityManager connectivityManager = (ConnectivityManager)Config.context.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
		return activeNetworkInfo != null && activeNetworkInfo.isConnected();
	}
	
		
	
}