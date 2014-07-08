package com.example.hitchbot;

import com.cleverscript.android.CleverscriptAPI;

import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
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
		//example how to do it
	//	cs.assignVariable("city_name", variableValue);
	//	cs.assignVariable("city_population", variableValue);
		
	}
	
	public void getInformationForVariables()
	{
		//get temp, city, population, etc..
	}
	
	public String getBotState()
	{
		return cs.retrieveBotState();
	}
	


}
