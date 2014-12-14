package com.example.hitchbot;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.widget.FrameLayout;
import android.widget.ImageView;

public class TakePicture {
	
 	private CameraPreview cameraPreview;
    private ImageView imageResult;
    private FrameLayout frameNew;
    private boolean takePicture = false;
    
	
	public TakePicture()
	{
		setUpCamera();
	}
	
	private void setUpCamera()
	{
		cameraPreview = new CameraPreview((HitchActivity)Config.context);
		imageResult = new ImageView((HitchActivity)Config.context.getApplicationContext());
		
		imageResult.setBackgroundColor(Color.GRAY);
		frameNew = (FrameLayout) ((HitchActivity)Config.context).findViewById(R.id.frameLayout);
		frameNew.addView(imageResult);
		frameNew.addView(cameraPreview);
		
		frameNew.bringChildToFront(imageResult);
	}
}
