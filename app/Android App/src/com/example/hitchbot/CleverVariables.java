package com.example.hitchbot;

import java.util.HashMap;

import android.os.Handler;


public class CleverVariables {
	public String weather_temperatureC;
	public String weather_temperatureK;
	public String current_city_name;
	public String current_city_mayor_name;
	public String current_city_population;
	public String local_time;
	public String local_time_intr;
	public String local_date;
	public String next_city_distance;
	public String next_city_time_minutes;
	public String next_city_time_hours;
	public String starting_date;
	public String starting_city;
	public String last_three_cities;
	public String last_opinion;
	public String current_velocity;
	public String maximum_velocty;
	public String local_sunrise_time;
	public String local_sunset_time;
	public String local_humidity;
	
	
	HashMap<String,Object> data =new HashMap<String,Object>();
	
	public CleverVariables()
	{
		data.put("weather_temperatureC", "50");
		data.put("weather_temperatureK", "1337");
		data.put("current_city_name", "Victoria");
		data.put("current_city_mayor_name", "Rob Ford");
		data.put("current_city_population", "300000");
		data.put("local_time", "6:00");
		data.put("local_time_intr", "6:00");
		data.put("local_date", "July 27th");
		data.put("next_city_distance", "3");
		data.put("next_city_time_minutes", "3");
		data.put("next_city_time_hours", "3");
		data.put("starting_date", "3");
		data.put("starting_city", "Halifax");
		data.put("last_three_cities", "Halifax");
		data.put("last_opinion", "Canada is the best country in the world");
		data.put("current_velocity", "0");
		data.put("maximum_velocty", "15");
		data.put("local_sunrise_time", "5:00");
		data.put("local_sunset_time", "8:30");
		data.put("local_humidity", "15");
	}
	
	public void getVariablesFromServer()
	{
		
		new HttpServerGet().execute("http://hitchbotapi.azurewebsites.net/api/hitchBOT");
		
	}
}
