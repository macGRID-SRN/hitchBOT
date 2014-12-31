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
	
	public SpeechController() {
		csh = new CleverScriptHelper(Config.cleverDB, Config.cleverAPIKey);
		Config.cH = csh;
		speechIn = new SpeechIn();
		speechOut = new SpeechOut();
		csh.setSpeechOut(speechOut);
		speechIn.setSpeechOut(speechOut);
		speechIn.setCleverScript(csh);
		speechOut.setSpeechIn(speechIn);
		storyHandler = new Handler();
		setupHandlers();
	}

	public SpeechIn getSpeechIn()
	{
		return this.speechIn;
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
			cancelGoogleActivityIfRunning();
			speechIn.pauseRecognizer();
		} else {
			pauseSpeechCycle();
		}
	}
	
	private void cancelGoogleActivityIfRunning()
	{
		try {
		    ActivityInfo[] list = Config.context.getPackageManager().getPackageInfo(Config.context.getPackageName(),
		    		PackageManager.GET_ACTIVITIES).activities;

		        for(int i = 0;i< list.length;i++)
		        {
		            Log.i(TAG, "List of running activities"+list[i].name);

		        } 
		    }

		    catch (NameNotFoundException e1) {
		        // TODO Auto-generated catch block
		        e1.printStackTrace();
		    }
	}

}
