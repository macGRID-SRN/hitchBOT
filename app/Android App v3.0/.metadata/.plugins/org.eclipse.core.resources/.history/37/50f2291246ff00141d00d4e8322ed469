package Speech;

import java.util.HashMap;
import java.util.Locale;

import com.example.hitchbot.Config;
import com.example.hitchbot.Activities.SpeechActivity;

import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import android.util.Log;

public class SpeechOut {

	public TextToSpeech mTts;
	private GoogleRecognizer gRecognizer;
	private PocketRecognizer pRecognizer;
	private boolean stopCycle = false;
	private static final String TAG = "SpeechOut";

	public SpeechOut() {
		mTts = new TextToSpeech(Config.context,
				new TextToSpeech.OnInitListener() {
					@Override
					public void onInit(int status) {
						if (status != TextToSpeech.ERROR) {
							mTts.setLanguage(Locale.CANADA);
							mTts.setPitch((float) 1.0);
							setTtsListener();
						}
					}
				});
	}

	public void setRecognizer(GoogleRecognizer recognizer) {
		this.gRecognizer = recognizer;
	}
	
	public void setRecognizer(PocketRecognizer recognizer) {
		this.pRecognizer = recognizer;
	}

	@SuppressWarnings("deprecation")
	public void Speak(String message) {

		HashMap<String, String> myHashAlarm = new HashMap<String, String>();
		// myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_STREAM,
		// String.valueOf(AudioManager.STREAM_ALARM));
		myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_UTTERANCE_ID,
				"SOME MESSAGE");

		Log.i(TAG, message + "");
		mTts.speak(message, TextToSpeech.QUEUE_FLUSH, myHashAlarm);
	}

	public void pauseTts() {
		mTts.stop();
	}

	private void setTtsListener() {
		int listenerResult = mTts
				.setOnUtteranceProgressListener(new UtteranceProgressListener() {
					@Override
					public void onDone(String utteranceId) {
						if (!stopCycle) {
							Config.context.runOnUiThread(new Runnable() {
								@Override
								public void run() {
										gRecognizer.startListening();
									//else
									//	pRecognizer.startListening(Config.searchName);
								}
							});
						} else {
							//TODO maybe put in timeout here (for hitchbot sleep?)
						}

					}

					@Override
					public void onError(String utteranceId) {
						// TODO
					}

					@Override
					public void onStart(String utteranceId) {
					}

				});
		if (listenerResult != TextToSpeech.SUCCESS) {
		}
	}

	// Below method to TODO be updated and removed
	/*
	 * private void queueSpoke(String spoke) { String uri =
	 * String.format(Config.spokePOST, Config.HITCHBOT_ID, Uri.encode(spoke),
	 * Config.getUtcDate()); HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
	 * Config.dQ.addItemToQueue(httpPost); }
	 */

	public boolean isSpeaking() {
		return mTts.isSpeaking();
	}

	public void stopRecognition() {
		this.stopCycle = true;
	}
}
