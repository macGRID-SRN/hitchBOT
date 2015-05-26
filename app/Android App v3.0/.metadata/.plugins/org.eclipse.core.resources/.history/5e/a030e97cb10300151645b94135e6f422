package com.example.hitchbot;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
import java.util.TimeZone;
import Models.DatabaseConfig;
import Speech.CleverScriptHelper;
import android.app.Activity;
import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

public final class Config {
	
	public static Activity context = null;
	public static DatabaseConfig dQ = null;
	//I would use a resource for these, but they don't play nicely with strings.xml, and the api
	//will change drastically soon anyways
	public static String conversationPost = "http://hitchbotapi.azurewebsites.net/api/Conversation?HitchBotID=%s&SpeechSaid=%s&SpeechHeard=%s&TimeTaken=%s&Person=%s&Notes=%s&MatchedLineLabel=%s&MatchAccuracy=%s&RmsDecibelLevel=%s&EnvironmentType=%s&RecognitionScore=%s&ResponseScore=%s&RecognizerEnum=%s";
	public static String exceptionPOST = "http://hitchbotapi.azurewebsites.net/api/Exception?HitchBotID=%s&Message=%s&TimeOccured=%s";
	public static String locationPOST_FINE = "http://hitchbotapi.azurewebsites.net/api/Location?HitchBotID=%s&Latitude=%s&Longitude=%s&Altitude=%s&Accuracy=%s&Velocity=%s&TakenTime=%s";
	public static String locationPOST_COURSE = "http://hitchbotapi.azurewebsites.net/api/Location?HitchBotID=%s&Latitude=%s&Longitude=%s&TakenTime=%s";
	public static String batteryPOST = "http://hitchbotapi.azurewebsites.net/api/Tablet?HitchBotID=%s&timeTaken=%s&isPluggedIn=%s&BatteryVoltage=%s&BatteryPercent=%s&BatteryTemp=%s";
	public static String imagePOST = "http://hitchbotapi.azurewebsites.net/api/Image?HitchBotID=%s&timeTaken=%s";
	public static String audioPOST = "http://hitchbotapi.azurewebsites.net/api/hitchBOT";
	public static String cleverGET = "http://hitchbotapi.azurewebsites.net/api/hitchBOT";

	public static String name = "";
	public static String searchName = "searchName";
	public static final int ID = 15; //14-18
	public static String specInfo = "";
	public static CleverScriptHelper csh = null;
	public static int HOUR = 1000 * 60 * 60;
	public static int FIVE_MINUTES = 1000 * 60 * 5;
	public static int FIFTEEN_MINUTES = 1000 * 60 * 15;
	public static int TWENTY_SECONDS = 1000 * 20;
	public static int HALF_HOUR = 1000 * 60 * 30;
	public static int TEN_MINUTES = 1000 * 60 * 30;
	public static int THREE_HOURS = 1000 * 60 * 60 * 3;
	public static int THIRTY_SECONDS = 1000 * 30;
	public static int ONE_MINUTE = 1000 * 60;
	public static int FORTYFIVE_MINUTES = 1000 * 60 * 45;
	private static final long TICKS_AT_EPOCH = 621355968000000000L;
	private static final long TICKS_PER_MILLISECOND = 10000;
	
	public static String getUtcDate() {
		final Date date = new Date();
		final SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss",
				Locale.CANADA);
		final TimeZone utc = TimeZone.getTimeZone("UTC");
		sdf.setTimeZone(utc);
		return sdf.format(date);
	}
	
	public static String millis2UtcDate(long millis)
	{
		final Date date=new Date(millis);
		final SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss",
				Locale.CANADA);
		final TimeZone utc = TimeZone.getTimeZone("UTC");
		sdf.setTimeZone(utc);
		return sdf.format(date);
	}

	public static boolean networkAvailable() {
		ConnectivityManager connectivityManager = (ConnectivityManager) Config.context
				.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo activeNetworkInfo = connectivityManager
				.getActiveNetworkInfo();
		return activeNetworkInfo != null && activeNetworkInfo.isConnected();
	}

	public static long convertTicksToMillis(String ticks) {
		long tick;
		try {
			tick = Long.parseLong(ticks);
		} catch (Exception e) {
			tick = System.currentTimeMillis();
		}
		Date date = new Date((tick - TICKS_AT_EPOCH) / TICKS_PER_MILLISECOND);
		// System.out.println(date);

		TimeZone utc = TimeZone.getTimeZone("UTC");
		Calendar calendar = Calendar.getInstance(utc);
		calendar.setTime(date);
		return calendar.getTimeInMillis();
	}
}
