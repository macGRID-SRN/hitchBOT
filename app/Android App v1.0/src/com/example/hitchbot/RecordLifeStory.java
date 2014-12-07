package com.example.hitchbot;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import android.media.MediaRecorder;
import android.os.Environment;
import android.os.Handler;
import android.util.Log;

public class RecordLifeStory {

	 private MediaRecorder mRecorder = null;
	 private static final String TAG = "AudioRecordTest";
	 public String mFileName = null;
	 private Handler recorderHandler;


	
	   public RecordLifeStory() {
	        mFileName = Environment.getExternalStorageDirectory().getAbsolutePath();
	        mFileName += "/" +Config.getUtcDate() +"lifeStory.3gp";
	        recorderHandler = new Handler();
	    }
	   
    public void startRecording() {
        mRecorder = new MediaRecorder();
        mRecorder.setAudioSource(MediaRecorder.AudioSource.MIC);
        mRecorder.setOutputFormat(MediaRecorder.OutputFormat.THREE_GPP);
        mRecorder.setOutputFile(mFileName);
        mRecorder.setAudioEncoder(MediaRecorder.AudioEncoder.AMR_NB);

        try {
            mRecorder.prepare();
        } catch (IOException e) {
            Log.e(TAG, "prepare() failed");
        }

        mRecorder.start();
    }
	
    public void stopRecording() {
        mRecorder.stop();
        mRecorder.release();
        mRecorder = null;
    }
    
    public void recordThirty()
    {
		startRecording();

    	recorderHandler.postDelayed(new Runnable()
    	{
			@Override
			public void run() {
				stopRecording();

			}
    		
    	}, 1000*30);
    }
    


}
