package com.example.hitchbot;

public class HttpPostDb {

		private String URI;
		//0 = false, 1 = true, 2 = irrelevent
		private int uploadToImgurSuccessful;
		// 0 = false, 1 = true
		private int uploadToServerSuccessful;
		private int _postID;
		
		public HttpPostDb(){}

		public HttpPostDb(String URI, int uploadToImgurSuccessful, int uploadToServerSuccessful)
		{
			this.URI = URI;
			this.uploadToImgurSuccessful = uploadToImgurSuccessful;
			this.uploadToServerSuccessful = uploadToServerSuccessful;
		}
		
		public HttpPostDb(String URI, int uploadToImgurSuccessful, int uploadToServerSuccessful, int _postID)
		{
			this.URI = URI;
			this.uploadToImgurSuccessful = uploadToImgurSuccessful;
			this.uploadToServerSuccessful = uploadToServerSuccessful;
			this._postID = _postID;
		}
		
		public String getURI() {
			return URI;
		}

		public void setURI(String uRI) {
			URI = uRI;
		}

		public int getUploadToImgurSuccessful() {
			return uploadToImgurSuccessful;
		}

		public void setUploadToImgurSuccessful(int UploadToImgurSuccessful) {
			uploadToImgurSuccessful = UploadToImgurSuccessful;
		}

		public int getUploadToServerSuccessful() {
			return uploadToServerSuccessful;
		}

		public void setUploadToServerSuccessful(int UploadToServerSuccessful) {
			uploadToServerSuccessful = UploadToServerSuccessful;
		}

		public int getPostID() {
			return _postID;
		}

		public void setPostID(int postID) {
			_postID = postID;
		}
		
		
	

}
