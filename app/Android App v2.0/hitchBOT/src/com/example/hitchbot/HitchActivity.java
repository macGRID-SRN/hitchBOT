package com.example.hitchbot;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;

import com.example.hitchbot.Data.DataGET;
import com.example.hitchbot.Data.DataPOST;
import com.example.hitchbot.Data.FileUpload;
import com.example.hitchbot.Models.FileUploadDb;
import com.example.hitchbot.Models.HttpPostDb;
import com.example.hitchbot.Speech.SpeechController;

import android.speech.RecognizerIntent;
import android.support.v7.app.ActionBarActivity;
import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.PowerManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

public class HitchActivity extends ActionBarActivity {

	public TakePicture tP;
	private SpeechController speechController;
	private Handler pictureHandler;
	private Handler dataCollectionHandler;
	private Handler internetHandler;
	private Handler fileUploadHander;
	private static final int REQUEST_CODE = 1234;
	Dialog match_text_dialog;
	ArrayList<String> matches_text;

	private static String TAG = "HitchActivity";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_hitch);
		Config.context = this;
		Config.dQ = DatabaseQueue.getHelper(this);
		Config.dQ.launchMissles();
		tP = new TakePicture();
		speechController = new SpeechController();
		setupHandlers();

	}

	@Override
	public void onDestroy() {
		super.onDestroy();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.hitch, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE && resultCode == RESULT_OK) {
			match_text_dialog = new Dialog(HitchActivity.this);
			matches_text = data
					.getStringArrayListExtra(RecognizerIntent.EXTRA_RESULTS);
			String message = matches_text.get(0);
			Log.i(TAG, message);
			String uri = String.format(Config.heardPOST, Config.HITCHBOT_ID,
					Uri.encode(message), Config.getUtcDate());
			HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
			Config.dQ.addItemToQueue(httpPost);
			speechController.getSpeechIn().setIsListening(false);
			Config.cH.sendCleverScriptResponse(message);
		} else {
			speechController.getSpeechIn().setIsListening(false);
			String uri = String.format(Config.heardPOST, Config.HITCHBOT_ID,
					Uri.encode("I didn't get that!"), Config.getUtcDate());
			HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
			Config.dQ.addItemToQueue(httpPost);
			Config.cH.sendCleverScriptResponse("I didn't get that!");
		}
	}

	public void takePicture() {
		tP.captureHandler();
	}

	private void setupHandlers() {
		dataCollectionHandler = new Handler();
		dataCollectionHandler.postDelayed(new Runnable() {

			@Override
			public void run() {
				// get environment and tablet info
				TabletInformation tI = new TabletInformation();
				tI.queueBatteryUpdates();

				// queue location info
				LocationUpdates lu = new LocationUpdates(Config.context);
				lu.setupProvider();
				dataCollectionHandler.postDelayed(this, Config.FIFTEEN_MINUTES);

			}

		}, Config.THIRTY_SECONDS);

		internetHandler = new Handler();
		internetHandler.postDelayed(new Runnable() {
			@Override
			public void run() {
				if (Config.networkAvailable()) {
					List<HttpPostDb> postQueue = Config.dQ
							.serverPostUploadQueue();
					HttpPostDb[] dbPostArray = new HttpPostDb[postQueue.size()];
					new DataPOST().execute(postQueue.toArray(dbPostArray));

					List<FileUploadDb> fileQueue = Config.dQ
							.serverFileUploadQueue();
					FileUploadDb[] dbFileArray = new FileUploadDb[fileQueue
							.size()];
					Log.i(TAG, String.valueOf(fileQueue.size()));
					uploadFile(fileQueue.toArray(dbFileArray));
					new DataGET().execute(Config.cleverGET);
					internetHandler.postDelayed(this, Config.HALF_HOUR);
				}
			}
		}, Config.ONE_MINUTE);

		pictureHandler = new Handler();
		pictureHandler.postDelayed(new Runnable() {

			@Override
			public void run() {
				takePicture();
				pictureHandler.postDelayed(this, Config.FIFTEEN_MINUTES);
			}

		}, Config.THIRTY_SECONDS);
	}

	public void uploadFile(final FileUploadDb[] fileUpload) {
		fileUploadHander = new Handler();
		fileUploadHander.post(new Runnable() {

			@Override
			public void run() {
				try {
					for (int i = 0; i < fileUpload.length; i++) {
						if (fileUpload[i] == null) {
							Log.i(TAG, "ITS NULL");
						}
						FileInputStream fstrm = new FileInputStream(
								fileUpload[i].getUri());
						String uploadUrl;
						switch (fileUpload[i].getFileType()) {
						case 0:
							uploadUrl = Config.audioPOST;
							break;
						case 1:
							uploadUrl = Config.imagePOST;
							break;
						default:
							uploadUrl = Config.audioPOST;
							break;
						}
						FileUpload hfu = new FileUpload(uploadUrl,
								"myfiletitle", "lifestoryORimage",
								fileUpload[i].getUri(), fileUpload[i]
										.getFileType());

						hfu.send_Now(fstrm);

					}
				} catch (FileNotFoundException e) {
					// TODO exception handling
				}
			}

		});

	}
}
