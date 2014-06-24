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

public class HttpServerPost {

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

	
}
