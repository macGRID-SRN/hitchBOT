package com.example.hitchbot;

import java.io.IOException;

import android.content.Context;
import android.hardware.Camera;
import android.util.Log;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

public class CameraPreview extends SurfaceView implements SurfaceHolder.Callback  {

	private SurfaceHolder mHolder;
	public Camera camera = null;
	
	public CameraPreview(Context context) {
		super(context);
		mHolder = getHolder();
		mHolder.addCallback(this);
}

	@Override
	public void surfaceCreated(SurfaceHolder holder) {
		camera = Camera.open();

		try{
			camera.setPreviewDisplay(mHolder);
		}catch(Exception e){
			
		}
	}

	@Override
	public void surfaceChanged(SurfaceHolder holder, int format, int width,
			int height) {
		Camera.Parameters params = camera.getParameters();
		//params.setPreviewSize(width, height);
		camera.setParameters(params);
		camera.startPreview();
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		camera.stopPreview();
		camera = null;
	}
	
	public void capture(Camera.PictureCallback jpegHandler)
	{
		
		camera.takePicture(null, null, jpegHandler);
	}

	
	
}
