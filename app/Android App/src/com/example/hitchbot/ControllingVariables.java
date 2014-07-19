package com.example.hitchbot;

public static class ControllingVariables {

	
	//variables received
	

	public String POST_CHANGE = "";
	public String GET_CHANGE = "";
	public boolean RESTART_APPLCIATION = false;
	public int TIME_TO_GRAB_LOCATION = 90000;
	
	
	//variables sent
	
	//tihis constructor checks server for updates, and if not available defaults to values above
	
	public ControllingVariables()
	{
		
	}
	
	
}
