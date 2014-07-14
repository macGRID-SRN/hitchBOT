package com.example.hitchbot;

import java.io.IOException;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.HTTP;
import org.apache.http.util.EntityUtils;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

public class HttpServerPost extends AsyncTask<HttpServerPost, Void, String> {

	String urlToPost;
	List<NameValuePair> nameValuePair;
	String[] postHeader;
	Context context;
	
	public HttpServerPost(String URLtoPost, List<NameValuePair> dataToSend, String[] postHeader)
	{
		this.urlToPost =URLtoPost;
		this.nameValuePair = dataToSend;
		this.postHeader = postHeader;
	}
	
	public HttpServerPost(String URLtoPost, List<NameValuePair> dataToSend)
	{
		this.urlToPost = URLtoPost;
		this.nameValuePair = dataToSend;
		this.postHeader = null;
	}
	
	//Had to make this constructor because Android doesn't play nice with windows azure api formatting
	public HttpServerPost(String URLtoPost, Context context)
	{
		this.urlToPost = URLtoPost;
		this.nameValuePair = null;
		this.postHeader = null;
		this.context = context;
	}
		
	
	public void postData()
	{
		HttpClient hC = new DefaultHttpClient();
		HttpPost hP = new HttpPost(urlToPost);
		if (postHeader != null)
		{
		hP.addHeader(postHeader[0], postHeader[1]);
		}
		
		try
		{
			hP.setEntity(new UrlEncodedFormEntity(nameValuePair));
			
			HttpResponse hR = hC.execute(hP);
			Log.i("WasUploadSuccessful", EntityUtils.toString(hR.getEntity()) );

		}
		catch(ClientProtocolException e)
		{
			
		}
		catch(IOException e)
		{
			
		}
		
	}



	@Override
	protected String doInBackground(HttpServerPost... params) {	
		for(int i = 0; i < params.length ; i ++){
		HttpClient hC = new DefaultHttpClient();
		HttpPost hP = new HttpPost(params[i].urlToPost);
		Log.i("WasUploadSuccessful", urlToPost );

		if (params[i].postHeader != null)
		{
		hP.addHeader(params[i].postHeader[0],params[i].postHeader[1]);
		Log.i("WasUploadSuccessful", "header isn't null" );

		}
		
		try
		{
			if(nameValuePair != null)
			{
			hP.setEntity(new UrlEncodedFormEntity(params[i].nameValuePair, HTTP.UTF_8));
			}
			
			HttpResponse hR = hC.execute(hP);
			String responseCheck = EntityUtils.toString(hR.getEntity());
			Log.i("WasUploadSuccessful", responseCheck );
			if(!responseCheck.equals("true"))
			{
				DatabaseQueue dQ = new DatabaseQueue(context);
				dQ.addItemToQueue(new HttpPostDb(urlToPost, 1, 0));
			}

		}
		catch(ClientProtocolException e)
		{
			Log.i("WasUploadSuccessful", e.toString() );
			DatabaseQueue dQ = new DatabaseQueue(context);
			dQ.addItemToQueue(new HttpPostDb(urlToPost, 1, 0));
			

		}
		catch(IOException e)
		{
			Log.i("WasUploadSuccessful", e.toString() );
			DatabaseQueue dQ = new DatabaseQueue(context);
			dQ.addItemToQueue(new HttpPostDb(urlToPost, 1, 0));
			

		}
	}
		return null;

	}

	
}
