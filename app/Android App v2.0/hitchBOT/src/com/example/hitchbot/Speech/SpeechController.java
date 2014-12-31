package com.example.hitchbot.Speech;

import java.util.Locale;

import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Handler;
import android.speech.tts.TextToSpeech;
import android.util.Log;

import com.example.hitchbot.Config;
import com.example.hitchbot.StoryRecorder;

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
	private GoogleSpeechRecognizer gsr;
	
	public SpeechController() {
		csh = new CleverScriptHelper(Config.cleverDB, Config.cleverAPIKey);
		gsr = new GoogleSpeechRecognizer();
		speechIn = new SpeechIn();
		speechOut = new SpeechOut();
		setControllers();
		storyHandler = new Handler();
		setupHandlers();
	}
	private void setControllers()
	{
		speechIn.setSpeechController(this);
		speechOut.setSpeechController(this);
		csh.setSpeechController(this);
		gsr.setSpeechController(this);
		Config.cH = csh;
	}
	public SpeechIn getSpeechIn()
	{
		return this.speechIn;
	}
	
	public SpeechOut getSpeechOut()
	{
		return this.speechOut;
	}
	
	public CleverScriptHelper getCleverScriptHelper()
	{
		return this.csh;
	}
	
	public GoogleSpeechRecognizer getGsr()
	{
		return this.gsr;
	}
	
	public void beginSpeechCycle() {
		speechIn.switchSearch(Config.searchName);
	}

	@SuppressWarnings("deprecation")
	public void askToRecordAudio() {
		csh.sendCleverScriptResponse("tell me a story please please");
		String response = csh.getRecentInput();
		response = response.toLowerCase(Locale.CANADA);
		if (!response.contains("no")) {
			speechOut.mTts.speak(
					"I will now record your life story for 60 seconds",
					TextToSpeech.QUEUE_FLUSH, null);

			final StoryRecorder rlS = new StoryRecorder();
			rlS.recordSixty();
		}
		else
		{
			csh.sendCleverScriptResponse("hello world");
		}

	}

	private void setupHandlers()
	{
		storyHandler.postDelayed(new Runnable()
		{

			@Override
			public void run() {
				pauseSpeechCycle();
				askToRecordAudio();
				storyHandler.postDelayed(this, Config.FORTYFIVE_MINUTES);
			}
			
		}, Config.ONE_MINUTE);
	}
	
	public void pauseSpeechCycle() {
		while (speechOut.isSpeaking()) {

		}
		if (speechIn.isListening()) {
			speechIn.pauseRecognizer();
			gsr.stopRecognizer();
		} else {
			pauseSpeechCycle();
		}
	}
	

}
