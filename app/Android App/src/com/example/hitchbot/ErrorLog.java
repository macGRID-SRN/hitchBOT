package com.example.hitchbot;

public class ErrorLog {

	private String ErrorMessage;
	// 0 is not successful, 1 is successful (sql lite doesn't take bool type)
	private int successfulUpload;
	private int iD;
	private String creationDate;
	
	public ErrorLog(){}
	
	public ErrorLog(String ErrorMessage, int successfulUpload)
	{
		this.ErrorMessage = ErrorMessage;
		this.successfulUpload = successfulUpload;
	}
	
	public ErrorLog(String ErrorMessage, int successfulUpload, int iD, String creationDate)
	{
		this.ErrorMessage = ErrorMessage;
		this.successfulUpload = successfulUpload;
		this.iD = iD;
		this.creationDate = creationDate;
	}

	public String getErrorMessage() {
		return ErrorMessage;
	}

	public void setErrorMessage(String errorMessage) {
		ErrorMessage = errorMessage;
	}

	public int getSuccessfulUpload() {
		return successfulUpload;
	}

	public void setSuccessfulUpload(int successfulUpload) {
		this.successfulUpload = successfulUpload;
	}

	public int getiD() {
		return iD;
	}

	public void setiD(int iD) {
		this.iD = iD;
	}

	public String getCreationDate() {
		return creationDate;
	}

	public void setCreationDate(String creationDate) {
		this.creationDate = creationDate;
	}

	
}
