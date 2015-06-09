package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;
import com.google.gson.Gson;

public class TabletPack extends InfoPack {
	public double BatteryTemp;
	public double BatteryVoltage;
	public boolean IsCharging;
	public double BatteryPercentage;

	public TabletPack(double batTemp, double batVolt, boolean isCharging,
			double batPercent) {
		this.BatteryTemp = batTemp;
		this.BatteryVoltage = batVolt;
		this.IsCharging = isCharging;
		this.BatteryPercentage = batPercent;
	}

	protected TabletPack() {

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
