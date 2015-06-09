package TransitionModels;

import org.json.JSONException;
import org.json.JSONObject;

import com.example.hitchbot.Config;
import com.google.gson.Gson;

public class LocationPack extends InfoPack {

	public double Latitude;
	public double Longitude;
	public double Altitude;
	public double Accuracy;
	public double Velocity;
	public boolean isPrecise;
	public int LocationProviderEnum;

	public LocationPack(double lat, double longitude) {
		this.Latitude = lat;
		this.Longitude = longitude;
		this.isPrecise = false;
		this.LocationProviderEnum = 2;
	}

	public LocationPack(double lat, double longitude, double alt,
			double accuracy, double velocity) {
		this.Latitude = lat;
		this.Longitude = longitude;
		this.Altitude = alt;
		this.Accuracy = accuracy;
		this.Velocity = velocity;
		this.isPrecise = true;
		this.LocationProviderEnum = 2;
	}

	protected LocationPack() {

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
