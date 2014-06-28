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

import android.os.AsyncTask;
import android.util.Log;

public class HttpServerPost extends AsyncTask<HttpServerPost, Void, String> {

	String urlToPost;
	List<NameValuePair> nameValuePair;
	String[] postHeader;
	
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
		if (params[i].postHeader != null)
		{
		hP.addHeader(params[i].postHeader[i],params[i].postHeader[1]);
		}
		
		try
		{
			hP.setEntity(new UrlEncodedFormEntity(params[i].nameValuePair));
			
			HttpResponse hR = hC.execute(hP);
			Log.i("WasUploadSuccessful", hR.getStatusLine().toString() );

		}
		catch(ClientProtocolException e)
		{
			
		}
		catch(IOException e)
		{
			
		}
	}
		return null;

	}

	
}
