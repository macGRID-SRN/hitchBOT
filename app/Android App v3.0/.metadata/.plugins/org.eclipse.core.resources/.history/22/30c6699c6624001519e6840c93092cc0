package Speech;

import static edu.cmu.pocketsphinx.SpeechRecognizerSetup.defaultSetup;

import java.io.File;
import java.io.IOException;
import java.util.HashMap;

import android.os.AsyncTask;
import android.os.Handler;
import android.util.Log;
import com.example.hitchbot.Config;
import edu.cmu.pocketsphinx.Assets;
import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
import edu.cmu.pocketsphinx.SpeechRecognizer;

public class PocketRecognizer implements RecognitionListener {

	private SpeechRecognizer recognizer;
	private HashMap<String, Integer> captions;
	private Handler freezeHandler;
	private SpeechController speechController;
	public boolean isListening = false;
	private boolean isInit = false;
	private CleverScriptHelper csh;
	private static final String TAG = "OfflineRecognizer";

	public PocketRecognizer() {
		initRecognizer();
	}

	public void setController(SpeechController sc) {
		this.speechController = sc;
	}

	private void initRecognizer() {

		// Prepare the data for UI
		captions = new HashMap<String, Integer>();
		captions.put(Config.searchName, 0);
		// ((TextView) ((SpeechActivity) Config.context)
		// .findViewById(R.id.textViewSpeechHistory))
		// .setText("Preparing the recognizer");

		new AsyncTask<Void, Void, Exception>() {
			@Override
			protected Exception doInBackground(Void... params) {
				try {
					Assets assets = new Assets(Config.context);
					File assetDir = assets.syncAssets();
					setupRecognizer(assetDir);
				} catch (IOException e) {
					return e;
				}
				return null;
			}

			@Override
			protected void onPostExecute(Exception result) {
				if (result != null) {
					Log.i(TAG, "Failed to init recognizer " + result);
				} else {
					isInit = true;
					speechController.startCycle();
				}
			}
		}.execute();
	}

	private void setupRecognizer(File assetsDir) {
		File modelsDir = new File(assetsDir, "models");
		recognizer = defaultSetup()
				.setAcousticModel(
						new File(modelsDir, "hmm/cmusphinx-en-us-5.2-8"))
				//.setFloat("-samprate", (float) 16000.0)
				.setDictionary(new File(modelsDir, "dict/1489.dic"))
				.setRawLogDir(assetsDir).setKeywordThreshold(1e-40f)
				.getRecognizer();

		recognizer.addListener(this);

		// Create keyword-activation search.
		// recognizer.addKeyphraseSearch(KWS_SEARCH, KEYPHRASE);
		// Create grammar-based searches.
		// File menuGrammar = new File(modelsDir, "grammar/menu.gram");
		// recognizer.addGrammarSearch(MENU_SEARCH, menuGrammar);
		// File digitsGrammar = new File(modelsDir, "grammar/digits.gram");
		// recognizer.addGrammarSearch(DIGITS_SEARCH, digitsGrammar);
		// Create language model search.
		File languageModel = new File(modelsDir, "lm/1489.dmp");
		recognizer.addNgramSearch(Config.searchName, languageModel);
	}

	public void startListening(String searchName) {
		if (isInit) {
			isListening = true;
			freezeHandler = new Handler();
			freezeHandler.postDelayed(new Runnable() {

				@Override
				public void run() {
					if (isListening)
						getResult();
				}

			}, 10 * 1000);
			recognizer.startListening(searchName);
		}
	}

	@Override
	public void onBeginningOfSpeech() {
		// TODO Auto-generated method stub

	}

	@Override
	public void onEndOfSpeech() {
		if (isListening) {
			isListening = false;
			recognizer.stop();
		}

	}

	@Override
	public void onPartialResult(Hypothesis arg0) {
		// TODO Auto-generated method stub

	}

	@Override
	public void onResult(Hypothesis hypothesis) {
		isListening = false;
		if (hypothesis != null) {
			String text = hypothesis.getHypstr();
			int score = hypothesis.getBestScore();
			Log.i(TAG, text);
			// TODO send data to cleverscript
			csh.sendCleverScriptResponse(text, score, 1);
		} else {
			Log.i(TAG, "null recognized result");
			csh.sendCleverScriptResponse("garbage");
			// TODO Send garbage to cleverscript
		}
	}

	public void setCleverHandler(CleverScriptHelper csh) {
		this.csh = csh;
	}

	public void getResult() {
		recognizer.cancel();
		isListening = false;
		Config.context.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				csh.sendCleverScriptResponse("garbage");
			}
		});
	}

}
