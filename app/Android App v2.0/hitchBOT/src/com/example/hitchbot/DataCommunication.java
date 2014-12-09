package com.example.hitchbot;

import android.os.AsyncTask;

public class DataCommunication extends AsyncTask<Data, Void, Boolean> {

	public DataCommunication()
	{
		
	}
	
	@Override
	protected Boolean doInBackground(Data... params) {
		// TODO Auto-generated method stub
		return null;
	}
	
	@Override
	protected void onPostExecute(Boolean result) {
		if(result)
		{
			//ok
		}
		else
		{
			//add back to queue
		}
	}

}
