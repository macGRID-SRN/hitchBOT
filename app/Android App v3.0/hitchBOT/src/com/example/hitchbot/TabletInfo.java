package com.example.hitchbot;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import Models.HttpPostDb;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.BatteryManager;
import android.util.Log;

public class TabletInfo {
	
	private static String TAG = "TabletInformation";
	private static IntentFilter iFilter;
	private static Intent batteryStatus;
	private String batteryPercent;
	private String isCharging;
	private String voltage;
	private String temp;
	
	public TabletInfo()
	{
		this.iFilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
		this.batteryStatus = Config.context.registerReceiver(null, iFilter);
		this.batteryPercent = getBatteryLevel();
		this.isCharging = isCharging();
		this.voltage = getBatteryVoltage();
		this.temp = getBatteryTemp();
	}
	
	private static String getBatteryLevel()
	{	
		int level = batteryStatus.getIntExtra(BatteryManager.EXTRA_LEVEL, -1);
		int scale = batteryStatus.getIntExtra(BatteryManager.EXTRA_SCALE, -1);
		return String.valueOf(level/ (float) scale);
	}

	private static String isCharging()
	{
		int status = batteryStatus.getIntExtra(BatteryManager.EXTRA_STATUS, -1);
		boolean isCharging = status == BatteryManager.BATTERY_STATUS_CHARGING ||
				status == BatteryManager.BATTERY_STATUS_FULL;
		return String.valueOf(isCharging);
	}

	private static String getBatteryVoltage()
	{
		int voltage = batteryStatus.getIntExtra(BatteryManager.EXTRA_VOLTAGE, -1);
		Log.i(TAG, String.valueOf(voltage));
		return String.valueOf(voltage / 1000.0);
	}

	private static String getBatteryTemp()
	{
		int temp = batteryStatus.getIntExtra(BatteryManager.EXTRA_TEMPERATURE, -1);
		return String.valueOf( temp/ 10.0);
	}

	public String getBatteryPercent() {
		return batteryPercent;
	}


	public String getIsCharging() {
		return isCharging;
	}



	public String getVoltage() {
		return voltage;
	}



	public String getTemp() {
		return temp;
	}

	public void queueBatNameValuePair()
	{
		List<NameValuePair> nvp = new ArrayList<NameValuePair>();
		nvp.add(new BasicNameValuePair("BatteryTemp",temp));
		nvp.add(new BasicNameValuePair("BatteryVoltage",voltage));
		nvp.add(new BasicNameValuePair("IsCharging",isCharging));
		nvp.add(new BasicNameValuePair("BatteryPercentage",batteryPercent));

		//String uri = String.format(Config.batteryPOST,Config.ID, timeTaken,
		//		isCharging, voltage, batteryPercent, temp);
		HttpPostDb postDb = new HttpPostDb(Config.batteryPOST, 0,null, nvp, 4);
		Config.dQ.addItemToQueue(postDb);
	}

}
