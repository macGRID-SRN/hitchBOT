package TransitionModels;

import org.json.JSONObject;

import com.example.hitchbot.Config;

public abstract class InfoPack {
	
	public String Message;
	public int HitchBotId; 
	public long TimeUnix;
	public String Time;
	
	public InfoPack(){
		this.HitchBotId = Config.ID;
		this.Time = Config.getUtcDate();
		this.TimeUnix = System.currentTimeMillis() / 1000;
	}
	
	public abstract JSONObject toJson();
	
}
