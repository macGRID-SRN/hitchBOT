package com.example.hitchbot.Speech;

import android.os.Handler;
import android.speech.tts.TextToSpeech;
import com.example.hitchbot.Config;
import com.example.hitchbot.StoryRecorder;
import com.example.hitchbot.Models.HttpPostDb;

public class SpeechController {
	/*
	 * The purpose of this class is to: 1. prevent freezing of speech
	 * recognition 2. update clever variables periodically 3. pause speech and
	 * start story recording periodically (every 45 minutes for now)
	 */
	private SpeechIn speechIn;
	private SpeechOut speechOut;
	private CleverScriptHelper csh;
	private static final String TAG = "SpeechController";
	private Handler storyHandler;

	public SpeechController() {
		csh = new CleverScriptHelper(Config.cleverDB, Config.cleverAPIKey);
		speechIn = new SpeechIn();
		speechOut = new SpeechOut();
		setControllers();
		//storyHandler = new Handler();
		//setupHandlers();
	}

	private void setControllers() {
		speechOut.setSpeechController(this);
		csh.setSpeechController(this);
		speechIn.getOnline().setSpeechController(this);
		speechIn.getOffline().setSpeechController(this);
		Config.cH = csh;
	}

	public SpeechIn getSpeechIn() {
		return this.speechIn;
	}

	public SpeechOut getSpeechOut() {
		return this.speechOut;
	}

	public CleverScriptHelper getCleverScriptHelper() {
		return this.csh;
	}

	public void beginSpeechCycle() {
		Config.context.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				speechIn.switchSearch(Config.searchName);
			}
		});
	}

	@SuppressWarnings("deprecation")
	public void askToRecordAudio() {
		speechOut.mTts.speak("May I record a story from you for 60 seconds?",
				TextToSpeech.QUEUE_FLUSH, null);
		final StoryRecorder rlS = new StoryRecorder();
		try {
			Thread.sleep(1000);
			rlS.recordSixty();

		} catch (IllegalStateException e) {
			// TODO

		} catch (InterruptedException e) {
			// TODO e.printStackTrace();
		}

	}

	private void setupHandlers() {
		storyHandler.postDelayed(new Runnable() {

			@Override
			public void run() {
				pauseSpeechCycle();
				askToRecordAudio();
				storyHandler.postDelayed(this, Config.FORTYFIVE_MINUTES);
			}

		}, Config.FORTYFIVE_MINUTES);
	}

	public void pauseSpeechCycle() {
		Config.context.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				if (speechOut.isSpeaking()) {
					speechOut.pauseTts();
				}
				if (speechIn.getIsListening()) {
					speechIn.pauseRecognizer();
				}
			}

		});

	}

}
