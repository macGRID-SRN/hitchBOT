package com.example.hitchbot;

import android.util.Log;

public class ConversationPost {

	String hitchbotHeard;
	String hitchbotSaid;
	String apiSaid = "http://hitchbotapi.azurewebsites.net/api/Conversation?HitchBotId=%s&SpeechSaid=%s&TimeTaken=%s";
	String apiHeard = "http://hitchbotapi.azurewebsites.net/api/Conversation?HitchBotId=%s&SpeechHeard=%s&TimeTaken=%s";
	String apiStart = "http://hitchbotapi.azurewebsites.net/api/Conversation?HitchBotID=%s&StartTime=%s";
	DatabaseQueue dQ;
	
	public ConversationPost(String spokenPhrase, boolean hitchbotSpoke)
	{
		dQ = DatabaseQueue.getHelper(Config.context);
		if(hitchbotSpoke)
		{
			this.hitchbotSaid = spokenPhrase;
			storeSaid();
		}
		else
		{
			this.hitchbotHeard = spokenPhrase;
			storeHeard();
		}
	}
	
	public ConversationPost()
	{
		dQ = DatabaseQueue.getHelper(Config.context);
	}
	
	public void conversationStart()
	{
		String id = Config.HITCHBOT_ID;
		String time = Config.getUtcDate();
		HttpPostDb hPd = new HttpPostDb(String.format(apiStart, id, time), 2,0);
		dQ.addItemToQueue(hPd);
	}
	
	public void storeSaid( )
	{
		
		String id = Config.HITCHBOT_ID;
		hitchbotSaid = hitchbotSaid.replaceAll(" ", "%20");
    	Log.i("FileDeleted", hitchbotSaid);

		String timeSaid = Config.getUtcDate();
		HttpPostDb hPd = new HttpPostDb(String.format(apiSaid, id, hitchbotSaid, timeSaid), 2, 0);
		dQ.addItemToQueue(hPd);
	}
	
	public void storeHeard() 
	{
		String id = Config.HITCHBOT_ID;
		hitchbotHeard = hitchbotHeard.replaceAll(" ", "%20");
    	Log.i("FileDeleted", hitchbotHeard);

		String timeHeard = Config.getUtcDate();
		HttpPostDb hPd = new HttpPostDb(String.format(apiHeard, id, hitchbotHeard, timeHeard), 2, 0);
		dQ.addItemToQueue(hPd);	
		
	}
	
}
