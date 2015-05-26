package Models;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;

import android.util.Log;

import com.example.hitchbot.Config;

public class HttpPostDb {

	private int ID;
	private String uri;
	// 0 is no, 1 is yes
	private int uploadToServer;
	private String dateCreated;
	private String serializedBody = "";
	private String serializedHeader = "";
	private List<NameValuePair> body;
	private List<NameValuePair> header;
	private static String TAG = "HttpPostDb";
	// 0 image, 1 location, 2 spokenphrase, 3 heardphrase, 4 battery, 5 image, 6
	// audio
	// 7 exception --- These aren't really needed at this point, for future use!
	private int uploadType;

	public HttpPostDb(int ID, String uri, int uploadedToServer,
			String dateCreated, int uT) {
		this.ID = ID;
		this.uploadToServer = uploadToServer;
		this.setUri(uri);
		this.dateCreated = dateCreated;
		this.setUploadType(uT);
	}

	// just query string
	public HttpPostDb(String uri, int uploadedToServer, int uT) {
		this.uploadToServer = uploadToServer;
		this.setUri(uri);
		this.dateCreated = Config.getUtcDate();
		this.setUploadType(uT);
	}

	// general httpdb post
	public HttpPostDb(String uri, int uploadedToServer,
			List<NameValuePair> header, List<NameValuePair> body, int uT) {
		this.uploadToServer = uploadToServer;
		this.header = header;
		this.body = setPrimaryBodyParameters(body);
		this.setUri(uri);
		this.dateCreated = Config.getUtcDate();
		this.setUploadType(uT);
	}

	public HttpPostDb(String uri, int uploadedToServer, String header,
			String body, int uT) {
		this.uploadToServer = uploadToServer;
		this.serializedHeader = header;
		this.serializedBody = body;
		this.setUri(uri);
		this.dateCreated = Config.getUtcDate();
		this.setUploadType(uT);
	}

	public HttpPostDb() {

	}

	private static List<NameValuePair> setPrimaryBodyParameters(List<NameValuePair> params)
	{
		params.add(new BasicNameValuePair("HitchBotId",String.valueOf(Config.ID)));
		params.add(new BasicNameValuePair("TimeUnix",String.valueOf(System.currentTimeMillis())));
		params.add(new BasicNameValuePair("Time",Config.getUtcDate()));
		return params;
	}
	
	public int getUploadToServer() {
		return uploadToServer;
	}

	public void setUploadToServer(int uploadToServer) {
		this.uploadToServer = uploadToServer;
	}

	public int getID() {
		return ID;
	}

	public void setID(int iD) {
		ID = iD;
	}

	public String getDateCreated() {
		return dateCreated;
	}

	public void setDateCreated(String dateCreated) {
		this.dateCreated = dateCreated;
	}

	public String getUri() {
		return uri;
	}

	public void setUri(String uri) {
		this.uri = uri;
	}

	public List<NameValuePair> getHeader() {
		if (serializedHeader != "") {
			try {
				List<NameValuePair> nvp = new ArrayList<NameValuePair>();
				JSONObject jO = new JSONObject(serializedHeader);
				Iterator<String> keys = jO.keys();
				while (keys.hasNext()) {
					String key = keys.next();
					nvp.add(new BasicNameValuePair(key, jO.getString(key)));
				}
				return nvp;
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				return null;
			}
		} else if(header != null)
			return header;
		else
			return null;
	}

	public void setHeader(List<NameValuePair> header) {
		this.header = header;
	}

	public List<NameValuePair> getBody() {
		if (serializedBody != "") {
			try {
				List<NameValuePair> nvp = new ArrayList<NameValuePair>();
				JSONObject jO = new JSONObject(serializedBody);
				Iterator<String> keys = jO.keys();
				while (keys.hasNext()) {
					String key = keys.next();
					nvp.add(new BasicNameValuePair(key, jO.getString(key)));
				}
				return nvp;
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				return null;
			}
		} else if(body != null)
			return body;		
		else
			return null;
	}

	public void setBody(List<NameValuePair> body) {
		this.body = body;
	}

	public int getUploadType() {
		return uploadType;
	}

	public void setUploadType(int uploadType) {
		this.uploadType = uploadType;
	}

	public String getSerializedBody() {
		JSONObject jO = new JSONObject();
		if (body != null && body.size() > 0) {
			for (NameValuePair nvp : body) {
				try {
					jO.put(nvp.getName(), nvp.getValue());
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
			Log.i(TAG, "jason " + jO.toString());
			return jO.toString();
		} else
			Log.i(TAG, "already serialized" + serializedBody);
			return serializedBody;
	}

	public void setSerializedBody(String serializedBody) {
		this.serializedBody = serializedBody;
	}

	public String getSerializedHeader() {
		JSONObject jO = new JSONObject();
		if (header != null && header.size() > 0) {
			for (NameValuePair nvp : header) {
				try {
					jO.put(nvp.getName(), nvp.getValue());
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
			return jO.toString();

		} else
			return this.serializedHeader;
	}

	public void setSerializedHeader(String serializedHeader) {
		this.serializedHeader = serializedHeader;
	}

}
