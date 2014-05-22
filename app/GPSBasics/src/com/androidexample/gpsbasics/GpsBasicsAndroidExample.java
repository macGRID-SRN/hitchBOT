package com.androidexample.gpsbasics;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.Date;

import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.widget.Toast;
import android.app.Activity;
import android.content.Context;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.*;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

public class GpsBasicsAndroidExample extends Activity implements LocationListener {

	private LocationManager locationManager;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_gps_basics_android_example);
		StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
		StrictMode.setThreadPolicy(policy);
		
		/********** get Gps location service LocationManager object ***********/
		locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		
		/*
		  Parameters :
		     First(provider)    :  the name of the provider with which to register 
		     Second(minTime)    :  the minimum time interval for notifications, in milliseconds. This field is only used as a hint to conserve power, and actual time between location updates may be greater or lesser than this value. 
		     Third(minDistance) :  the minimum distance interval for notifications, in meters 
		     Fourth(listener)   :  a {#link LocationListener} whose onLocationChanged(Location) method will be called for each location update 
        */
		
		locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER,
				50000,   // 3 sec
				40, this);
		
		/********* After registration onLocationChanged method called periodically after each 3 sec ***********/
	}
	
	/************* Called after each 3 sec **********/
	@Override
	public void onLocationChanged(Location location) {
		   
		Date date = new Date();
		HttpClient httpclient = new DefaultHttpClient();
		HttpPost httppost = new HttpPost("http://hitchbotapi.azurewebsites.net/api/Location?HitchBot=5&Latitude=" + location.getLatitude() + "&Longitude=" +location.getLongitude() + "&TakenTime=" +new SimpleDateFormat("yyyyMMddHHmmss").format(date));
		
		// Request parameters and other properties.
		//List<NameValuePair> params = new ArrayList<NameValuePair>(2);
		//params.add(new BasicNameValuePair("HitchBotID", "3"));
		//params.add(new BasicNameValuePair("Latitude", String.valueOf((int)location.getLatitude()*100000)));
		//params.add(new BasicNameValuePair("Longitude", String.valueOf((int)location.getLongitude()*100000)));
		//params.add(new BasicNameValuePair("TakenTime", new SimpleDateFormat("yyyyMMddHHmmss").format(date)));
		
		Toast.makeText(getBaseContext(), new SimpleDateFormat("yyyyMMddHHmmss").format(date), Toast.LENGTH_LONG).show();
		
		

		//Execute and get the response.
		try {
			HttpResponse response = httpclient.execute(httppost);
		} catch (ClientProtocolException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		String str = "Latitude: "+location.getLatitude()+" \nLongitude: "+location.getLongitude();
		//Toast.makeText(getBaseContext(), str, Toast.LENGTH_LONG).show();
	}

	/*@SuppressWarnings("deprecation")
	public String buildDateTime(Date date){
		
		return String.valueOf(date.getYear() + 1900) + String.valueOf(date.getMonth());
	}*/
	
	@Override
	public void onProviderDisabled(String provider) {
		
		/******** Called when User off Gps *********/
		
		Toast.makeText(getBaseContext(), "Gps turned off ", Toast.LENGTH_LONG).show();
	}

	@Override
	public void onProviderEnabled(String provider) {
		
		/******** Called when User on Gps  *********/
		
		Toast.makeText(getBaseContext(), "Gps turned on ", Toast.LENGTH_LONG).show();
	}

	@Override
	public void onStatusChanged(String provider, int status, Bundle extras) {
		// TODO Auto-generated method stub
		
	}
}
