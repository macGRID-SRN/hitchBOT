package com.example.hitchbot;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Locale;

import com.cleverscript.android.CleverscriptAPI;

import static android.widget.Toast.makeText;
import edu.cmu.pocketsphinx.Assets;
import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.SpeechRecognizer;
import edu.cmu.pocketsphinx.RecognitionListener;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import android.support.v7.app.ActionBarActivity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.hardware.Camera;
import android.hardware.Camera.AutoFocusCallback;
import android.media.AudioManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import static edu.cmu.pocketsphinx.SpeechRecognizerSetup.defaultSetup;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends ActionBarActivity implements RecognitionListener{

	String mCurrentPhotoPath;
	static final int REQUEST_IMAGE_CAPTURE = 1;
	static final int REQUEST_TAKE_PHOTO = 1;
	TextToSpeech mTts;
    private static final String KWS_SEARCH = "wakeup";
    private static final String FORECAST_SEARCH = "forecast";
    private static final String DIGITS_SEARCH = "digits";
    private static final String MENU_SEARCH = "menu";
    private static final String KEYPHRASE = "oh mighty computer";
    private static final String HELLO_WORLD = "Hello World";
    private SpeechRecognizer recognizer;
    private HashMap<String, Integer> captions;
    private static Context context;
    private Bitmap image = null;  
    private CameraPreview cameraPreview;
    private ImageView imageResult;
    private FrameLayout frameNew;
    private boolean takePicture = false;
    
	//edit text and button are for debugging purposes, to be removed when hitchbot is ready for launch
	Button b;
	EditText editText;
	
	@Override	
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		editText = (EditText)findViewById(R.id.editText1);
		
		b = (Button)findViewById(R.id.button1);
		setUpCamera();
		mTts = new TextToSpeech(MainActivity.this, new TextToSpeech.OnInitListener()
				{
			@Override
		      public void onInit(int status) {
		         if(status != TextToSpeech.ERROR){
		             mTts.setLanguage(Locale.CANADA);
		             setTtsListener();
		            }		
				
			}
			});
		/*b.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
			}
		});*/
		
        captions = new HashMap<String, Integer>();
        captions.put(KWS_SEARCH, R.string.kws_caption);
        captions.put(MENU_SEARCH, R.string.menu_caption);
        captions.put(DIGITS_SEARCH, R.string.digits_caption);
        captions.put(FORECAST_SEARCH, R.string.forecast_caption);
        captions.put(HELLO_WORLD,R.string.hello_world);
        // Prepare the data for UI

 
        ((TextView) findViewById(R.id.editText2))
                .setText("Preparing the recognizer");
		
		 new AsyncTask<Void, Void, Exception>() {
	            protected Exception doInBackground(Void... params) {
	                try {
	                    Assets assets = new Assets(MainActivity.this);
	                    File assetDir = assets.syncAssets();
	                    setupRecognizer(assetDir);
	                } catch (IOException e) {
	                    return e;
	                }
	                return null;
	            }
	            
	            protected void onPostExecute(Exception result) {
	                if (result != null) {
	                    ((TextView) findViewById(R.id.editText2))
	                            .setText("Failed to init recognizer " + result);
	                } else {
	                    switchSearch(KWS_SEARCH);
	                }
	            }
	            
		}.execute();
	}
	
	public void getResponseFromCleverscript(String message)
	{
		CleverHelper cH = new CleverHelper("testers.db", "piuzd14d1da153d7e0982b169b8b87455d57d", this);
		Speak(cH.cs.sendMessage(message));		

	}
	
	public void Speak(String message)
	{
		recognizer.stop();
	    HashMap<String, String> myHashAlarm = new HashMap<String, String>();
		myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_STREAM, String.valueOf(AudioManager.STREAM_ALARM));
	    myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_UTTERANCE_ID, "SOME MESSAGE");
		mTts.speak(message, TextToSpeech.QUEUE_FLUSH, myHashAlarm);
	}
	
	@Override
	protected void onPause()
	{
	if (mTts != null)
	{
		mTts.stop();
		mTts.shutdown();
	}
	if (recognizer != null)
	{
		recognizer.stop();
		recognizer.cancel();
	}
	super.onPause();
	}
	
	@Override
	public void onResume() {
	    super.onResume();  

	    if (mTts == null) {
			mTts = new TextToSpeech(MainActivity.this, new TextToSpeech.OnInitListener() {
				
				@Override
				public void onInit(int status) {
					if (status != TextToSpeech.ERROR){
						mTts.setLanguage(Locale.US);
	}			
				}
			});
			
		      captions = new HashMap<String, Integer>();
		        captions.put(KWS_SEARCH, R.string.kws_caption);
		        captions.put(MENU_SEARCH, R.string.menu_caption);
		        captions.put(DIGITS_SEARCH, R.string.digits_caption);
		        captions.put(FORECAST_SEARCH, R.string.forecast_caption);
				
		        // Prepare the data for UI

		 
		        ((TextView) findViewById(R.id.editText2))
		                .setText("Preparing the recognizer");
				
				 new AsyncTask<Void, Void, Exception>() {
			            protected Exception doInBackground(Void... params) {
			                try {
			                    Assets assets = new Assets(MainActivity.this);
			                    File assetDir = assets.syncAssets();
			                    setupRecognizer(assetDir);
			                } catch (IOException e) {
			                    return e;
			                }
			                return null;
			            }
			            
			            protected void onPostExecute(Exception result) {
			                if (result != null) {
			                    ((TextView) findViewById(R.id.editText2))
			                            .setText("Failed to init recognizer " + result);
			                } else {
			                    switchSearch(MENU_SEARCH);
			                }
			            }
			            
				}.execute();
			
	    }
	}
	
	private void setTtsListener() {
	        int listenerResult = mTts.setOnUtteranceProgressListener(new UtteranceProgressListener()
	        {
	            @Override
	            public void onDone(String utteranceId)
	            {
	            	switchSearch(MENU_SEARCH);
	            }

	            @Override
	            public void onError(String utteranceId)
	            {
	            }

	            @Override
	            public void onStart(String utteranceId)
	            {
	            	recognizer.stop();	            }
	        });
	        if (listenerResult != TextToSpeech.SUCCESS)
	        {
	        }
	    }
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	private void switchSearch(String searchName) {
    recognizer.stop();
    recognizer.startListening(searchName);
    String caption = getResources().getString(captions.get(searchName));
	//mTts.speak(caption, TextToSpeech.QUEUE_FLUSH, null);
    ((TextView) findViewById(R.id.editText2)).setText(caption);
}

	private void setupRecognizer(File assetsDir) {
    File modelsDir = new File(assetsDir, "models");
    recognizer = defaultSetup()
            .setAcousticModel(new File(modelsDir, "hmm/en-us-semi"))
            .setDictionary(new File(modelsDir, "dict/cmu07a.dic"))
            .setRawLogDir(assetsDir).setKeywordThreshold(1e-20f)
            .getRecognizer();
    recognizer.addListener(this);

    // Create keyword-activation search.
   recognizer.addKeyphraseSearch(KWS_SEARCH, KEYPHRASE);
    // Create grammar-based searches.
    File menuGrammar = new File(modelsDir, "grammar/menu.gram");
    recognizer.addGrammarSearch(MENU_SEARCH, menuGrammar);
    File digitsGrammar = new File(modelsDir, "grammar/digits.gram");
    recognizer.addGrammarSearch(DIGITS_SEARCH, digitsGrammar);
    // Create language model search.
    File languageModel = new File(modelsDir, "lm/weather.dmp");
    recognizer.addNgramSearch(FORECAST_SEARCH, languageModel);
}

	@Override
	public void onBeginningOfSpeech() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onEndOfSpeech() {
        if (DIGITS_SEARCH.equals(recognizer.getSearchName()) ||
                 FORECAST_SEARCH.equals(recognizer.getSearchName()) || HELLO_WORLD.equals(recognizer.getSearchName()) )
        	recognizer.stop();
            switchSearch(MENU_SEARCH);  	
	}

	@Override
	public void onPartialResult(Hypothesis hypothesis) {
        String text = hypothesis.getHypstr();
        if (text.equals(KEYPHRASE))
            switchSearch(MENU_SEARCH);
        else if (text.equals(DIGITS_SEARCH))
            switchSearch(DIGITS_SEARCH);
        else if (text.equals(FORECAST_SEARCH))
            switchSearch(FORECAST_SEARCH);
        else{
            ((TextView) findViewById(R.id.editText2)).setText(text);
           // mTts.speak(text, TextToSpeech.QUEUE_FLUSH, null);
        }

	}

	@Override 
	public void onResult(Hypothesis hypothesis) {

        ((TextView) findViewById(R.id.editText2)).setText("");
        if (hypothesis != null) {
            String text = hypothesis.getHypstr();
            makeText(getApplicationContext(), text, Toast.LENGTH_SHORT).show();
            getResponseFromCleverscript(text);		
	}


}
	
	public void setUpCamera()
	{
		cameraPreview = new CameraPreview(this);
		imageResult = new ImageView(getApplicationContext());
		
		imageResult.setBackgroundColor(Color.GRAY);
		frameNew = (FrameLayout) findViewById(R.id.frame);
		frameNew.addView(imageResult);
		frameNew.addView(cameraPreview);
		
		frameNew.bringChildToFront(imageResult);
	}
	
	public void captureHandler(View view)
	{
		if (takePicture)
		{
			
		cameraPreview.capture(jpegHandler);	

		}
		else
		{
			takePicture = true;
			frameNew.bringChildToFront(cameraPreview);
			imageResult.setImageBitmap(null);
			b.setText("Capture");
			cameraPreview.camera.startPreview();
			//cameraPreview.detectFaces(image);
		}
	}
	
	public Camera.PictureCallback jpegHandler = new Camera.PictureCallback() {
		
		@Override
		public void onPictureTaken(byte[] data, Camera camera) {
			image = BitmapFactory.decodeByteArray(data, 0, data.length);
			imageResult.setImageBitmap(image);
			frameNew.bringChildToFront(imageResult);
			b.setText("Take Picture");
			takePicture = false;
			File pictureFile = getOutputMediaFile();
			FileOutputStream fos;
			try {
				fos = new FileOutputStream(pictureFile);
				fos.write(data);
				fos.close();
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			

		}
	};

	
	
	 private static File getOutputMediaFile() {
	        File mediaStorageDir = new File(
	                Environment
	                        .getExternalStoragePublicDirectory(Environment.DIRECTORY_PICTURES),
	                "MyCameraApp");
	        if (!mediaStorageDir.exists()) {
	            if (!mediaStorageDir.mkdirs()) {
	                Log.d("MyCameraApp", "failed to create directory");
	                return null;
	            }
	        }
	        // Create a media file name
	        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss")
	                .format(new Date());
	        File mediaFile;
	        mediaFile = new File(mediaStorageDir.getPath() + File.separator
	                + "IMG_" + timeStamp + ".jpg");

	        return mediaFile;
	    }
	
	 public static Context getAppContext() {
	        return MainActivity.context;
	    }
	 

	
	
	}

