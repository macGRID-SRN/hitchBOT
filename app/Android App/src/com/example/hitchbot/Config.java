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

public static String getUtcDate()
{
	final Date date = new Date();
	final SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
	final TimeZone utc = TimeZone.getTimeZone("UTC");
	sdf.setTimeZone(utc);
	return sdf.format(date);
}
	

}
