package com.example.hitchbot;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URI;
import java.net.URISyntaxException;

import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

public class HttpServerGet {
	
	String Uri;
	BufferedReader in = null;
	
	public HttpServerGet(String Uri)
	{
		this.Uri = Uri;
	}
	
public void getData()
{
	try {
	HttpClient hC = new DefaultHttpClient();
	HttpGet request = new HttpGet();	
	URI website = new URI(Uri);
	
	HttpResponse response = hC.execute(request);
	
	in = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));
	
	String line = in.readLine();
	
	//return line;
	
	} catch (URISyntaxException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
		//return null;
	} catch (ClientProtocolException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
		//return null;
	} catch (IOException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
		//return null;
	}
	
	

}

}
