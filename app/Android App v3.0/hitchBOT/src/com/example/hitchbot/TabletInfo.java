package com.example.hitchbot;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import Models.HttpPostDb;
import TransitionModels.TabletPack;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.BatteryManager;
import android.util.Log;

public class TabletInfo {
	
	private static String TAG = "TabletInformation";
	private static IntentFilter iFilter;
	private static Intent batteryStatus;
	private double batteryPercent;
	private boolean isCharging;
	private double voltage;
	private double temp;
	
	public TabletInfo()
	{
		this.iFilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
		this.batteryStatus = Config.context.registerReceiver(null, iFilter);
		this.batteryPercent = getBatteryLevel();
		this.isCharging = isCharging();
		this.voltage = getBatteryVoltage();
		this.temp = getBatteryTemp();
	}
	
	private static double getBatteryLevel()
	{	
		int level = batteryStatus.getIntExtra(BatteryManager.EXTRA_LEVEL, -1);
		int scale = batteryStatus.getIntExtra(BatteryManager.EXTRA_SCALE, -1);
		return level/ (double) scale;
	}

	private static boolean isCharging()
	{
		int status = batteryStatus.getIntExtra(BatteryManager.EXTRA_STATUS, -1);
		boolean isCharging = status == BatteryManager.BATTERY_STATUS_CHARGING ||
				status == BatteryManager.BATTERY_STATUS_FULL;
		return isCharging;
	}

	private static double getBatteryVoltage()
	{
		int voltage = batteryStatus.getIntExtra(BatteryManager.EXTRA_VOLTAGE, -1);
		Log.i(TAG, String.valueOf(voltage));
		return voltage / 1000.0;
	}

	private static double getBatteryTemp()
	{
		int temp = batteryStatus.getIntExtra(BatteryManager.EXTRA_TEMPERATURE, -1);
		return temp/ 10.0;
	}


	public void queueBatteryUpdates()
	{

		TabletPack tabPack = new TabletPack(temp, voltage, isCharging, batteryPercent);
		
		HttpPostDb postDb = new HttpPostDb(Config.batteryPOST, 0,null, tabPack.toJson(), 4);
		Config.dQ.addItemToQueue(postDb);
	}

}
