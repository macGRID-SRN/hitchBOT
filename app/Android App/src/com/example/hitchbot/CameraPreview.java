package com.example.hitchbot;


import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.hardware.Camera;
import android.hardware.Camera.AutoFocusCallback;
import android.media.FaceDetector;
import android.media.FaceDetector.Face;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

public class CameraPreview extends SurfaceView implements SurfaceHolder.Callback  {

	private SurfaceHolder mHolder;
	public Camera camera = null;
	boolean takePicture = false;
	MainActivity context;
	private static final int MAX_FACES = 5;

	public CameraPreview(MainActivity context) {
		super(context);
		mHolder = getHolder();
		mHolder.addCallback(this);
		this.context = context;
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
		camera.setDisplayOrientation(90);
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
			camera.autoFocus(null);
			camera.takePicture(null, null, context.jpegHandler);
			camera.release();
		}
		
	};
	
	
	
	public void capture(Camera.PictureCallback jpegHandler)
	{		
		camera.autoFocus(_autoCallback);
	}


	public int detectFaces(Bitmap cameraBitmap){
	    if(null != cameraBitmap){
	            int width = cameraBitmap.getWidth();
	            int height = cameraBitmap.getHeight();
	            
	            FaceDetector detector = new FaceDetector(width, height,MAX_FACES);
	            Face[] faces = new Face[MAX_FACES];
	            
	            Bitmap bitmap565 = Bitmap.createBitmap(width, height, Config.RGB_565);
	            Paint ditherPaint = new Paint();
	            Paint drawPaint = new Paint();
	            
	            ditherPaint.setDither(true);
	            drawPaint.setColor(Color.RED);
	            drawPaint.setStyle(Paint.Style.STROKE);
	            drawPaint.setStrokeWidth(2);
	            
	            Canvas canvas = new Canvas();
	            canvas.setBitmap(bitmap565);
	            canvas.drawBitmap(cameraBitmap, 0, 0, ditherPaint);
	            
	            int facesFound = detector.findFaces(bitmap565, faces);
	        //    PointF midPoint = new PointF();
	        //    float eyeDistance = 0.0f;
	        //    float confidence = 0.0f;
	            
	       //     Log.i("FaceDetector", "Number of faces found: " + facesFound);
	            
	         /*   if(facesFound > 0)
	            {
	                    for(int index=0; index<facesFound; ++index){
	                            faces[index].getMidPoint(midPoint);
	                            eyeDistance = faces[index].eyesDistance();
	                            confidence = faces[index].confidence();
	                            
	                            Log.i("FaceDetector", 
	                                            "Confidence: " + confidence + 
	                                            ", Eye distance: " + eyeDistance + 
	                                            ", Mid Point: (" + midPoint.x + ", " + midPoint.y + ")");
	                            
	                            canvas.drawRect((int)midPoint.x - eyeDistance , 
	                                                            (int)midPoint.y - eyeDistance , 
	                                                            (int)midPoint.x + eyeDistance, 
	                                                            (int)midPoint.y + eyeDistance, drawPaint);
	                    }
	            }*/
	            return facesFound;
	            

	    }
		return 0;
	}
	
}



	

	
	

