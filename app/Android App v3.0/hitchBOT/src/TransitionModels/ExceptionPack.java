package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;

public class ExceptionPack extends InfoPack {

	private String Message;

	
	public ExceptionPack(String message)
	{
		this.Message = message;
	}
	
	@Override
	public JSONObject toJson() {
		JSONObject jO = new JSONObject();
		try {
			jO.put("HitchBotId", Config.ID);
			jO.put("TimeUnix", System.currentTimeMillis() / 1000);
			jO.put("Time", Config.getUtcDate());

			jO.put("Message", this.Message);

			return jO;
			
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return null;
	}
	
}
