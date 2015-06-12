package Speech;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONObject;

import Models.HttpPostDb;
import TransitionModels.SpeechPack;
import android.net.Uri;
import android.util.Log;

import com.cleverscript.android.CleverscriptAPI;
import com.example.hitchbot.Config;
import com.example.hitchbot.Activities.SpeechActivity;

public class CleverScriptHelper {

	// This class should be responsible to logging input and output to db

	public CleverscriptAPI cs;
	private static String TAG = "CleverScriptHelper";
	private SpeechOut speechOut;

	public CleverScriptHelper(String db, String APIkey) {
		cs = new CleverscriptAPI(Config.context);
		cs.setLocation(db);
		cs.setApiKey(APIkey);
		cs.setDebugLevel(4);
		cs.loadDatabase();
		//loadVariables();
	}

	public void setSpeechOut(SpeechOut speechOut) {
		this.speechOut = speechOut;
	}

	//temporary defaults for testing purposes!
	public void loadVariables() {
		//cs.assignVariable("current_velocity", "2");
	//	cs.assignVariable("weather_status", "fog");
	//	cs.assignVariable("weather_temperatureC", "999");
	//	cs.assignVariable("localtime", "100");
	//	cs.assignVariable("current_city_name", "Toronto");
	//	cs.assignVariable("trip", "netherlands");
	}

	public void sendCleverScriptResponse(String message, double rmsDbLevel,
			float accuracy, int recognizer) {
		String output = getResponseFromCleverScript(message);
				
		int cleverAccuracy = 0;
		try{
			cleverAccuracy = Integer.parseInt(getAccuracy());
		}catch(Exception e){
			//do nothing
		}

		SpeechPack speechPack  = new SpeechPack(output, message, getRecentOutputLabel(),
				cleverAccuracy, 0, rmsDbLevel);
		
		HttpPostDb httpPost = new HttpPostDb(Config.conversationPost, 0,null, speechPack.toJson(), 3);
		Config.dQ.addItemToQueue(httpPost);
		speechOut.Speak(output, isSpanishPhrase());
	}

	private boolean isSpanishPhrase(){
		return getRecentOutputLabel().startsWith("es_");
	}

	
	public String getResponseFromCleverScript(String message) {
		Log.i(TAG, message);
		return cs.sendMessage(message);
	}

	public String getRecentInput() {
		return cs.retrieveVariable("input");
	}

	public String getRecentOutputLabel() {
		return cs.retrieveVariable("output_label");
	}


	public void getInformationForVariables(String jsonString) {
		if (jsonString != null) {
			try {
				JSONObject obj = new JSONObject(jsonString);
				JSONArray jO = obj.getJSONArray("data");

				for (int j = 0; j < jO.length(); j++) {
					JSONObject jobj = jO.getJSONObject(j);

					//NameValuePair nvP = new BasicNameValuePair(
					//		jobj.getString("key"),
					//		(String) jobj.getString("value"));
					//Config.cleverPair.add(nvP);

					cs.assignVariable((String) jobj.getString("key"),
							(String) jobj.getString("value"));
					Log.i(TAG, (String) jobj.getString("key") + " ");
					Log.i(TAG, (String) jobj.getString("value") + " ");

				}
			} catch (Exception e) { 
			}
		}
	}

	public String getBotState() {
		return cs.retrieveBotState();
	}

	public void loadBotState(String state) {
		cs.assignBotState(state);
	}

	public String getAccuracy() {
		return cs.retrieveVariable("accuracy");
	}

	public String getAudio_on() {
		return cs.retrieveVariable("audio_on");
	}

	public String getClever_data() {
		return cs.retrieveVariable("cleverdata_on");
	}

}
