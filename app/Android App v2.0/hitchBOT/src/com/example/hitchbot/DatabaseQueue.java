package com.example.hitchbot;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.locks.ReadWriteLock;
import java.util.concurrent.locks.ReentrantReadWriteLock;

import com.example.hitchbot.Models.HttpPostDb;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class DatabaseQueue extends SQLiteOpenHelper{
	
	public static final String TABLE_HTTPPOSTQUEUE = "HttpPostQueue";
	public static final String COLUMN_POSTID = "_id";
	public static final String COLUMN_URI = "URI";
	public static final String COLUMN_UPLOAD_TO_SERVER = "UploadToServer";
	public static final String COLUMN_DATE = "DateCreated";
	
	private static final String DATABASE_NAME = "hitchBOT.db";
	private static final int DATABASE_VERSION = 1;
	
	//because writing to sqlite across threads sucks
	private static final ReadWriteLock rwLock = new ReentrantReadWriteLock(true);
	private static DatabaseQueue db;
	
	public DatabaseQueue(Context context) {
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
	}
	
	 private static final String HTTPPOST_TABLE_CREATE = "CREATE TABLE "
		      + TABLE_HTTPPOSTQUEUE + "(" + COLUMN_POSTID
		      + " INTEGER PRIMARY KEY AUTOINCREMENT, " + COLUMN_URI
		      + " INTEGER," + COLUMN_UPLOAD_TO_SERVER+ " INTEGER," +COLUMN_DATE +" TEXT"+");";

	@Override
	public void onCreate(SQLiteDatabase db) {
		db.execSQL(HTTPPOST_TABLE_CREATE);
		
	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		db.execSQL("DROP TABLE IF EXISTS " + TABLE_HTTPPOSTQUEUE);		
        onCreate(db);		

	}
	
	public List<HttpPostDb> serverAudioUploadQueue()
	{
		
		List<HttpPostDb> databasePosts = new ArrayList<HttpPostDb>();
		SQLiteDatabase database = null;
		try
		{
			beginReadLock();
			String queryString = "SELECT * FROM " + TABLE_HTTPPOSTQUEUE + " WHERE " + COLUMN_UPLOAD_TO_SERVER+ " = 3";
			SQLiteDatabase db = this.getReadableDatabase();
			Cursor cursor = db.rawQuery(queryString, null);
			if(cursor.moveToFirst())
			{
				do{
					HttpPostDb htpD = new HttpPostDb();
					htpD.setID(Integer.parseInt(cursor.getString(0)));
					htpD.setUri(cursor.getString(1));
					htpD.setUploadToServer(Integer.parseInt(cursor.getString(2)));
					htpD.setDateCreated(cursor.getString(3));
					
					databasePosts.add(htpD);
					
				}while(cursor.moveToNext());
				cursor.close();

			}
		}catch (Exception e)
		{
			
		}
		finally{
			if(database!= null)
			{
	               try
	                {
	           		database.close();
	                }
	                catch (Exception e) {}
			}
			endReadLock();
		}
		return databasePosts;
		
	}
	
	public void addItemToQueue(HttpPostDb httpPost)
	{
		SQLiteDatabase database = null;

		try
		{
			beginWriteLock();
			database = this.getWritableDatabase();
			ContentValues newRecord = new ContentValues();
			
			newRecord.put(COLUMN_URI, httpPost.getUri());
			newRecord.put(COLUMN_UPLOAD_TO_SERVER, httpPost.getUploadToServer());
			newRecord.put(COLUMN_DATE, httpPost.getDateCreated());
			
			database.insert(TABLE_HTTPPOSTQUEUE, null, newRecord);
		}catch (Exception e)
		{
			
		}
		finally{
			if(database!= null)
			{
	               try
	                {
	                    database.close();
	                }
	                catch (Exception e) {}
			}
			endWriteLock();
		}
		
	}
	
	
	public void markAsUploadedToServer(HttpPostDb httpPost)
	{		
		SQLiteDatabase database = null;

		try
		{
			beginWriteLock();
			database = this.getWritableDatabase();
			database.delete(TABLE_HTTPPOSTQUEUE, COLUMN_POSTID + " = " + httpPost.getID(), null);
		}catch (Exception e)
		{
			
		}
		finally{
			if(database!= null)
			{
	               try
	                {
	                    database.close();
	                }
	                catch (Exception e) {}
			}
			endWriteLock();
		}
	}
	
    private static void beginReadLock()
    {
        rwLock.readLock().lock();
    }

    private static void endReadLock()
    {
        rwLock.readLock().unlock();
    }

    private static void beginWriteLock()
    {
        rwLock.writeLock().lock();
    }

    private static void endWriteLock()
    {
        rwLock.writeLock().unlock();
    }
    
    public static synchronized DatabaseQueue getHelper(Context context)
    {
        if (db == null)
            db = new DatabaseQueue(context);

        return db;
    }
    
	//For testing only
	public void launchMissles()
	{
		//dangerous use only in extreme circumstances
		SQLiteDatabase db = this.getWritableDatabase();
		db.delete(TABLE_HTTPPOSTQUEUE, null, null);
		db.close();
	}
	
}
