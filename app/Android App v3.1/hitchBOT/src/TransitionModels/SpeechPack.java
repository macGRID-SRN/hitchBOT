package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;
import com.google.gson.Gson;

public class SpeechPack extends InfoPack {

	public String Said;
	public String Heard;
	public String Person;
	public String Notes;
	public String MatchedLineLabel;

	// These are optional
	public int MatchAccuracy;
	public int GoogleRecognitionScore;
	public double RmsDecibalLevel;

	public SpeechPack(String said, String heard, String matchedLine,
			int matchAccuracy, int recognitionScore, double decibalLevel) {
		this.Said = said;
		this.Heard = heard;
		//These parameters don't make sense in the production version
		this.MatchedLineLabel = matchedLine;
		this.MatchAccuracy = matchAccuracy;
		this.GoogleRecognitionScore = recognitionScore;
		this.RmsDecibalLevel = decibalLevel;
	}

	protected SpeechPack() {

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
