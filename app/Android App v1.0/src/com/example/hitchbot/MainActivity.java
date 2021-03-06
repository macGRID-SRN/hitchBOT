package com.example.hitchbot;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Locale;
import java.util.Timer;
import java.util.TimerTask;

import com.example.hitchbot.Bluetooth.BluetoothLeService;

import static android.widget.Toast.makeText;
import edu.cmu.pocketsphinx.Assets;
import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.SpeechRecognizer;
import edu.cmu.pocketsphinx.RecognitionListener;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import android.support.v7.app.ActionBarActivity;
import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothGattCharacteristic;
import android.bluetooth.BluetoothGattService;
import android.bluetooth.BluetoothManager;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.hardware.Camera;
import android.media.AudioManager;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.IBinder;
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
    //private static final String KWS_SEARCH = "wakeup";
   // private static final String HELLO_SEARCH = "hello world";
    private String MAIN_SEARCH = Config.MAIN_SEARCH;
    private SpeechRecognizer recognizer;
    private HashMap<String, Integer> captions;
    private static Context context;
    private Bitmap image = null;  
    private CameraPreview cameraPreview;
    private ImageView imageResult;
    private FrameLayout frameNew;
    private boolean takePicture = false;
	private String cleverState;
	private boolean _poweredOn = true;
	
	private static final long SCAN_PERIOD = 5000;
//-------Handlers--------------------------------
	private Handler audioUploadHandler;
	//private Handler bluetoothHandler;
	private Handler cameraHandler;
	private Handler locationHandler;
	private Handler serverGetHandler;
	private Handler serverPostHandler;
	private Handler batteryUploadHandler;
