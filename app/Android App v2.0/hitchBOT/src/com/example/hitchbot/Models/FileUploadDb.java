package com.example.hitchbot.Models;

import com.example.hitchbot.Config;

public class FileUploadDb {

	private int ID;
	private String uri;
	private int fileType;
	private int uploadToServer;
	private String dateCreated;

	public FileUploadDb(int ID, String uri, 
			int uploadToServer, String dateCreated, int uploadType)
	{
		this.setID(ID);
		this.uri = uri;
		this.uploadToServer = uploadToServer;
		this.dateCreated = dateCreated;
		this.fileType = uploadType;
	}
	
	public FileUploadDb(String uri, int uploadToServer, int uploadType) {
		this.uri = uri;
		this.uploadToServer = uploadToServer;
		this.dateCreated = Config.getUtcDate();
		this.fileType = uploadType;
	}

	public FileUploadDb() {
		// TODO Auto-generated constructor stub
	}

	public String getUri() {
		return uri;
	}

	public void setUri(String uri) {
		this.uri = uri;
	}

	public String getDateCreated() {
		return dateCreated;
	}

	public void setDateCreated(String dateCreated) {
		this.dateCreated = dateCreated;
	}

	public int getFileType() {
		return fileType;
	}

	public void setFileType(int fileType) {
		this.fileType = fileType;
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
}
