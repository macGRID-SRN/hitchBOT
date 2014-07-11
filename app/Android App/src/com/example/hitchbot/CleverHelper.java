package com.example.hitchbot;

import com.cleverscript.android.CleverscriptAPI;

import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
import android.util.Log;
import android.widget.Toast;

public class CleverHelper  {
	
	public CleverscriptAPI cs;
	
	public CleverHelper(String db, String APIkey, MainActivity context)
	{
		cs = new CleverscriptAPI(context);
		cs.setLocation(db);
		cs.setApiKey(APIkey);
		cs.setDebugLevel(4);
	    cs.loadDatabase();
	}

	public void loadVariables()
	{
		CleverVariables cV = new CleverVariables();
		cs.assignVariable("weather_temperatureC", cV.weather_temperatureC);
		cs.assignVariable("weather_temperatureK", cV.weather_temperatureK);
		cs.assignVariable("current_city_name", cV.current_city_name);
		cs.assignVariable("current_city_mayor_name", cV.current_city_mayor_name);
		cs.assignVariable("current_city_population", cV.current_city_population);
		cs.assignVariable("local_time", cV.local_time);
		cs.assignVariable("local_time_intr", cV.local_time_intr);
		cs.assignVariable("local_date", cV.local_date);
		cs.assignVariable("next_city_distance", cV.next_city_distance);
		cs.assignVariable("next_city_time_minutes", cV.next_city_time_minutes);
		cs.assignVariable("next_city_time_hours", cV.next_city_time_hours);
		cs.assignVariable("starting_date", cV.starting_date);
		cs.assignVariable("starting_city", cV.starting_city);
		cs.assignVariable("last_three_cities", cV.last_three_cities);
		cs.assignVariable("last_opinion", cV.last_opinion);
		cs.assignVariable("current_velocity", cV.current_velocity);
		cs.assignVariable("maximum_velocty", cV.maximum_velocty);
		cs.assignVariable("local_sunrise_time", cV.local_sunrise_time);
		cs.assignVariable("local_sunset_time", cV.local_sunset_time);
		cs.assignVariable("local_humidity", cV.local_humidity);


	}
	
	public void getInformationForVariables()
	{
		//get temp, city, population, etc..
	}
	
	public String getBotState()
	{
		return cs.retrieveBotState();
	}
	
	public void loadBotState(String state)
	{
		cs.assignBotState(state);
	}

	public String getAccuracy()
	{
		return cs.retrieveVariable("accuracy");
	}
	
	

}
