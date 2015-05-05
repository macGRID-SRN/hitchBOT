package Speech;

import com.example.hitchbot.Config;
import com.example.hitchbot.R;

import android.widget.Toast;

public class SpeechController {
	//The flow of speech is recognizer -> cleverscript -> text to speech .. repeat
	SpeechOut speechOut;
	CleverScriptHelper csh;
	GoogleRecognizer recognizer;
	
	public SpeechController()
	{
		recognizer = new GoogleRecognizer();
		speechOut = new SpeechOut();
        csh = new CleverScriptHelper(Config.context.getString(R.string.clever_db), 
        		Config.context.getString(R.string.clever_apikey));
        setup();
	}
	
	private void setup()
	{
		recognizer.setCleverHandler(csh);
		speechOut.setRecognizer(recognizer);
		csh.setSpeechOut(speechOut);
	}

	public void startCycle()
	{
		recognizer.startListening();
	}
	
	public void stopCycle()
	{
		speechOut.stopRecognition();
		Toast.makeText(Config.context, "Speech will stop after next speech output", 
				Toast.LENGTH_LONG).show();
	}
}
