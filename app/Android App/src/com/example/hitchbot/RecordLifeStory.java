package com.example.hitchbot;

import java.io.IOException;

import android.media.MediaRecorder;
import android.os.Environment;
import android.os.Handler;
import android.util.Log;

public class RecordLifeStory {

	 private MediaRecorder mRecorder = null;
	 private static final String TAG = "AudioRecordTest";
	 private static String mFileName = null;
	 private Handler recorderHandler;


	
	   public RecordLifeStory() {
	        mFileName = Environment.getExternalStorageDirectory().getAbsolutePath();
	        mFileName += "/" +Config.getUtcDate() +"lifeStory.3gp";
	        recorderHandler = new Handler();
	    }
	   
    private void startRecording() {
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
	
    private void stopRecording() {
        mRecorder.stop();
        mRecorder.release();
        mRecorder = null;
    }
    
    public void recordThirty()
    {
    	recorderHandler.post(new Runnable()
    	{

			@Override
			public void run() {
				startRecording();
				try {
					Thread.sleep(30000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				stopRecording();

			}
    		
    	});
    }
}
