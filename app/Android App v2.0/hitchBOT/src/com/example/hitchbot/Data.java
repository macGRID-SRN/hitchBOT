package com.example.hitchbot;

import java.util.List;

import org.apache.http.NameValuePair;

public class Data {
	
	private String uri;
	private List<NameValuePair> header;
	private List<NameValuePair> body;
	
	public Data()
	{
	}

	public String getUri() {
		return uri;
	}

	public void setUri(String uri) {
		this.uri = uri;
	}

	public List<NameValuePair> getHeader() {
		return header;
	}

	public void setHeader(List<NameValuePair> header) {
		this.header = header;
	}

	public List<NameValuePair> getBody() {
		return body;
	}

	public void setBody(List<NameValuePair> body) {
		this.body = body;
	}
	
	
	
}
