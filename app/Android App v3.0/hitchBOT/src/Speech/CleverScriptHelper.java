package Speech;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

import com.cleverscript.android.CleverscriptAPI;
import com.example.hitchbot.Config;

public class CleverScriptHelper {

	//This class should be responsible to logging input and output to db
	
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

	/*
	 * public void loadVariables() { // cs.assignVariable("test", "string"); for
	 * (NameValuePair pair : Config.cleverPair) {
	 * cs.assignVariable(pair.getName(), pair.getValue()); }
	 * 
	 * // TODO Load default variables }
	 */

	public void sendCleverScriptResponse(String message) {
		speechOut.Speak(getResponseFromCleverScript(message));
	}

	public String getResponseFromCleverScript(String message) {
		return cs.sendMessage(message);
	}

	public String getRecentInput() {
		return cs.retrieveVariable("input");
	}

	// Below method will most likely be updated and replaced
	/*
	 * public void getInformationForVariables(String jsonString) { if(jsonString
	 * != null) { try { JSONObject obj = new JSONObject(jsonString); JSONArray
	 * jO = obj.getJSONArray("data");
	 * 
	 * for(int j = 0 ; j < jO.length() ; j ++) { JSONObject jobj =
	 * jO.getJSONObject(j);
	 * 
	 * NameValuePair nvP = new BasicNameValuePair(jobj.getString("key"),
	 * (String) jobj.getString("value")); Config.cleverPair.add(nvP);
	 * 
	 * cs.assignVariable((String) jobj.getString("key"), (String)
	 * jobj.getString("value")); Log.i(TAG, (String) jobj.getString("key") +
	 * " "); Log.i(TAG, (String) jobj.getString("value") + " ");
	 * 
	 * } } catch(Exception e) { //stuff } } }
	 */

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
