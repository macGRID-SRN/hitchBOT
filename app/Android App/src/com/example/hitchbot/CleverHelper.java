package com.example.hitchbot;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

import com.cleverscript.android.CleverscriptAPI;

public class CleverHelper  {
	
	public CleverscriptAPI cs;
	
	public CleverHelper(String db, String APIkey, MainActivity context)
	{
		cs = new CleverscriptAPI(context);
		cs.setLocation(db);
		cs.setApiKey(APIkey);
		cs.setDebugLevel(4);
	    cs.loadDatabase();
	    loadVariables();
	}

	public void loadVariables()
	{
		CleverVariables cV = new CleverVariables();
		cs.assignVariable("weather_temperatureC", (String) cV.data.get("weather_temperatureC"));
		cs.assignVariable("weather_temperatureK", (String) cV.data.get("weather_temperatureK"));
		cs.assignVariable("current_city_name", (String) cV.data.get("current_city_name"));
		cs.assignVariable("current_city_mayor_name", (String) cV.data.get("current_city_mayor_name"));
		cs.assignVariable("current_city_population",(String) cV.data.get("current_city_population"));
		cs.assignVariable("local_time", (String) cV.data.get("local_time"));
		cs.assignVariable("local_time_intr", (String) cV.data.get("local_time_intr"));
		cs.assignVariable("local_date", (String) cV.data.get("local_date"));
		cs.assignVariable("next_city_distance", (String) cV.data.get("next_city_distance"));
		cs.assignVariable("next_city_time_minutes", (String) cV.data.get("next_city_time_minutes"));
		cs.assignVariable("next_city_time_hours", (String) cV.data.get("next_city_time_hours"));
		cs.assignVariable("starting_date", (String) cV.data.get("starting_date"));
		cs.assignVariable("starting_city", (String) cV.data.get("starting_city"));
		cs.assignVariable("last_three_cities", (String) cV.data.get("last_three_cities"));
		cs.assignVariable("last_opinion", (String) cV.data.get("last_opinion"));
		cs.assignVariable("current_velocity", (String) cV.data.get("current_velocity"));
		cs.assignVariable("maximum_velocty", (String) cV.data.get("maximum_velocty"));
		cs.assignVariable("local_sunrise_time", (String) cV.data.get("local_sunrise_time"));
		cs.assignVariable("local_sunset_time", (String) cV.data.get("local_sunset_time"));
		cs.assignVariable("local_humidity", (String) cV.data.get("local_humidity"));
		cV.getVariablesFromServer();

	}
	
	public void getInformationForVariables(String jsonString)
	{
		if(jsonString != null)
		{
		try
		{
			JSONObject obj = new JSONObject(jsonString);
			JSONArray jO = obj.getJSONArray("data");

			for(int j = 0 ; j <= obj.length() ; j ++)
			{
				JSONObject jobj = jO.getJSONObject(j);
				
				cs.assignVariable((String) jobj.getString("key"), (String) jobj.getString("value"));
				Log.i("HTTPGET", (String) jobj.getString("key" + " "));
				Log.i("HTTPGET", (String) jobj.getString("value" + " "));

			}
		}
		catch(Exception e)
		{
			//stuff
		}
		}
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
