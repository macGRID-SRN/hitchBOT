package com.example.hitchbot.Speech;

import com.example.hitchbot.Config;

public class SpeechIn {

	private boolean isListening = false;
	private OnlineRecognizer onlineRecognizer;
	private OfflineRecognizer offlineRecognizer;
	
	public SpeechIn() {
		onlineRecognizer = new OnlineRecognizer();
		offlineRecognizer = new OfflineRecognizer();
		//switchSearch(
			//	Config.searchName);
	}
	
	public void setIsListening(boolean isListening)
	{
		this.isListening = isListening;
	}

	public boolean getIsListening() {
		return isListening;
	}
	
	public void pauseRecognizer() {
		offlineRecognizer.pauseRecognizer();
		onlineRecognizer.pauseRecognizer();
	}

	public OnlineRecognizer getOnline()
	{
		return onlineRecognizer;
	}
	
	public OfflineRecognizer getOffline()
	{
		return offlineRecognizer;
	}
	
	public void switchSearch(String searchName) {
		if (Config.networkAvailable()) {
			onlineRecognizer.startListening();
		} else {
			offlineRecognizer.startListening(searchName);
		}
	}


	


}
