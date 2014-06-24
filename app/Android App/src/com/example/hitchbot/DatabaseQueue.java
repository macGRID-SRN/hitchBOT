package com.example.hitchbot;

import java.util.List;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;
import android.provider.BaseColumns;

public class DatabaseQueue extends SQLiteOpenHelper {

	public static final String TABLE_HTTPPOSTQUEUE = "HttpPostQueue";
	public static final String COLUMN_POSTID = "_postID";
	public static final String COLUMN_URI = "URI";
	public static final String COLUMN_UPLOAD_TO_SERVER = "UploadToServer";
	public static final String COLUMN_UPLOAD_TO_IMGUR = "UploadToImgur";
	public static final String COLUMN_DATE = "DateCreated";
	
	public static final String TABLE_ERRORLOG = "ErrorLog";
	public static final String COLUMN_ERRORID = "ErrorID";
	public static final String COLUMN_ERRORMESSAGE = "ErrorMessage";
	public static final String COLUMN_ERROR_UPLOAD_TO_SERVER = "ErrorUploadSuccessful";
	public static final String COLUMN_DATELOGGED = "DateLogged";
	
	private static final String DATABASE_NAME = "hitchBOT.db";
	private static final int DATABASE_VERSION = 1;
	
	
	
	public DatabaseQueue(Context context, String name, CursorFactory factory ,
			int version) {
		super(context, name, factory, version);
		// TODO Auto-generated constructor stub
	}
	
	 private static final String HTTPPOST_TABLE_CREATE = "CREATE TABLE "
		      + TABLE_HTTPPOSTQUEUE + "(" + COLUMN_POSTID
		      + " INTEGER PRIMARY KEY AUTOINCREMENT, " + COLUMN_URI
		      + " TEXT," +COLUMN_UPLOAD_TO_IMGUR + " INTEGER," + COLUMN_UPLOAD_TO_SERVER+ " INTEGER," +COLUMN_DATE +" DATETIME"+");";
	 
	 private static final String ERRORLOG_TABLE_CREATE = "CREATE TABLE "
		      + TABLE_ERRORLOG + "(" + COLUMN_ERRORID
		      + " INTEGER PRIMARY KEY AUTOINCREMENT, " + COLUMN_ERRORMESSAGE
		      + " TEXT," + COLUMN_ERROR_UPLOAD_TO_SERVER+ " INTEGER," +COLUMN_DATELOGGED +" DATETIME"+");";

	 
	@Override
	public void onCreate(SQLiteDatabase db) {
		db.execSQL(HTTPPOST_TABLE_CREATE);
		db.execSQL(ERRORLOG_TABLE_CREATE);
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // on upgrade drop older tables
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_HTTPPOSTQUEUE);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_ERRORLOG);
 
        // create new tables
        onCreate(db);		
	}

	public List<HttpPostDb> imgurUploadQueue()
	{
		return null;
	}
	
	public List<HttpPostDb> serverImageLinkUploadQueue()
	{
		return null;
	}
	
	public List<ErrorLog> errorLogUploadQueue()
	{
		return null;
	}
	
	public void markImageAsUploadedToImgur()
	{
		
	}
	
	public void markItemAsUploadedToServer()
	{
		
	}
	
	public void markErrorLogAsUploadedToServer()
	{
		
	}
	
	public void addItemToQueue(HttpPostDb httpPost)
	{
		
	}
	
	public void addItemToQueue(ErrorLog errorLog)
	{
		
	}
}
