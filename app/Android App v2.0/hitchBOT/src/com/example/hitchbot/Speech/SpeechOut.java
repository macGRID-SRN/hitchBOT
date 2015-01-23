package com.example.hitchbot.Speech;

import java.util.HashMap;
import java.util.Locale;

import com.example.hitchbot.Config;
import com.example.hitchbot.Models.HttpPostDb;

import android.content.Context;
import android.media.AudioManager;
import android.net.Uri;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import android.util.Log;

public class SpeechOut {

	public TextToSpeech mTts;
	private SpeechController speechController;
	private boolean isStopping;
	private static final String TAG = "SpeechOut";
	private AudioManager am;

	public SpeechOut() {
		   am = (AudioManager) Config.context
					.getSystemService(Context.AUDIO_SERVICE);
		isStopping = false;
		mTts = new TextToSpeech(Config.context,
				new TextToSpeech.OnInitListener() {
					@Override
					public void onInit(int status) {
						if (status != TextToSpeech.ERROR) {
							mTts.setLanguage(Locale.GERMAN);
							mTts.setPitch((float) 1.0);
							setTtsListener();
						}
					}
				});
	}

	public void setSpeechController(SpeechController speechController) {
		this.speechController = speechController;
	}

	@SuppressWarnings("deprecation")
	public void Speak(String message) {

		HashMap<String, String> myHashAlarm = new HashMap<String, String>();
		// myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_STREAM,
		// String.valueOf(AudioManager.STREAM_ALARM));
		myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_UTTERANCE_ID,
				"SOME MESSAGE");
		queueSpoke(message);
		isStopping = false;
		Log.i(TAG, message);
		am.setStreamVolume(AudioManager.STREAM_MUSIC,
				am.getStreamMaxVolume(AudioManager.STREAM_MUSIC) / 4, 0);
		mTts.speak(message, TextToSpeech.QUEUE_FLUSH, myHashAlarm);
	}

	public void pauseTts() {
		isStopping = true;
		mTts.stop();
	}

	private void setTtsListener() {
		int listenerResult = mTts
				.setOnUtteranceProgressListener(new UtteranceProgressListener() {
					@Override
					public void onDone(String utteranceId) {						
						if (speechController.getSpeechIn() != null && isStopping == false) {
							Config.context.runOnUiThread(new Runnable() {
								@Override
								public void run() {
									if (!isStopping) {
										speechController
												.getSpeechIn()
												.switchSearch(Config.searchName);
									}
								}
							});
						}
						else
						{
							isStopping = false;
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

	private void queueSpoke(String spoke) {
		String uri = String.format(Config.spokePOST, Config.HITCHBOT_ID,
				Uri.encode(spoke), Config.getUtcDate());
		HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
		Config.dQ.addItemToQueue(httpPost);
	}

	public boolean isSpeaking() {
		return mTts.isSpeaking();
	}
	
	public void setIsStopping(boolean stop)
	{
		this.isStopping = stop;
	}

}
