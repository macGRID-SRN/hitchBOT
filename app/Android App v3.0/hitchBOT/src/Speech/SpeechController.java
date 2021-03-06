package Speech;

import com.example.hitchbot.Config;
import com.example.hitchbot.R;

import android.widget.Toast;

public class SpeechController {
	//The flow of speech is recognizer -> cleverscript -> text to speech .. repeat
	SpeechOut speechOut;
	CleverScriptHelper csh;
	GoogleRecognizer gRecognizer;
	//PocketRecognizer pRecognizer;
	
	public SpeechController()
	{
		gRecognizer = new GoogleRecognizer();
		//pRecognizer = new PocketRecognizer();
		//pRecognizer.setController(this);
		speechOut = new SpeechOut();
        csh = new CleverScriptHelper(Config.context.getString(R.string.clever_db), 
        		Config.context.getString(R.string.clever_apikey));
        setup();
	}
	
	private void setup()
	{
		gRecognizer.setCleverHandler(csh);
		//pRecognizer.setCleverHandler(csh);
		//speechOut.setRecognizer(pRecognizer);
		speechOut.setRecognizer(gRecognizer);
		csh.setSpeechOut(speechOut);
		Config.csh = csh;
	}

	public void startCycle()
	{
		//if(Config.networkAvailable())
		gRecognizer.startListening();
		//else
		//	pRecognizer.startListening(Config.searchName);
	}
	
	public void stopCycle()
	{
		speechOut.stopRecognition();
		Toast.makeText(Config.context, "Speech will stop after next speech output", 
				Toast.LENGTH_LONG).show();
	}
}
