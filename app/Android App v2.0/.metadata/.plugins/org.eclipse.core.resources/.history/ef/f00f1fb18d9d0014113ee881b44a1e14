package com.example.hitchbot.Speech;

import java.util.ArrayList;

import com.example.hitchbot.Config;
import com.example.hitchbot.Models.HttpPostDb;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.util.Log;
import android.widget.Toast;

public class OnlineRecognizer implements RecognitionListener{

	private SpeechRecognizer mSpeechRecognizer;
	private Intent mSpeechRecognizerIntent; 
	private SpeechController speechController;
	private static final String TAG = "GoogleSpeechRecognizer";
	private boolean stopRecognizer = false;
	
	public OnlineRecognizer()
	{
		mSpeechRecognizer = SpeechRecognizer.createSpeechRecognizer(Config.context);
	    mSpeechRecognizer.setRecognitionListener(this); 
		mSpeechRecognizerIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
	    mSpeechRecognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
	                                     RecognizerIntent.LANGUAGE_MODEL_FREE_FORM);
	    mSpeechRecognizerIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE,
	                                     Config.context.getPackageName());
	    mSpeechRecognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE, "de-DE");
	    mSpeechRecognizerIntent.putExtra("android.speech.extra.EXTRA_ADDITIONAL_LANGUAGES", new String[]{});
	    //de-DE
	}
	
	public void setSpeechController(SpeechController speechController)
	{
		this.speechController = speechController;
	}
	
	public void pauseRecognizer()
	{
		mSpeechRecognizer.cancel();
	}
	
	public void stopRecognizer()
	{
		stopRecognizer = true;
		mSpeechRecognizer.stopListening();
	}
	
	public void startListening()
	{
		stopRecognizer = false;
		speechController.getSpeechIn().setIsListening(true);
		mSpeechRecognizer.startListening(mSpeechRecognizerIntent);
	}
	
	@Override
	public void onReadyForSpeech(Bundle params) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onBeginningOfSpeech() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onRmsChanged(float rmsdB) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onBufferReceived(byte[] buffer) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onEndOfSpeech() {
		//mSpeechRecognizer.stopListening();
		
	}

	@Override
	public void onError(int error) {
		if(stopRecognizer){
		}
		else
		{
			Config.cH.sendCleverScriptResponse("I didn't catch that.");
		}
	}

	@Override
	public void onResults(Bundle results) {
		 ArrayList<String> matches = results.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
		 String message = matches.get(0);
		 Log.i(TAG, message + " ");
			String uri = String.format(Config.heardPOST, Config.HITCHBOT_ID,
					Uri.encode(message), Config.getUtcDate());
			HttpPostDb httpPost = new HttpPostDb(uri, 0, 3);
			Config.dQ.addItemToQueue(httpPost);
			speechController.getSpeechIn().setIsListening(false);
			Toast.makeText(Config.context, message, Toast.LENGTH_SHORT).show();
			Config.cH.sendCleverScriptResponse(message);
	}

	@Override
	public void onPartialResults(Bundle partialResults) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onEvent(int eventType, Bundle params) {
		// TODO Auto-generated method stub
		
	}

}
