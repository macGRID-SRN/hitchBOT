package com.example.hitchbot;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.Scanner;

import org.apache.http.HttpRequest;
import org.apache.http.NameValuePair;
import org.apache.http.client.utils.URIUtils;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONObject;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

public class UploadImageImgur extends AsyncTask<Void, Void, String> {

		private static final String TAG = UploadImageImgur.class.getSimpleName();
		private static final String clientID = "2d90150ad211086";
		private static final String clientSecret = "8b2966ce25695fe1796b1916b2a5c0aa10de098e";
		private static final String UPLOAD_URL = "https://api.imgur.com/3/image";
		private Uri image; 
		private Activity activity;
		private Context context;
	   
		//Uploads image anonymously

		
	public UploadImageImgur(Uri image, Activity activity, Context context)
	{
		this.image = image;
		this.activity = activity;
		this.context = context;
	}
	
		@Override
		protected void onPostExecute(String url)
		{
			if (url != null)
			{
				
				String url1 = "http://hitchbotapi.azurewebsites.net/api/Image?HitchBotID=%s&timeTaken=%s&URL=%s";
				String timeStamp = Config.getUtcDate();
				String hitchBOTid = Config.HITCHBOT_ID;
				String URL = url;
				
				HttpServerPost  hSp = new HttpServerPost(String.format(url1,hitchBOTid, timeStamp, URL), context);

				hSp.execute(hSp);
				
				boolean deleted = new File(image.getPath()).delete();
				Log.i("FileDeleted",String.valueOf(deleted));
			}
			else
			{

				DatabaseQueue dQ = new DatabaseQueue(context);	
				
				HttpPostDb hPd = new HttpPostDb(image.toString(),0, 2);
				dQ.addItemToQueue(hPd);

				Toast.makeText(activity, url, Toast.LENGTH_SHORT).show();
			}

		}

		@Override
		protected String doInBackground(Void... params) {
			InputStream imageIn;
			try {
				imageIn = activity.getContentResolver().openInputStream(image);
			} catch (FileNotFoundException e) {
				Log.e(TAG, "could not open InputStream", e);
				return null;
			}

	        HttpURLConnection conn = null;
	        InputStream responseIn = null;

			try {
	            conn = (HttpURLConnection) new URL(UPLOAD_URL).openConnection();
	            conn.addRequestProperty("Authorization","Client-ID " + clientID);

	            conn.setDoOutput(true);
	            OutputStream out = conn.getOutputStream();
	            copy(imageIn, out);
	            out.flush();
	            out.close();

	            if (conn.getResponseCode() == HttpURLConnection.HTTP_OK) {
	                responseIn = conn.getInputStream();
	                return onInput(responseIn);
	            }
	            else {
	                Log.i(TAG, "responseCode=" + conn.getResponseCode());
	                responseIn = conn.getErrorStream();
	                StringBuilder sb = new StringBuilder();
	                Scanner scanner = new Scanner(responseIn);
	                while (scanner.hasNext()) {
	                    sb.append(scanner.next());
	                }
	                scanner.close();
	                Log.i(TAG, "error response: " + sb.toString());
	                return null;
	            }
			} catch (Exception ex) {
				Log.e(TAG, "Error during POST", ex);
				return null;
			} finally {
	            try {
	                responseIn.close();
	            } catch (Exception ignore) {}
	            try {
	                conn.disconnect();
	            } catch (Exception ignore) {}
				try {
					imageIn.close();
				} catch (Exception ignore) {}
			}
		}
		
		
	
		   private static int copy(InputStream input, OutputStream output) throws IOException {
		        byte[] buffer = new byte[8192];
		        int count = 0;
		        int n = 0;
		        while (-1 != (n = input.read(buffer))) {
		            output.write(buffer, 0, n);
		            count += n;
		        }
		        return count;
		    }
		
		protected String onInput(InputStream in) throws Exception {
	        StringBuilder sb = new StringBuilder();
	        Scanner scanner = new Scanner(in);
	        while (scanner.hasNext()) {
	            sb.append(scanner.next());
	        }
	        scanner.close();
	        JSONObject root = new JSONObject(sb.toString());
	        String id = root.getJSONObject("data").getString("id");
	        String deletehash = root.getJSONObject("data").getString("deletehash");

			Log.i(TAG, "new imgur url: http://imgur.com/" + id + " (delete hash: " + deletehash + ")");
			return id;
		}
	  

}
