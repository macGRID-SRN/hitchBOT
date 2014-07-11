package com.example.hitchbot;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class DatabaseQueue extends SQLiteOpenHelper {

	public static final String TABLE_HTTPPOSTQUEUE = "HttpPostQueue";
	public static final String COLUMN_POSTID = "_id";
	public static final String COLUMN_URI = "URI";
	public static final String COLUMN_UPLOAD_TO_SERVER = "UploadToServer";
	public static final String COLUMN_UPLOAD_TO_IMGUR = "UploadToImgur";
	public static final String COLUMN_DATE = "DateCreated";
	
	public static final String TABLE_ERRORLOG = "ErrorLog";
	public static final String COLUMN_ERRORID = "_id";
	public static final String COLUMN_ERRORMESSAGE = "ErrorMessage";
	public static final String COLUMN_ERROR_UPLOAD_TO_SERVER = "ErrorUploadSuccessful";
	public static final String COLUMN_DATELOGGED = "DateLogged";
	
	private static final String DATABASE_NAME = "hitchBOT.db";
	private static final int DATABASE_VERSION = 1;
	
	private SQLiteDatabase database;
	private Context context;
	
	public DatabaseQueue(Context context) {
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
		this.context = context;
		// TODO Auto-generated constructor stub
	}
	
	 private static final String HTTPPOST_TABLE_CREATE = "CREATE TABLE "
		      + TABLE_HTTPPOSTQUEUE + "(" + COLUMN_POSTID
		      + " INTEGER PRIMARY KEY AUTOINCREMENT, " + COLUMN_URI
		      + " TEXT," +COLUMN_UPLOAD_TO_IMGUR + " INTEGER," + COLUMN_UPLOAD_TO_SERVER+ " INTEGER," +COLUMN_DATE +" TEXT"+");";
	 
	 private static final String ERRORLOG_TABLE_CREATE = "CREATE TABLE "
		      + TABLE_ERRORLOG + "(" + COLUMN_ERRORID
		      + " INTEGER PRIMARY KEY AUTOINCREMENT, " + COLUMN_ERRORMESSAGE
		      + " TEXT," + COLUMN_ERROR_UPLOAD_TO_SERVER+ " INTEGER," +COLUMN_DATELOGGED +" TEXT"+");";

	 
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
		List<HttpPostDb> databasePosts = new ArrayList<HttpPostDb>();
		String queryString = "SELECT * FROM " + TABLE_HTTPPOSTQUEUE + " WHERE " + COLUMN_UPLOAD_TO_IMGUR+ " = 0";
		
		SQLiteDatabase db = this.getWritableDatabase();
		Cursor cursor = db.rawQuery(queryString, null);
		if(cursor.moveToFirst())
		{
			do{
				HttpPostDb htpD = new HttpPostDb();
				htpD.setPostID(Integer.parseInt(cursor.getString(0)));
				htpD.setURI(cursor.getString(1));
				htpD.setUploadToImgurSuccessful(Integer.parseInt(cursor.getString(2)));
				htpD.setUploadToServerSuccessful(Integer.parseInt(cursor.getString(3)));
				htpD.setCreationDate(cursor.getString(4));
				
				databasePosts.add(htpD);
				
			}while(cursor.moveToNext());
		}
		cursor.close();
		db.close();
		return databasePosts;
	}
	public List<HttpPostDb> serverImageLinkUploadQueue()
	{
		List<HttpPostDb> databasePosts = new ArrayList<HttpPostDb>();
		String queryString = "SELECT * FROM " + TABLE_HTTPPOSTQUEUE + " WHERE " + COLUMN_UPLOAD_TO_SERVER+ " = 0";
		SQLiteDatabase db = this.getWritableDatabase();
		Cursor cursor = db.rawQuery(queryString, null);
		if(cursor.moveToFirst())
		{
			do{
				HttpPostDb htpD = new HttpPostDb();
				htpD.setPostID(Integer.parseInt(cursor.getString(0)));
				htpD.setURI(cursor.getString(1));
				htpD.setUploadToImgurSuccessful(Integer.parseInt(cursor.getString(2)));
				htpD.setUploadToServerSuccessful(Integer.parseInt(cursor.getString(3)));
				htpD.setCreationDate(cursor.getString(4));
				
				databasePosts.add(htpD);
				
			}while(cursor.moveToNext());
		}
		cursor.close();
		db.close();
		return databasePosts;
	}
	
	public List<ErrorLog> errorLogUploadQueue()
	{
		List<ErrorLog> eL = new ArrayList<ErrorLog>();
		String queryString = "SELECT * FROM " + TABLE_ERRORLOG + " WHERE " + COLUMN_ERROR_UPLOAD_TO_SERVER + " = 0";
		SQLiteDatabase db = this.getWritableDatabase();
		Cursor cursor = db.rawQuery(queryString, null);
		if(cursor.moveToFirst())
		{
			do
			{
				ErrorLog errorLog = new ErrorLog();
				errorLog.setiD(Integer.parseInt(cursor.getString(0)));
				errorLog.setErrorMessage(cursor.getString(1));
				errorLog.setSuccessfulUpload(Integer.parseInt(cursor.getString(2)));
				errorLog.setCreationDate(cursor.getString(3));
				eL.add(errorLog);
				
			}while(cursor.moveToNext());
		}
		cursor.close();
		db.close();
		return eL;
	}
	
	public void markAsUploadedToImgur(HttpPostDb httpPost)
	{
		SQLiteDatabase db = this.getWritableDatabase();
		String where = " where " + COLUMN_POSTID + " = " + httpPost.getPostID();
		ContentValues con = new ContentValues();
		con.put(COLUMN_UPLOAD_TO_IMGUR, 1);
		db.update(TABLE_HTTPPOSTQUEUE, con, where, null);
		db.close();
	}
	
	public void markAsUploadedToServer(HttpPostDb httpPost)
	{
		SQLiteDatabase db = this.getWritableDatabase();
		String where = " where " + COLUMN_POSTID + " = " + httpPost.getPostID();
		ContentValues con = new ContentValues();
		con.put(COLUMN_UPLOAD_TO_SERVER, 1);
		db.update(TABLE_HTTPPOSTQUEUE, con, where, null);	
		db.close();
	}
	
	public void markAsUploadedToServer(ErrorLog errorLog)
	{
		SQLiteDatabase db = this.getWritableDatabase();
		String where = " where " + COLUMN_ERRORID + " = " + errorLog.getiD();
		ContentValues con = new ContentValues();
		con.put(COLUMN_ERROR_UPLOAD_TO_SERVER, 1);
		db.update(TABLE_ERRORLOG, con, where, null);	
		db.close();
	}
	
	public void addItemToQueue(HttpPostDb httpPost)
	{
		database = this.getWritableDatabase();
		ContentValues newRecord = new ContentValues();
		
		newRecord.put(COLUMN_URI, httpPost.getURI());
		newRecord.put(COLUMN_UPLOAD_TO_SERVER, httpPost.getUploadToServerSuccessful());
		newRecord.put(COLUMN_UPLOAD_TO_IMGUR, httpPost.getUploadToImgurSuccessful());
		newRecord.put(COLUMN_DATE, getDateTime());
		
		database.insert(TABLE_HTTPPOSTQUEUE, null, newRecord);
		//can't forget to close it
		database.close();
		
	}
	
	private String getDateTime()
	{
		SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.getDefault());
		Date date = new Date();
		return dateFormat.format(date);
	}
	
	public void addItemToQueue(ErrorLog errorLog)
	{
		database = this.getWritableDatabase();
		ContentValues newRecord = new ContentValues();
		
		newRecord.put(COLUMN_URI, errorLog.getErrorMessage());
		newRecord.put(COLUMN_UPLOAD_TO_SERVER, errorLog.getSuccessfulUpload());
		newRecord.put(COLUMN_DATE, getDateTime());
		
		database.insert(TABLE_ERRORLOG, null, newRecord);
		//can't forget to close it
		database.close();
	}
}
