package com.example.hitchbot.Speech;

import java.util.HashMap;
import java.util.Locale;

import com.example.hitchbot.Config;
import com.example.hitchbot.Models.HttpPostDb;

import android.media.AudioManager;
import android.net.Uri;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;

public class SpeechOut {

	public TextToSpeech mTts;
	private boolean isSpeaking;
	private SpeechController speechController;

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

		mTts.speak(message, TextToSpeech.QUEUE_FLUSH, myHashAlarm);
		queueSpoke(message);
	}

	private void setTtsListener() {
		int listenerResult = mTts
				.setOnUtteranceProgressListener(new UtteranceProgressListener() {
					@Override
					public void onDone(String utteranceId) {
						isSpeaking = false;
						if(speechController.getSpeechIn() != null)
						{
							Config.context.runOnUiThread(new Runnable()
							{

								@Override
								public void run() {
									speechController.getSpeechIn().switchSearch(Config.searchName);									
								}
							});
						}
						
					}

					@Override
					public void onError(String utteranceId) {
						// TODO
					}

					@Override
					public void onStart(String utteranceId) {
						isSpeaking = true;
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
		return isSpeaking;
	}

}
