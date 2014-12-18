package com.example.hitchbot.Models;

import java.util.Date;

import com.example.hitchbot.Config;

public class HttpPostDb {

	private int ID;
	private String uri;
	//0 is no, 1 is yes
	private int uploadToServer;
	private String dateCreated;
	
	public HttpPostDb(int ID, String uri, int uploadToServer, String dateCreated)
	{
		this.ID = ID;
		this.uploadToServer = uploadToServer;
		this.setUri(uri);
		this.dateCreated = dateCreated;
	}
	
	public HttpPostDb(String uri, int uploadToServer)
	{
		this.uploadToServer = uploadToServer;
		this.setUri(uri);
		this.dateCreated = Config.getUtcDate();
	}
	
	public HttpPostDb()
	{
		
	}
	
	public int getUploadToServer() {
		return uploadToServer;
	}
	public void setUploadToServer(int uploadToServer) {
		this.uploadToServer = uploadToServer;
	}
	public int getID() {
		return ID;
	}
	public void setID(int iD) {
		ID = iD;
	}
	public String getDateCreated() {
		return dateCreated;
	}
	public void setDateCreated(String dateCreated) {
		this.dateCreated = dateCreated;
	}

	public String getUri() {
		return uri;
	}

	public void setUri(String uri) {
		this.uri = uri;
	}

}
