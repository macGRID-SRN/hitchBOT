package com.example.hitchbot;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import android.content.Intent;
import android.content.IntentFilter;
import android.os.BatteryManager;

public final class Config {
	
public static MainActivity context = null;
public static int THRESHHOLD_ACCURACY = 40;
public static String HITCHBOT_ID = "1";
public static int HOUR = 1000*60*60;
public static int FIFTEEN_MINUTES = 1000*60*15;
public static int HALF_HOUR = 1000*60*30;
public static int TEN_MINUTES = 1000*60*30;
public static int THREE_HOURS = 1000*60*60*3;
public static CleverHelper cH = null;

public static String getUtcDate()
{
	final Date date = new Date();
	final SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
	final TimeZone utc = TimeZone.getTimeZone("UTC");
	sdf.setTimeZone(utc);
	return sdf.format(date);
}
	

}
