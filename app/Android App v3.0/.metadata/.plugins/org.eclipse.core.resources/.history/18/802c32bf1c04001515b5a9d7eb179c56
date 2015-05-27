package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;

public class LocationPack extends InfoPack {

	private double Latitude;
	private double Longitude;
	private double Altitude;
	private double Accuracy;
	private double Velocity;
	private boolean isPrecise;

	public LocationPack(double lat, double longitude) {
		this.Latitude = lat;
		this.Longitude = longitude;
		this.isPrecise = false;
	}

	public LocationPack(double lat, double longitude, double alt,
			double accuracy, double velocity) {
		this.Latitude = lat;
		this.Longitude = longitude;
		this.Altitude = alt;
		this.Accuracy = accuracy;
		this.Velocity = velocity;
		this.isPrecise = true;
	}

	protected LocationPack() {

	}

	@Override
	public JSONObject toJson() {
		JSONObject jO = new JSONObject();
		try {
			jO.put("HitchBotId", Config.ID);
			jO.put("TimeUnix", System.currentTimeMillis() / 1000);
			jO.put("Time", Config.getUtcDate());

			jO.put("Latitude", this.Latitude);
			jO.put("Longitude", this.Longitude);
			if (isPrecise) {
				jO.put("Altitude", this.Altitude);
				jO.put("Accuracy", this.Accuracy);
				jO.put("Velocity", this.Velocity);
			}

			return jO;
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return null;
	}

}
