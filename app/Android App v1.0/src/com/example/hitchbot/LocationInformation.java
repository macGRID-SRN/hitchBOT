package com.example.hitchbot;

import android.content.Context;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;

public class LocationInformation{
Context context;
String locationProvider;
boolean useGPS;
LocationManager locationManager;
Handler handler;
Runnable runnable;
double latitude;
double longitude;
double altitude;
double accuracy;
double speed;
boolean haveLocation = false;
boolean isFine = false;
String TAG = "LocationFinderClass";

public LocationInformation(Context context)
{
	this.context = context;
}

public void setupProvider()
{
	locationManager = (LocationManager)context.getSystemService(Context.LOCATION_SERVICE); 
	Location location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
	if(location != null)
	{
		speed = location.getSpeed();
		altitude = location.getAltitude();
		latitude = location.getLatitude();
		longitude = location.getLongitude();
		accuracy = location.getAccuracy();
		isFine = true;
		haveLocation = true;
		Log.i(TAG, "gps info got");
		postFineLocation();
	}
	else
	{
		location = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
		if(location != null)
		{
			latitude = location.getLatitude();
			longitude = location.getLongitude();
			haveLocation = true;
			postCourseLocation();
			locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, locationListenerGPS);
			locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, locationListenerNETWORK);

		}
		else
		{
			locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 0, locationListenerGPS);
			locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 0, locationListenerNETWORK);

		}
	}
}



private void postFineLocation()
{
	String url1 = "http://hitchbotapi.azurewebsites.net/api/Location?HitchBotID=%s&Latitude=%s&Longitude=%s&Altitude=%s&Accuracy=%s&Velocity=%s&TakenTime=%s";
	String hitchBOTid = Config.HITCHBOT_ID;
	String sLatitude = String.valueOf(latitude);
	String sLongitude = String.valueOf(longitude);
	String sAltitude = String.valueOf(altitude);
	String Accuracy = String.valueOf(accuracy);
	String sSpeed = String.valueOf(speed);
	String timeStamp = Config.getUtcDate();
	
	HttpServerPost  hSp = new HttpServerPost(String.format(url1,hitchBOTid, sLatitude, sLongitude,
			sAltitude, Accuracy, sSpeed, timeStamp), context);

	hSp.execute(hSp);		
}

private void postCourseLocation()
{
	String url1 = "http://hitchbotapi.azurewebsites.net/api/Location?HitchBotID=%s&Latitude=%s&Longitude=%s&TakenTime=%s";
	String hitchBOTid = Config.HITCHBOT_ID;
	String sLatitude = String.valueOf(latitude);
	String sLongitude = String.valueOf(longitude);
	String timeStamp = Config.getUtcDate();
	
	HttpServerPost  hSp = new HttpServerPost(String.format(url1,hitchBOTid, sLatitude, sLongitude,
			 timeStamp), context);

	hSp.execute(hSp);		
}


LocationListener locationListenerGPS = new LocationListener()
{

	@Override
	public void onLocationChanged(Location location) {
		speed = location.getSpeed();
		latitude = location.getLatitude();
		longitude = location.getLongitude();
		altitude = location.getAltitude();
		accuracy = location.getAccuracy();
		haveLocation = true;
		isFine = true;
		locationManager.removeUpdates(locationListenerGPS);
		locationManager.removeUpdates(locationListenerNETWORK);
		postFineLocation();

	}
	

	@Override
	public void onStatusChanged(String provider, int status, Bundle extras) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onProviderEnabled(String provider) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onProviderDisabled(String provider) {
		// TODO Auto-generated method stub
		
	}
	
};

LocationListener locationListenerNETWORK = new LocationListener()
{

	@Override
	public void onLocationChanged(Location location) {

		latitude = location.getLatitude();
		longitude = location.getLongitude();

		haveLocation = true;
		isFine = false;
		locationManager.removeUpdates(locationListenerNETWORK);
		locationManager.removeUpdates(locationListenerGPS);
		postCourseLocation();

	}
	

	@Override
	public void onStatusChanged(String provider, int status, Bundle extras) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onProviderEnabled(String provider) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onProviderDisabled(String provider) {
		// TODO Auto-generated method stub
		
	}
	
};




}
