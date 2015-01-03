package com.example.hitchbot;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

import com.example.hitchbot.Models.FileUploadDb;
import com.example.hitchbot.Models.HttpPostDb;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.hardware.Camera;
import android.net.Uri;
import android.os.Environment;
import android.util.Log;
import android.widget.FrameLayout;
import android.widget.ImageView;

@SuppressWarnings("deprecation")
public class TakePicture {

	private CameraPreview cameraPreview;
	private ImageView imageResult;
	private FrameLayout frameNew;
	private boolean takePicture = false;
	private static String TAG = "TakePicture";
	private Bitmap image = null;

	public TakePicture() {
		setUpCamera();
	}

	private void setUpCamera() {
		cameraPreview = new CameraPreview((HitchActivity) Config.context, this);
		imageResult = new ImageView(
				Config.context.getApplicationContext());

		imageResult.setBackgroundColor(Color.GRAY);
		frameNew = (FrameLayout) ((HitchActivity) Config.context)
				.findViewById(R.id.frameLayout);
		frameNew.addView(imageResult);
		frameNew.addView(cameraPreview);

		frameNew.bringChildToFront(imageResult);
	}

	public void captureHandler() {
		if (takePicture) {

			cameraPreview.capture(jpegHandler);

		} else {
			takePicture = true;
			frameNew.bringChildToFront(cameraPreview);
			imageResult.setImageBitmap(null);
			try {
				cameraPreview.camera.startPreview();
				Thread.sleep(1000);
				captureHandler();
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

	public Camera.PictureCallback jpegHandler = new Camera.PictureCallback() {

		@Override
		public void onPictureTaken(byte[] data, Camera camera) {
			image = BitmapFactory.decodeByteArray(data, 0, data.length);
			imageResult.setImageBitmap(image);
			frameNew.bringChildToFront(imageResult);
			takePicture = false;
			File pictureFile = getOutputMediaFile();
			Uri imageUri = Uri.fromFile(pictureFile);
			Log.i(TAG, imageUri.toString());
			FileUploadDb insert = new FileUploadDb(pictureFile.getAbsolutePath(), 0,1);
			Config.dQ.addItemToQueue(insert);
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
				Log.d(TAG, "failed to create directory");
				return null;
			}
		}
		// Create a media file name
		String timeStamp = Config.getUtcDate();
		File mediaFile;
		mediaFile = new File(mediaStorageDir.getPath() + File.separator
				+ "IMG_" + timeStamp + ".jpg");

		return mediaFile;
	}
}
