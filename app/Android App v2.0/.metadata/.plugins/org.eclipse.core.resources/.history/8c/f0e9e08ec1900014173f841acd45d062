package com.example.hitchbot.Speech;

import java.io.File;
import java.io.IOException;
import java.util.HashMap;

import com.example.hitchbot.Config;
import com.example.hitchbot.HitchActivity;
import com.example.hitchbot.R;
import com.example.hitchbot.Models.HttpPostDb;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.speech.RecognizerIntent;
import android.widget.TextView;
import android.widget.Toast;
import edu.cmu.pocketsphinx.Assets;
import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
import edu.cmu.pocketsphinx.SpeechRecognizer;
import static edu.cmu.pocketsphinx.SpeechRecognizerSetup.defaultSetup;

public class SpeechIn {

	private boolean isListening = false;
	private OnlineRecognizer onlineRecognizer;
	private OfflineRecognizer offlineRecognizer;
	
	public SpeechIn() {
		onlineRecognizer = new OnlineRecognizer();
		offlineRecognizer = new OfflineRecognizer();
	}
	
	public void setIsListening(boolean isListening)
	{
		this.isListening = isListening;
	}

	public boolean getIsListening() {
		return isListening;
	}
	
	public void pauseRecognizer() {
		offlineRecognizer.pauseRecognizer();
		onlineRecognizer.pauseRecognizer();
	}

	public OnlineRecognizer getOnline()
	{
		return onlineRecognizer;
	}
	
	public OfflineRecognizer getOffline()
	{
		return offlineRecognizer;
	}
	
	public void switchSearch(String searchName) {
		if (Config.networkAvailable()) {
			onlineRecognizer.startListening();
		} else {
			offlineRecognizer.startListening(searchName);
		}
	}


	


}
