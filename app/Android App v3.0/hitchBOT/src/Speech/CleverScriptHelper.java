package Speech;

import org.json.JSONArray;
import org.json.JSONObject;

import Models.HttpPostDb;
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
	    loadVariables();
	}

	public void setSpeechOut(SpeechOut speechOut) {
		this.speechOut = speechOut;
	}

	public void loadVariables() {
		cs.assignVariable("current_velocity", "2");
		cs.assignVariable("weather_status", "fog");
		cs.assignVariable("weather_temperatureC", "9001");
		cs.assignVariable("localtime", "100");
		cs.assignVariable("current_city_name", "Toronto");
	}

	public void sendCleverScriptResponse(String message, double rmsDbLevel,
			float accuracy, int recognizer) {
		((SpeechActivity) Config.context).updateYourChat(message);
		String output = getResponseFromCleverScript(message);
		String uri = String.format(Config.conversationPost, Config.ID,
				Uri.encode(output), Uri.encode(message), Config.getUtcDate(),
				Uri.encode(Config.name), Uri.encode(Config.specInfo),
				Uri.encode(getRecentOutputLabel()), getAccuracy(), rmsDbLevel,
				"", "", accuracy, recognizer);
		HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
		Config.dQ.addItemToQueue(httpPost);
		speechOut.Speak(output);
	}

	public void sendCleverScriptResponse(String message, int accuracy,
			int recognizer) {
		((SpeechActivity) Config.context).updateYourChat(message);
		String output = getResponseFromCleverScript(message);
		String uri = String.format(Config.conversationPost, Config.ID,
				Uri.encode(output), Uri.encode(message), Config.getUtcDate(),
				Uri.encode(Config.name), Uri.encode(Config.specInfo),
				Uri.encode(getRecentOutputLabel()), getAccuracy(), "", "", "",
				accuracy, recognizer);
		HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
		Config.dQ.addItemToQueue(httpPost);
		speechOut.Speak(output);
	}

	public void sendCleverScriptResponse(String message) {
		((SpeechActivity) Config.context).updateYourChat(message);
		String output = getResponseFromCleverScript(message);
		String uri = String.format(Config.conversationPost, Config.ID,
				Uri.encode(output), Uri.encode(message), Config.getUtcDate(),
				Uri.encode(Config.name), Uri.encode(Config.specInfo),
				Uri.encode(getRecentOutputLabel()), getAccuracy(), "", "", "",
				"", "");
		HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
		Config.dQ.addItemToQueue(httpPost);
		speechOut.Speak(output);
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