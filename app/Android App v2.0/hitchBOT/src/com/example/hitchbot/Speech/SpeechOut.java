package com.example.hitchbot.Speech;

import java.util.HashMap;
import java.util.Locale;

import com.example.hitchbot.Config;

import android.speech.tts.TextToSpeech;

public class SpeechOut {
	private TextToSpeech mTts;

	public SpeechOut()
	{
		mTts = new TextToSpeech(Config.context, new TextToSpeech.OnInitListener()
		{
	@Override
	  public void onInit(int status) {
	     if(status != TextToSpeech.ERROR){
	         mTts.setLanguage(Locale.GERMAN);
	         mTts.setPitch((float) 1.0);
	         setTtsListener();
	        }			
	}
	});
	}
	

	@SuppressWarnings("deprecation")
	public void Speak(String message)
	{

		//recognizer.stop();
		//boolean containsBadWord = wordFilter(message);
		
		
	    HashMap<String, String> myHashAlarm = new HashMap<String, String>();
		myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_STREAM, String.valueOf(AudioManager.STREAM_ALARM));
	    myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_UTTERANCE_ID, "SOME MESSAGE");

		mTts.speak(message, TextToSpeech.QUEUE_FLUSH, myHashAlarm);


	    
	}
	
	private void setTtsListener() {
        int listenerResult = mTts.setOnUtteranceProgressListener(new UtteranceProgressListener()
        {
            @Override
            public void onDone(String utteranceId)
            {
            	//TODO
            }

            @Override
            public void onError(String utteranceId)
            {
            	//TODO
            }

            @Override
            public void onStart(String utteranceId)
            {
            	//TODO
            }
            	
        });
        if (listenerResult != TextToSpeech.SUCCESS)
        {
        }
    }
}