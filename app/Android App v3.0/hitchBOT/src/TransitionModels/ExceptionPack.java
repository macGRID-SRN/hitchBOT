package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;

public class ExceptionPack extends InfoPack {

	private String Message;
	private String Exception;
	private String Arguments;
	private String Method;
	private String Data;
	private String Action;
	
	public ExceptionPack(String message)
	{
		this.Message = message;
		this.Exception = "";
		this.Arguments = "";
		this.Method = "";
		this.Data = "";
		this.Action = "";
	}
	
	@Override
	public JSONObject toJson() {
		JSONObject jO = new JSONObject();
		try {
			jO.put("HitchBotId", Config.ID);
			jO.put("TimeUnix", System.currentTimeMillis());
			jO.put("Time", Config.getUtcDate());

			jO.put("Message", this.Message);
			jO.put("Exception", this.Exception);
			jO.put("Arguments", this.Arguments);
			jO.put("Method", this.Method);
			jO.put("Data", this.Data);
			jO.put("Action", this.Action);

			return jO;
			
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return null;
	}
	
}
