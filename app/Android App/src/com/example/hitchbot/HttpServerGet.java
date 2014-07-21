package com.example.hitchbot;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URI;
import java.net.URISyntaxException;

import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONObject;

import android.os.AsyncTask;
import android.util.Log;

public class HttpServerGet extends AsyncTask<String, String, String>{
	
	BufferedReader in = null;
	
	public HttpServerGet()
	{
	}
	

@Override
protected String doInBackground(String... uri) {
	HttpClient httpclient = new DefaultHttpClient();
    HttpResponse response;
    String responseString = null;
    try {
        response = httpclient.execute(new HttpGet(uri[0]));
        StatusLine statusLine = response.getStatusLine();
        if(statusLine.getStatusCode() == HttpStatus.SC_OK){
            ByteArrayOutputStream out = new ByteArrayOutputStream();
            response.getEntity().writeTo(out);
            out.close();
            responseString = out.toString();
            return responseString;

        } else{
            //Closes the connection.
            response.getEntity().getContent().close();
        }
    } catch (IOException e) {
        return null;
    }
   
	return responseString;
}

@Override
protected void onPostExecute(String result) {
    super.onPostExecute(result);
	Log.i("HTTPGET", result + " ");
    Config.cH.getInformationForVariables(result);
											}

}
