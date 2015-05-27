package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;

public class SpeechPack extends InfoPack {

	private String Said;
	private String Heard;
	private String Person;
	private String Notes;
	private String MatchedLineLabel;

	// These are optional
	private int MatchAccuracy;
	private int GoogleRecognitionScore;
	private double RmsDecibalLevel;

	public SpeechPack(String said, String heard, String matchedLine,
			int matchAccuracy, int recognitionScore, double decibalLevel) {
		this.Said = said;
		this.Heard = heard;
		//These parameters don't make sense in the production version
		this.Person = "";
		this.Notes = "";
		this.MatchedLineLabel = matchedLine;
		this.MatchAccuracy = matchAccuracy;
		this.GoogleRecognitionScore = recognitionScore;
		this.RmsDecibalLevel = decibalLevel;
	}

	protected SpeechPack() {

	}

	@Override
	public JSONObject toJson() {
		JSONObject jO = new JSONObject();
		try {
			jO.put("HitchBotId", Config.ID);
			jO.put("TimeUnix", System.currentTimeMillis() / 1000);
			jO.put("Time", Config.getUtcDate());

			jO.put("Said", this.Said);
			jO.put("Heard", this.Heard);
			jO.put("Person", this.Person);
			jO.put("Notes", this.Notes);
			jO.put("MatchedLineLabel", this.MatchedLineLabel);
			jO.put("GoogleRecognitionScore", this.GoogleRecognitionScore);
			jO.put("MatchAccuracy", this.MatchAccuracy);
			jO.put("RmsDecibalLevel", this.RmsDecibalLevel);
			jO.put("RecognizerType", 2);

			return jO;
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return null;
	}

}
