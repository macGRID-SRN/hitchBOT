package com.example.hitchbot;

import android.hardware.Camera;
import android.hardware.Camera.AutoFocusCallback;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

public class CameraPreview extends SurfaceView implements SurfaceHolder.Callback{

	//Leaving in depricated camera API because new camera API isn't supported
	//On android 4.4.x
	private SurfaceHolder mHolder;
	public Camera camera = null;
	boolean takePicture = false;
	HitchActivity context;

	public CameraPreview(HitchActivity context) {
		super(context);
		mHolder = getHolder();
		mHolder.addCallback(this);
		this.context = context;
}
		
	@SuppressWarnings("deprecation")
	@Override
	public void surfaceCreated(SurfaceHolder holder) {
		camera = getCameraInstance();

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
		camera.setDisplayOrientation(90);
		params.setWhiteBalance(Camera.Parameters.WHITE_BALANCE_DAYLIGHT);
		camera.setParameters(params);
		camera.startPreview();

	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {

		camera.stopPreview();
		camera.release();
		camera = null;
	}
	
	public AutoFocusCallback _autoCallback = new AutoFocusCallback()
	{

		@Override
		public void onAutoFocus(boolean success, Camera camera) {
			camera.takePicture(null, null, context.jpegHandler);

		}
		
	};
	
	public void capture(Camera.PictureCallback jpegHandler)
	{		
		camera.autoFocus(_autoCallback);

	}

	private static Camera getCameraInstance(){
	    Camera c = null;
	    try {
	        c = Camera.open(); // attempt to get a Camera instance
	    }
	    catch (Exception e){
	        // Camera is not available (in use or does not exist)
	    }
	    return c; // returns null if camera is unavailable
	}
	


}
