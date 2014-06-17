package com.example.hitchbot;

import com.cleverscript.android.CleverscriptAPI;

import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
import android.widget.Toast;

public class CleverHelper implements RecognitionListener {
	
	public CleverscriptAPI cs;
	
	public CleverHelper(String db, String APIkey, MainActivity context)
	{
		cs = new CleverscriptAPI(context);
		cs.setLocation(db);
		cs.setApiKey(APIkey);
		cs.setDebugLevel(4);
	    cs.loadDatabase();
	}

	@Override
	public void onBeginningOfSpeech() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onEndOfSpeech() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onPartialResult(Hypothesis arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onResult(Hypothesis arg0) {
		// TODO Auto-generated method stub
		
	}

}
