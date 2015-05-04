package Speech;

import java.util.ArrayList;

import com.example.hitchbot.Config;

import android.content.Context;
import android.content.Intent;
import android.media.AudioManager;
import android.os.Bundle;
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.util.Log;
import android.widget.Toast;

public class GoogleRecognizer implements RecognitionListener {

	private SpeechRecognizer mSpeechRecognizer;
	private Intent mSpeechRecognizerIntent; 
	private SpeechController speechController;
	private static final String TAG = "GoogleRecognizer";
	private boolean isListening = false;
	private boolean isSetup = false;
	AudioManager aM;
	
	public GoogleRecognizer ()
	{
		aM = (AudioManager)Config.context.getSystemService(Context.AUDIO_SERVICE);
		setupRecognizer();
	}
	
	private void setupRecognizer(){
		isSetup = true;
		mSpeechRecognizer = SpeechRecognizer.createSpeechRecognizer(Config.context);
	    mSpeechRecognizer.setRecognitionListener(this); 
		mSpeechRecognizerIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
	    mSpeechRecognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
	                                     RecognizerIntent.LANGUAGE_MODEL_FREE_FORM);
	    mSpeechRecognizerIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE,
	                                     Config.context.getPackageName());
	   // mSpeechRecognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE, "de-DE");
	   // mSpeechRecognizerIntent.putExtra("android.speech.extra.EXTRA_ADDITIONAL_LANGUAGES", new String[]{});
	}
	
	public void startListening()
	{
		if(!isSetup)
			setupRecognizer();
		aM.setStreamVolume(AudioManager.STREAM_MUSIC, 0, 0);
		isListening = true;
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
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onError(int error) {
		Log.i(TAG, "Speech Error: " + error);
		switch(error){
		case SpeechRecognizer.ERROR_AUDIO:
			handleError("");
			break;
		case SpeechRecognizer.ERROR_CLIENT:
			handleError("");
			break;
		case SpeechRecognizer.ERROR_NETWORK:
			handleError("");
			break;
		case SpeechRecognizer.ERROR_NETWORK_TIMEOUT:
			handleError("");
			break;
		case SpeechRecognizer.ERROR_NO_MATCH:
			handleError("7");
			break;
		case SpeechRecognizer.ERROR_SERVER:
			handleError("");
			break;
		case SpeechRecognizer.ERROR_SPEECH_TIMEOUT:
			handleError("");
			break;
		case SpeechRecognizer.ERROR_RECOGNIZER_BUSY:
			killRecognizer();
			handleError("8");
			break;
		default:
			handleError("");
			break;
		}
	}
	
	private void killRecognizer()
	{
		mSpeechRecognizer.destroy();
		isSetup = false;
	}
	
	private void handleError(String cleverText)
	{
		mSpeechRecognizer.cancel();
		Config.cH.sendCleverScriptResponse(cleverText);
	}

	@Override
	public void onResults(Bundle results) {
		isListening = false;
		aM.setStreamVolume(AudioManager.STREAM_MUSIC,
							aM.getStreamMaxVolume(AudioManager.STREAM_MUSIC), 0);
		ArrayList<String> matches = results.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
		String message = matches.get(0);
		Log.i(TAG, message + " ");
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