//------------------------BLE--------------------------
	
	private BluetoothGattCharacteristic characteristicTx = null;
	private BluetoothAdapter mBluetoothAdapter;
	private BluetoothLeService mBluetoothLeService;
	private BluetoothDevice mDevice = null;
	private String mDeviceAddress;

	private boolean ok = true;
	private boolean flag = true;
	private boolean connState = false;
	private boolean scanFlag = false;
	private byte[] data = new byte[3];
	private static final int REQUEST_ENABLE_BT = 1;

	//---------------------------------------------------------
	boolean firstOn;
	boolean secondOne;
	boolean thirdOn;
	String currentSearch;
	//-------------------------------------------------------
	
	String whatHitchbotHeard;
	String whatHitchbotSaid;
	
	ConversationPost said;
	ConversationPost heard;
	
	private DatabaseQueue dQ;
	//-------------------------------------------------------
	
	//edit text and button are for debugging purposes, to be removed when hitchbot is ready for launch
	Button b;
	EditText editText;
	
	@Override	
	protected void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		Config.context = this;
		editText = (EditText)findViewById(R.id.editText1);
		
	//To ensure app crashes and will be rebooted automatically with autostart and stay
		//Also logs the error that caused the crash
	Thread.setDefaultUncaughtExceptionHandler(new Thread.UncaughtExceptionHandler() {
		
		@Override
		public void uncaughtException(Thread thread, Throwable ex) {
			
			ErrorLog eL = new ErrorLog(ex.getMessage(), 0);
			dQ.addItemToQueue(eL);
			recognizer.cancel();
			cameraPreview.camera.release();
			System.exit(2);
		}
	});
	
	dQ = DatabaseQueue.getHelper(this);
		Config.cH = new CleverHelper("finalFinalCan.db", "e5f8vac6354c12086c404362878d5d27e5c4e", this);
		/*final BluetoothManager mBluetoothManager = (BluetoothManager) getSystemService(Context.BLUETOOTH_SERVICE);
		mBluetoothAdapter = mBluetoothManager.getAdapter();
		if(mBluetoothAdapter != null)
		{
			Log.i("bluetooth", "adapter isn't null");
		}
		if (!getPackageManager().hasSystemFeature(
				PackageManager.FEATURE_BLUETOOTH_LE)) {
			Toast.makeText(this, "Ble not supported", Toast.LENGTH_SHORT)
					.show();
			ErrorLog eL = new ErrorLog("BLE not supported (somehow turned off...)", 0);
            dQ.addItemToQueue(eL);
			finish();
		}*/
		
		/*Intent gattServiceIntent = new Intent(MainActivity.this,
				BluetoothLeService.class);
		bindService(gattServiceIntent, mServiceConnection, BIND_AUTO_CREATE);*/
		
		locationHandler = new Handler();
		locationHandler.postDelayed(new Runnable()
		{

			@Override
			public void run() {
				LocationInformation lI = new LocationInformation(Config.context);
				lI.setupProvider();
				locationHandler.postDelayed(this, Config.FIFTEEN_MINUTES);
			}
			
		}, 6000);

		b = (Button)findViewById(R.id.button1);
		
		setUpCamera();
		
	   /*bluetoothHandler = new Handler();
	    
	    bluetoothHandler.postDelayed(new Runnable()
	    {

			@Override
			//public void run() {
				//connectToDevice();
				bluetoothHandler.postDelayed(this, Config.TEN_MINUTES);
			}
	    	
	    }, 1000);*/
	    
	    cameraHandler = new Handler();
	    
	    cameraHandler.postDelayed(new Runnable()
	    {

			@Override
			public void run() {
				if(!takePicture)
				{
				b.performClick();
				try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				b.performClick();
			}
				cameraHandler.postDelayed(this, Config.FIFTEEN_MINUTES);

			}
	    	
	    }, 2000);

		serverPostHandler = new Handler();
		
		serverPostHandler.postDelayed(new Runnable()
		{

			@Override
			public void run() {
				postStuff();
				serverPostHandler.postDelayed(this, Config.THREE_HOURS);
			}
		}
		, 1000);
	    

		
		batteryUploadHandler = new Handler();
		
		batteryUploadHandler.postDelayed(new Runnable()
		{

			@Override
			public void run() 
			{
				BatteryInformation bI = new BatteryInformation();
				bI.uploadBatteryInformation();
				batteryUploadHandler.postDelayed(this,Config.HOUR);
			}
	
		}, 5000);		


		startTtsAndSpeechRec();
        captions = new HashMap<String, Integer>();
        //captions.put(KWS_SEARCH, R.string.kws_caption);
        captions.put(Config.MAIN_SEARCH, R.string.forecast_caption);
       // captions.put(FIRST_SEARCH, R.string.digits_caption);
        captions.put(Config.SECOND_SEARCH, R.string.hello_world);
      //  captions.put(THIRD_SEARCH, R.string.kws_caption);
      //  captions.put(HELLO_WORLD,R.string.hello_world);
        // Prepare the data for UI

 
        ((TextView) findViewById(R.id.editText2))
                .setText("Preparing the recognizer");
		

		set_poweredOn(false);
		AudioManager aM = (AudioManager) getSystemService(Context.AUDIO_SERVICE);
		aM.setStreamVolume(AudioManager.STREAM_MUSIC, aM.getStreamMaxVolume(AudioManager.STREAM_MUSIC), 0);

		aM.setSpeakerphoneOn(true);
	}

	public void startTtsAndSpeechRec()
	{
	    mTts = new TextToSpeech(MainActivity.this, new TextToSpeech.OnInitListener()
				{
			@Override
		      public void onInit(int status) {
		         if(status != TextToSpeech.ERROR){
		             mTts.setLanguage(Locale.US);
		             mTts.setPitch((float) 1.0);
		             setTtsListener();
		            }		
				
			}
			});
		
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
	                    ErrorLog eL = new ErrorLog("Recognizer failed: " + result, 0);
	                    dQ.addItemToQueue(eL);
	                } else {
	                    switchSearch(MAIN_SEARCH);
	                }
	            }
	            
		}.execute();
	}
	
	public void firstCleverScript(View view)
	{
		recordLifeStory();

	}
	
	
	public void getResponseFromCleverscript(String message, CleverHelper cH)
	{
		/*Checks if empty input, if so don't send to cleverscript and restart recognizer.
		 * Also, if below some threshhold accuracy the next cleverstate is not accessed
		 * This is done to hopefully improve recognition in situations with a lot of
		 * background noise (ex/ car) */
		String tAg = "Script";
		Log.i(tAg, cH.cs.retrieveBotState());
		Log.i(tAg, "testbar");
		Log.i(tAg, cH.cs.retrieveVariable("audio_on"));
		Log.i(tAg, cH.cs.retrieveVariable("accuracy"));
		Log.i(tAg, cH.cs.retrieveVariable("wikipedia_output")  + "wikipedia");
		Log.i(tAg, cH.cs.retrieveVariable("last_three_cities") + "cities");


		//Log.i(tAg, cH.getClever_data());
		Log.i(tAg, message);

		if(message.isEmpty() || !ok)
		{
        	switchSearch(MAIN_SEARCH);
		}
		else
		{
		String response = cH.cs.sendMessage(message);
		Log.i("CleverScript", cH.getAccuracy());
	if(!cH.getAccuracy().isEmpty())
	{
		if(Integer.parseInt(cH.getAccuracy()) < Config.THRESHHOLD_ACCURACY)
		{
			cH.loadBotState(cleverState);
        	switchSearch(MAIN_SEARCH);
		}
		else
		{
			cleverState = cH.getBotState();
			whatHitchbotHeard = message;
			if(cH.getAudio_on().equals("true"))
			{
				recordLifeStory();
			}
			else
			{
				(new ConversationPost()).conversationStart();
				Speak(response);				
			}
		}
	}
	else
	{
		(new ConversationPost()).conversationStart();
		MAIN_SEARCH = Config.MAIN_SEARCH;
		whatHitchbotHeard = message;
		if(cH.getAudio_on().equals("true"))
		{
			recordLifeStory();
		}
		else
		{
			Speak(response);
		}	
	}
		}
	}
	
	
	
	public void Speak(String message)
	{

		//recognizer.stop();
		//boolean containsBadWord = wordFilter(message);
		
		
	    HashMap<String, String> myHashAlarm = new HashMap<String, String>();
		myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_STREAM, String.valueOf(AudioManager.STREAM_ALARM));
	    myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_UTTERANCE_ID, "SOME MESSAGE");
	   
	    whatHitchbotSaid = message;
	    if(whatHitchbotHeard != ""){
	    	Log.i("FileDeleted", " heard wasn't null" + whatHitchbotHeard);

	    heard = new ConversationPost(whatHitchbotHeard, false);
	    }
	    if(whatHitchbotSaid != ""){
	    	Log.i("FileDeleted", "said wasn't null");
	    said = new ConversationPost(whatHitchbotSaid, true);
	    }

		mTts.speak(message, TextToSpeech.QUEUE_FLUSH, myHashAlarm);


	    
	}
	
	
	//Don't want hitchBOT talking about these things
	public boolean wordFilter(String message)
	{
		message = message.toLowerCase(Locale.CANADA);
		String[] bannedWords = new String[] {
				"god",
				"jesus",
				"church",
				"christian",
				"idiot"
		};
		for(String bannedWord : bannedWords)
		{
			if(message.contains(bannedWord))
			{
				return true;
			}
		}
		return false;
	}
	
	@Override
	protected void onPause()
	{
		cameraPreview.camera.release();
		System.exit(2);
		super.onPause();
	}
	
	@Override
	public void onResume() {
	    super.onResume();  
	
	}
	
	@Override
	public void onStop()
	{
		System.exit(2);
	}
	
	private void setTtsListener() {
	        int listenerResult = mTts.setOnUtteranceProgressListener(new UtteranceProgressListener()
	        {
	            @Override
	            public void onDone(String utteranceId)
	            {
	            	if(mDevice != null){
	            	stoppedTalking();
	            	Log.i("deviceNull", data.toString() + " stopped talking");
	            	}
	            	switchSearch(MAIN_SEARCH);
	            }

	            @Override
	            public void onError(String utteranceId)
	            {
	            	ErrorLog eL = new ErrorLog("Tts error: " + utteranceId, 0);
                    dQ.addItemToQueue(eL);
                    System.exit(2);
	            }

	            @Override
	            public void onStart(String utteranceId)
	            {
	            	//recognizer.stop();
	            	if(mDevice != null){
	            	isTalking();
	            	Log.i("deviceNull", data.toString() + " is talking");

	            	}
	            	
	            }
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
    //((TextView) findViewById(R.id.editText2)).setText(caption);
}

	private void setupRecognizer(File assetsDir) {
    File modelsDir = new File(assetsDir, "models");
    recognizer = defaultSetup()
            .setAcousticModel(new File(modelsDir, "hmm/en-us-semi"))
            .setDictionary(new File(modelsDir, "dict/3829.dic"))
            .setRawLogDir(assetsDir).setKeywordThreshold(1e-20f)
            .getRecognizer();
    recognizer.addListener(this);

    // Create keyword-activation search.
  // recognizer.addKeyphraseSearch(KWS_SEARCH, KEYPHRASE);

    //recognizer.addGrammarSearch(DIGITS_SEARCH, digitsGrammar);
    // Create language model search.
    File languageModel1 = new File(modelsDir, "lm/3829.dmp");
  //  File languageModel2 = new File(modelsDir, "lm/6392.dmp");
   // File languageModel3 = new File(modelsDir, "lm/7400.dmp");
  //  File languageModel = new File(modelsDir, "lm/7788.dmp");
    recognizer.addNgramSearch(Config.MAIN_SEARCH, languageModel1);
   // recognizer.addNgramSearch(THIRD_SEARCH, languageModel3);
  //  recognizer.addNgramSearch(Config.SECOND_SEARCH, languageModel2);
   //     recognizer.addNgramSearch(THIRD_SEARCH, languageModel3);


}

	@Override
	public void onBeginningOfSpeech() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onEndOfSpeech() {
		//if (MAIN_SEARCH.equals(recognizer.getSearchName()))
       recognizer.stop();
            //switchSearch(MAIN_SEARCH);  	
	}

	@Override
	public void onPartialResult(Hypothesis hypothesis) {
			String text = hypothesis.getHypstr();
			//Limit the amount of words it will listen for (to prevent convo
			//from hanging
            ((TextView) findViewById(R.id.editText2)).setText(text);

			if (text.split("\\s+").length >= 8)
			{
				recognizer.stop();			}
           // mTts.speak(text, TextToSpeech.QUEUE_FLUSH, null);
            //TODO use this method to fix background noise problem
	}

	@Override 
	public void onResult(Hypothesis hypothesis) {
		

        ((TextView) findViewById(R.id.editText2)).setText("");
        if (hypothesis != null ) {
            String text = hypothesis.getHypstr();
    		if(text.contains("HITCHBOT SLEEP") || text.contains("HITCH BOT SLEEP"))
    		{
    			sleepHitchBOT();
    		}
    		else
    		{
            makeText(getApplicationContext(), text, Toast.LENGTH_SHORT).show();
            getResponseFromCleverscript(text, Config.cH);
    		}
    	
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
			//String numFace = String.valueOf(cameraPreview.detectFaces(image));
			//Toast.makeText(context, numFace, Toast.LENGTH_SHORT).show();
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
			Uri imageUri = Uri.fromFile(pictureFile);
			new UploadImageImgur(imageUri, getThis(), getThis(), Config.getUtcDate() ).execute();
			
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

	private MainActivity getThis()
	{
		return this;
	}
	
	
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
	        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss", Locale.US)
	                .format(new Date());
	        File mediaFile;
	        mediaFile = new File(mediaStorageDir.getPath() + File.separator
	                + "IMG_" + timeStamp + ".jpg");

	        return mediaFile;
	    }
	
	 public static Context getAppContext() {
	        return MainActivity.context;
	    }

	public boolean is_poweredOn() {
		return _poweredOn;
	}

	public void set_poweredOn(boolean _poweredOn) {
		this._poweredOn = _poweredOn;
	}
	 
	private void scanLeDevice() {
		new Thread() {

			@Override
			public void run() {
				mBluetoothAdapter.startLeScan(mLeScanCallback);

				try {
					Thread.sleep(SCAN_PERIOD);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}

				mBluetoothAdapter.stopLeScan(mLeScanCallback);
			}
		}.start();
	}

	//Device scan callback.
	private BluetoothAdapter.LeScanCallback mLeScanCallback =
	     new BluetoothAdapter.LeScanCallback() {
	 @Override
	 public void onLeScan(final BluetoothDevice device, int rssi,
	         byte[] scanRecord) {
			Log.i("bluetooth", device.getName());
			Log.i("bluetooth", device.getAddress());
			
		/*	if (device != null)
			{
				mDeviceAddress = device.getAddress();
				mDevice = device;	
			}*/
			
			runOnUiThread(new Runnable() {
				@Override
				public void run() {
					if (device != null) {
						Log.i("bluetooth", "device wasn't null");

						if ( device.getName().contains("Biscuit")) {

							mDeviceAddress = device.getAddress();
							mDevice = device;
						}
					}
				}
			});
		}
	};

	public void isTalking()
	{
		data = new byte[] { (byte) 0x01, (byte) 0x00, (byte) 0x00 };
		characteristicTx.setValue(data);
		mBluetoothLeService.writeCharacteristic(characteristicTx);
	}

	public void stoppedTalking()
	{
		data = new byte[] { (byte) 0x00, (byte) 0x00, (byte) 0x00 };
		characteristicTx.setValue(data);
		mBluetoothLeService.writeCharacteristic(characteristicTx);
	}

	private void getGattService(BluetoothGattService gattService) {
		if (gattService == null)
			return;

		startReadRssi();

		characteristicTx = gattService
				.getCharacteristic(BluetoothLeService.UUID_BLE_SHIELD_TX);

		BluetoothGattCharacteristic characteristicRx = gattService
				.getCharacteristic(BluetoothLeService.UUID_BLE_SHIELD_RX);
		mBluetoothLeService.setCharacteristicNotification(characteristicRx,
				true);
		mBluetoothLeService.readCharacteristic(characteristicRx);
	}

	private void startReadRssi() {
		new Thread() {
			public void run() {

				while (flag) {
					mBluetoothLeService.readRssi();
					try {
						sleep(500);
					} catch (InterruptedException e) {
						e.printStackTrace();
					}
				}
			};
		}.start();
	}

	public void connectToDevice()
	{
		if (scanFlag == false) {
			scanLeDevice();

			Timer mTimer = new Timer();
			mTimer.schedule(new TimerTask() {

				@Override
				public void run() {
					if (mDevice != null) {
					//	mDeviceAddress = mDevice.getAddress();
						mBluetoothLeService.connect(mDeviceAddress);
						scanFlag = true;
					} else {
					Log.i("bluetooth", "something went wrong");
					}
				}
			}, SCAN_PERIOD);
		}
	//	try {
	//		Thread.sleep(1000);
	//	} catch (InterruptedException e) {
	//		e.printStackTrace();
	//	}
		//System.out.println(connState);
		if (connState == false) {
			Log.i("bluetooth", mDeviceAddress + "  foobar");
			Log.i("bluetooth",mBluetoothLeService.toString());
			mBluetoothLeService.connect(mDeviceAddress);
		} else {
			mBluetoothLeService.disconnect();
			mBluetoothLeService.close();
		}	
		
	}

	public ServiceConnection getmServiceConnection() {
		return mServiceConnection;
	}

	private final ServiceConnection mServiceConnection = new ServiceConnection() {

		@Override
		public void onServiceConnected(ComponentName componentName,
				IBinder service) {
			mBluetoothLeService = ((BluetoothLeService.LocalBinder) service)
					.getService();
			Log.i("bluetooth", "foobar t");
			if (!mBluetoothLeService.initialize()) {
				Log.e("BluetoothScanningClass", "Unable to initialize Bluetooth");
				finish();
			}
		}

		@Override
		public void onServiceDisconnected(ComponentName componentName) {
			mBluetoothLeService = null;
		}
	};

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		// User chose not to enable Bluetooth.
		if (requestCode == REQUEST_ENABLE_BT
				&& resultCode == Activity.RESULT_CANCELED) {
			finish();
			return;
		}

		super.onActivityResult(requestCode, resultCode, data);
	}
	
	private final BroadcastReceiver mGattUpdateReceiver = new BroadcastReceiver() {
		@Override
		public void onReceive(Context context, Intent intent) {
			final String action = intent.getAction();

			if (BluetoothLeService.ACTION_GATT_DISCONNECTED.equals(action)) {
				Toast.makeText(getApplicationContext(), "Disconnected",
						Toast.LENGTH_SHORT).show();
			} else if (BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED
					.equals(action)) {
				Toast.makeText(getApplicationContext(), "Connected",
						Toast.LENGTH_SHORT).show();

				getGattService(mBluetoothLeService.getSupportedGattService());
			} else if (BluetoothLeService.ACTION_DATA_AVAILABLE.equals(action)) {
				data = intent.getByteArrayExtra(BluetoothLeService.EXTRA_DATA);

				//readAnalogInValue(data);
			} else if (BluetoothLeService.ACTION_GATT_RSSI.equals(action)) {
			}
		}
	};
	
	
	private static IntentFilter makeGattUpdateIntentFilter() {
		final IntentFilter intentFilter = new IntentFilter();

		intentFilter.addAction(BluetoothLeService.ACTION_GATT_CONNECTED);
		intentFilter.addAction(BluetoothLeService.ACTION_GATT_DISCONNECTED);
		intentFilter.addAction(BluetoothLeService.ACTION_GATT_SERVICES_DISCOVERED);
		intentFilter.addAction(BluetoothLeService.ACTION_DATA_AVAILABLE);
		intentFilter.addAction(BluetoothLeService.ACTION_GATT_RSSI);

		return intentFilter;
	}
	
	private void postStuff()
	{
		PostGeneralUpdates pGu = new PostGeneralUpdates();
		pGu.sendImgurUploads();
		pGu.sendErrorLog();
		pGu.sendImagePosts();
		pGu.sendAudioRecordings();
	}
	
	private void sleepHitchBOT()
	{
		mTts.speak("I will now sleep for an hour, I am getting tired.", 
				TextToSpeech.QUEUE_FLUSH, null);
		recognizer.stop();
		Handler sleepHandler = new Handler();
		sleepHandler.postDelayed(new Runnable()
		{

			@Override
			public void run() {
				switchSearch(MAIN_SEARCH);
			}
			
		}, Config.HOUR);
	}
	
	private void recordLifeStory()
	{
		recognizer.stop();
		mTts.speak("I will now record your life story for 60 seconds",
				TextToSpeech.QUEUE_FLUSH, null);
		final RecordLifeStory rlS = new RecordLifeStory();
		rlS.startRecording();
		(new Handler()).postDelayed(new Runnable()
		{

			@Override
			public void run() {
				rlS.stopRecording();
				uploadAudioFile(rlS.mFileName);
				Config.cH.cs.assignVariable("audio_on", "false");
				getResponseFromCleverscript("done", Config.cH);
				
			}
			
		}, 60000);
		
		
	}
	public void uploadAudioFile(final String filePath){
		audioUploadHandler = new Handler();
		audioUploadHandler.post(new Runnable()
		{

			@Override
			public void run() {
				try {
				    FileInputStream fstrm = new FileInputStream(filePath);

				    UploadAudio hfu = new UploadAudio("http://hitchbotapi.azurewebsites.net/api/hitchBOT", "myfiletitle","lifestory", filePath);

				    hfu.send_Now(fstrm);

				  } catch (FileNotFoundException e) {
				    ErrorLog eL = new ErrorLog("filenotfoundaudiologupload", 0);
				    dQ.addItemToQueue(eL);
				  }
				}				
			
			
		});

	
	}
}

