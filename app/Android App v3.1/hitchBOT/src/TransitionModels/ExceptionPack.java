package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;
import com.google.gson.Gson;

public class ExceptionPack extends InfoPack {

	public String Message;

	
	public ExceptionPack(String message)
	{
		this.Message = message;
	}
	
	@Override
	public JSONObject toJson() {
		Gson gs = new Gson();
		String json = gs.toJson(this);
		JSONObject jO;
		try {
			jO = new JSONObject(json);
		} catch (JSONException e) {
			e.printStackTrace();
			return null;
		}
		return jO;

	}
	
}
