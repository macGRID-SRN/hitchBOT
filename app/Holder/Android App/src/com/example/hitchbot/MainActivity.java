package com.example.hitchbot;

import java.io.File;
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
import android.view.Gravity;
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
   // private static final String HELLO_SEARCH = "hello world";
    private static final String MAIN_SEARCH = "forecast";
    private SpeechRecognizer recognizer;
    private HashMap<String, Integer> captions;
    private static Context context;
    private Bitmap image = null;  
    private CameraPreview cameraPreview;
    private ImageView imageResult;
    private FrameLayout frameNew;
    private boolean takePicture = false;
	private CleverHelper cH;
	private boolean _poweredOn = true;
	private BluetoothAdapter mBluetoothAdapter;
	
	private static final long SCAN_PERIOD = 10000;
	private Handler mHandler2;
	private Handler locationHandler;
	private BluetoothGattCharacteristic characteristicTx = null;
	private BluetoothLeService mBluetoothLeService;
	private BluetoothDevice mDevice = null;
	private String mDeviceAddress;

	private boolean flag = true;
	private boolean connState = false;
	private boolean scanFlag = false;
	private byte[] data = new byte[3];
	private static final int REQUEST_ENABLE_BT = 1;


    
	//edit text and button are for debugging purposes, to be removed when hitchbot is ready for launch
	Button b;
	EditText editText;
	
	@Override	
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		Config.context = this;
		editText = (EditText)findViewById(R.id.editText1);
		//j0zo6727bb5bea8c76abe674e05a49bdc08e2
		cH = new CleverHelper("finalCBC.db", "j0zo6727bb5bea8c76abe674e05a49bdc08e2", this);
		//cH = new CleverHelper("theRealDeal.db", "8z9whb0759bf324287c8fac1f7a06673bd1ef", this);
		final BluetoothManager mBluetoothManager = (BluetoothManager) getSystemService(Context.BLUETOOTH_SERVICE);
		mBluetoothAdapter = mBluetoothManager.getAdapter();
	
		if (!getPackageManager().hasSystemFeature(
				PackageManager.FEATURE_BLUETOOTH_LE)) {
			Toast.makeText(this, "Ble not supported", Toast.LENGTH_SHORT)
					.show();
			finish();
		}
		
		Intent gattServiceIntent = new Intent(MainActivity.this,
				BluetoothLeService.class);
		bindService(gattServiceIntent, mServiceConnection, BIND_AUTO_CREATE);
		
		locationHandler = new Handler();
		locationHandler.postDelayed(new Runnable()
		{

			@Override
			public void run() {
				LocationInformation lI = new LocationInformation(Config.context);
				lI.setupProvider();
				locationHandler.postDelayed(this, 450000);
			}
			
		}, 6000);
		//cH = new CleverHelper("testers.db", "piuzd14d1da153d7e0982b169b8b87455d57d", this);
		//cH = new CleverHelper("wertfsdfs.db", "lafon34b520180254a9650307f0873860f218", this);
		b = (Button)findViewById(R.id.button1);
		setUpCamera();
		//HttpServerGet htpD = new HttpServerGet("api/hitchBOT?HitchBotID=5");
        //Log.i("Getting Location",htpD.getData());
		mTts = new TextToSpeech(MainActivity.this, new TextToSpeech.OnInitListener()
				{
			@Override
		      public void onInit(int status) {
		         if(status != TextToSpeech.ERROR){
		             mTts.setLanguage(Locale.CANADA);
		             mTts.setPitch((float) 1);
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
        captions.put(MAIN_SEARCH, R.string.forecast_caption);
      //  captions.put(HELLO_WORLD,R.string.hello_world);
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

	                    switchSearch(MAIN_SEARCH);
	                }
	            }
	            
		}.execute();
		set_poweredOn(false);
	}
	
	public void getResponseFromCleverscript(String message)
	{
		Speak(cH.cs.sendMessage(message));		

	}
	
	public void Speak(String message)
	{
		recognizer.stop();
	    HashMap<String, String> myHashAlarm = new HashMap<String, String>();
		myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_STREAM, String.valueOf(AudioManager.STREAM_ALARM));
	    myHashAlarm.put(TextToSpeech.Engine.KEY_PARAM_UTTERANCE_ID, "SOME MESSAGE");
	    if(mDevice != null){
	    isTalking();
	    }
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
	    {
	    	if (!mBluetoothAdapter.isEnabled()) {
				Intent enableBtIntent = new Intent(
						BluetoothAdapter.ACTION_REQUEST_ENABLE);
				startActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
			}

			registerReceiver(mGattUpdateReceiver, makeGattUpdateIntentFilter());
	    }
	}
	
	private void setTtsListener() {
	        int listenerResult = mTts.setOnUtteranceProgressListener(new UtteranceProgressListener()
	        {
	            @Override
	            public void onDone(String utteranceId)
	            {
	            	if(mDevice != null){
	            	stoppedTalking();
	            	}
	            	switchSearch(MAIN_SEARCH);
	            }

	            @Override
	            public void onError(String utteranceId)
	            {
	            }

	            @Override
	            public void onStart(String utteranceId)
	            {
	            	recognizer.stop();
	            	if(mDevice != null){
	            	isTalking();
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
    ((TextView) findViewById(R.id.editText2)).setText(caption);
}

	private void setupRecognizer(File assetsDir) {
    File modelsDir = new File(assetsDir, "models");
    recognizer = defaultSetup()
            .setAcousticModel(new File(modelsDir, "hmm/en-us-semi"))
            .setDictionary(new File(modelsDir, "dict/7788.dic"))
            .setRawLogDir(assetsDir).setKeywordThreshold(1e-20f)
            .getRecognizer();
    recognizer.addListener(this);

    // Create keyword-activation search.
  // recognizer.addKeyphraseSearch(KWS_SEARCH, KEYPHRASE);

    //recognizer.addGrammarSearch(DIGITS_SEARCH, digitsGrammar);
    // Create language model search.
    File languageModel = new File(modelsDir, "lm/7788.dmp");
    recognizer.addNgramSearch(MAIN_SEARCH, languageModel);
}

	@Override
	public void onBeginningOfSpeech() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onEndOfSpeech() {
		if (MAIN_SEARCH.equals(recognizer.getSearchName()))
        	recognizer.stop();
            switchSearch(MAIN_SEARCH);  	
	}

	@Override
	public void onPartialResult(Hypothesis hypothesis) {
			String text = hypothesis.getHypstr();

            ((TextView) findViewById(R.id.editText2)).setText(text);
           // mTts.speak(text, TextToSpeech.QUEUE_FLUSH, null);
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
			new UploadImageImgur(imageUri, getThis(), getThis()).execute();
			
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
	     mHandler2.post(new Runnable() {
	        @Override
	        public void run() {
	        	if (device != null) {
					if (device.getName().contains("Shield")
							|| device.getName().contains("Biscuit")) {
						mDevice = device;
					}
	        	}
	        }
	    });
	}
	};

	public void isTalking()
	{
		byte buf[] = new byte[] { (byte) 0x01, (byte) 0x00, (byte) 0x00 };
		characteristicTx.setValue(buf);
		mBluetoothLeService.writeCharacteristic(characteristicTx);
	}

	public void stoppedTalking()
	{
		byte buf[] = new byte[] { (byte) 0x00, (byte) 0x00, (byte) 0x00 };
		characteristicTx.setValue(buf);
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
						mDeviceAddress = mDevice.getAddress();
						mBluetoothLeService.connect(mDeviceAddress);
						scanFlag = true;
					} else {
						runOnUiThread(new Runnable() {
							public void run() {
								Toast toast = Toast
										.makeText(
												MainActivity.this,
												"Couldn't search Ble Shielded device!",
												Toast.LENGTH_SHORT);
								toast.setGravity(0, 0, Gravity.CENTER);
								toast.show();
							}
						});
					}
				}
			}, SCAN_PERIOD);
		}

		//System.out.println(connState);
		if (connState == false) {
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
	
	}

