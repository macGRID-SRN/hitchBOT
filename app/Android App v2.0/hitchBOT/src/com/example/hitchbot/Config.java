package com.example.hitchbot;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

public final class Config {
	
	public static Context context = null;
	public static String HITCHBOT_ID = "3";
	public static int HOUR = 1000*60*60;
	public static int FIFTEEN_MINUTES = 1000*60*15;
	public static int HALF_HOUR = 1000*60*30;
	public static int TEN_MINUTES = 1000*60*30;
	public static int THREE_HOURS = 1000*60*60*3;
	public static int THIRTY_SECONDS = 1000 * 30;
	
	public static String getUtcDate()
	{
		final Date date = new Date();
		final SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
		final TimeZone utc = TimeZone.getTimeZone("UTC");
		sdf.setTimeZone(utc);
		return sdf.format(date);
	}
	
	public static boolean networkAvailable()
	{
		ConnectivityManager connectivityManager = (ConnectivityManager)Config.context.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
		return activeNetworkInfo != null && activeNetworkInfo.isConnected();
	}
}