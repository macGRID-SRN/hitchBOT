package com.example.hitchbot.Speech;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

import com.cleverscript.android.CleverscriptAPI;
import com.example.hitchbot.Config;

public class CleverScriptHelper {

	public CleverscriptAPI cs;
	private static String TAG = "CleverScriptHelper";
	
	public CleverScriptHelper(String db, String APIkey)
	{
		cs = new CleverscriptAPI(Config.context);
		cs.setLocation(db);
		cs.setApiKey(APIkey);
		cs.setDebugLevel(4);
	    cs.loadDatabase();
	    loadVariables();
	}

	public void loadVariables()
	{

		//TODO Load default variables
	}
	
	public String getResponseFromCleverScript(String message)
	{
		return cs.sendMessage(message);
	}
	
	public void getInformationForVariables(String jsonString)
	{
		if(jsonString != null)
		{
		try
		{
			JSONObject obj = new JSONObject(jsonString);
			JSONArray jO = obj.getJSONArray("data");

			for(int j = 0 ; j < jO.length() ; j ++)
			{
				JSONObject jobj = jO.getJSONObject(j);
				
				cs.assignVariable((String) jobj.getString("key"), (String) jobj.getString("value"));
				Log.i(TAG, (String) jobj.getString("key") + " ");
				Log.i(TAG, (String) jobj.getString("value") + " ");

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
	
	public String getAudio_on()
	{
		return cs.retrieveVariable("audio_on");
	}
	
	public String getClever_data()
	{
		return cs.retrieveVariable("cleverdata_on");
	}

	
}