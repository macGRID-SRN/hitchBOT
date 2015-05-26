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
	
	//These are optional
	private int MatchAccuracy;
	private int GoogleRecognitionScore;
	private int RmsDecibalLevel;
	
	public SpeechPack(String said, String heard, String matchedLine, int matchAccuracy,
			int recognitionScore, int decibalLevel){
		this.Said = said;
		this.Heard = heard;
		this.Person = "";
		this.Notes = "";
		this.MatchedLineLabel = matchedLine;
		this.MatchAccuracy = matchAccuracy;
		this.GoogleRecognitionScore = recognitionScore;
		this.RmsDecibalLevel = decibalLevel;
	}
	
	@Override
	public JSONObject toJson() {
		JSONObject jO = new JSONObject();
		try {
			jO.put("HitchBotId", Config.ID);
			jO.put("TimeUnix", System.currentTimeMillis());
			jO.put("Time", Config.getUtcDate());
			
			jO.put("Said", this.Said);
			jO.put("Heard", this.Heard);
			jO.put("Person", this.Person);
			jO.put("Notes", this.Notes);
			jO.put("MatchedLineLabel", this.MatchedLineLabel);
			jO.put("GoogleRecognitionScore", this.GoogleRecognitionScore);
			jO.put("MatchAccuracy", this.MatchAccuracy);
			jO.put("RmsDecibalLevel", this.RmsDecibalLevel);

			return jO;
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return null;
	}

}
