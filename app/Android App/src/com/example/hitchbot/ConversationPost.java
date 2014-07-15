package com.example.hitchbot;

public class ConversationPost {

	String hitchbotHeard;
	String hitchbotSaid;
	String apiSaid = "http://hitchbotapi.azurewebsites.net/api/Conversation?convID=%s&SpeechSaid=%s&TimeTaken=%s";
	String apiHeard = "http://hitchbotapi.azurewebsites.net/api/Conversation?convID=%s&SpeechHeard=%s&TimeTaken=%s";
	
	public ConversationPost(String spokenPhrase, boolean hitchbotSpoke)
	{
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
	
	public void storeSaid( )
	{
		String id = Config.HITCHBOT_ID;
		String said = hitchbotSaid;
		String timeSaid = Config.getUtcDate();
		HttpPostDb hPd = new HttpPostDb(String.format(apiSaid, id, said, timeSaid), 2, 0);
		DatabaseQueue dQ = new DatabaseQueue(Config.context);
		dQ.addItemToQueue(hPd);
	}
	
	public void storeHeard() 
	{
		String id = Config.HITCHBOT_ID;
		String heard = hitchbotHeard;
		String timeHeard = Config.getUtcDate();
		HttpPostDb hPd = new HttpPostDb(String.format(apiHeard, id, heard, timeHeard), 2, 0);
		DatabaseQueue dQ = new DatabaseQueue(Config.context);
		dQ.addItemToQueue(hPd);	
		
	}
	
}
