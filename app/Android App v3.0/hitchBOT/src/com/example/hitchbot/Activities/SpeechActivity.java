package com.example.hitchbot.Activities;

import com.example.hitchbot.Config;
import com.example.hitchbot.R;

import Speech.CleverScriptHelper;
import Speech.SpeechController;
import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;

public class SpeechActivity extends Activity {

	SpeechController speechController;
	private boolean speechIsGo = true;
	Button speechButton;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_speech);
		Config.context = this;
		speechButton = (Button) findViewById(R.id.buttonStart);
		speechController = new SpeechController();
		speechController.startCycle();
	}

	public void modifySpeechCycle(View view) {
		if (speechIsGo) {
			speechButton.setText("Start Speech");
			speechController.stopCycle();
		} else {
			speechButton.setText("Stop Speech");
			speechController.startCycle();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.hitch, menu);
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

}
