package com.example.hitchbot;

import android.content.Intent;
import android.content.IntentFilter;
import android.os.BatteryManager;
import android.os.Handler;
import android.util.Log;

public class BatteryInformation {

	private static IntentFilter iFilter;
	private static Intent batteryStatus;
	String batPercent;
	String isCharging;
	String voltage;
	String temp;
	static Handler batteryUploadHandler;
	
	public BatteryInformation()
	{
		iFilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
		batteryStatus = Config.context.registerReceiver(null, iFilter);
		batPercent = getBatteryLevel();
		isCharging = isCharging();
		voltage = getBatteryVoltage();
		temp = getBatteryTemp();
	}
	
	public static String getBatteryLevel()
	{	
		int level = batteryStatus.getIntExtra(BatteryManager.EXTRA_LEVEL, -1);
		int scale = batteryStatus.getIntExtra(BatteryManager.EXTRA_SCALE, -1);
		return String.valueOf(level/ (float) scale);
	}

	public static String isCharging()
	{
		int status = batteryStatus.getIntExtra(BatteryManager.EXTRA_STATUS, -1);
		boolean isCharging = status == BatteryManager.BATTERY_STATUS_CHARGING ||
				status == BatteryManager.BATTERY_STATUS_FULL;
		return String.valueOf(isCharging);
	}

	public static String getBatteryVoltage()
	{
		int voltage = batteryStatus.getIntExtra(BatteryManager.EXTRA_VOLTAGE, -1);
		Log.i("PostGeneralUpdates", String.valueOf(voltage));
		return String.valueOf(voltage / 1000.0);
	}

	public static String getBatteryTemp()
	{
		int temp = batteryStatus.getIntExtra(BatteryManager.EXTRA_TEMPERATURE, -1);
		return String.valueOf( temp/ 10.0);
	}
	
	public void uploadBatteryInformation()
	{
		batteryUploadHandler = new Handler();
		
		batteryUploadHandler.post(new Runnable()
		{

			@Override
			public void run() 
			{
				String timeTaken = Config.getUtcDate();	
				String url = "http://hitchbotapi.azurewebsites.net/api/Tablet?HitchBotID=%s&timeTaken=%s&isPluggedIn=%s&BatteryVoltage=%s&BatteryPercent=%s&BatteryTemp=%s";
			
				HttpServerPost  hSp = new HttpServerPost(String.format(url,Config.HITCHBOT_ID, timeTaken,
						isCharging, voltage, batPercent, temp), Config.context);

				hSp.execute(hSp);
			}
			
		});
	}
}
