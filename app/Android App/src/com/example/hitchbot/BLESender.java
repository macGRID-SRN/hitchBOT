package com.example.hitchbot;

import android.app.ListActivity;
import android.app.Service;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothGatt;
import android.bluetooth.BluetoothGattCallback;
import android.bluetooth.BluetoothGattCharacteristic;
import android.bluetooth.BluetoothManager;
import android.bluetooth.BluetoothProfile;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.IBinder;
import android.util.Log;

	public class BLESender extends Service {

		@Override
		public IBinder onBind(Intent intent) {
			// TODO Auto-generated method stub
			return null;
		}

		/*private BluetoothAdapter bluetoothAdapter;
		private static long SCAN_TIME = 5000;
		private Handler handler;
		private boolean scanning;
		private BluetoothGatt bluetoothGatt;
		private String TAG = "BLESender";
		
		public BLESender()
		{
			final BluetoothManager bluetoothManager = (BluetoothManager) getSystemService(Context.BLUETOOTH_SERVICE);
			bluetoothAdapter = bluetoothManager.getAdapter();
		}
		
		
		private void scanLeDevice(boolean goodToScan)
		{
			if(goodToScan)
			{
				handler.postDelayed(new Runnable()
				{

					@Override
					public void run() {
						scanning = false;
						bluetoothAdapter.stopLeScan(LeScanCallback);
					}
					
				}
				, SCAN_TIME);
				
				scanning = true;
				bluetoothAdapter.startLeScan(LeScanCallback);
				
			}
			else
			{
				scanning = false;
				bluetoothAdapter.stopLeScan(LeScanCallback);
			}
		}
		
/*private BluetoothAdapter.LeScanCallback LeScanCallback = new BluetoothAdapter.LeScanCallback() {
	
	@Override
	public void onLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
		bluetoothGatt = device.connectGatt(MainActivity.getAppContext(),  true,  gattCallback);		
	}
};*/


/*private final BluetoothGattCallback gattCallback = new BluetoothGattCallback()
{
	@Override
    public void onConnectionStateChange(BluetoothGatt gatt, int status,
            int newState) {
        String intentAction;
        if (newState == BluetoothProfile.STATE_CONNECTED) {
            intentAction = ACTION_GATT_CONNECTED;
            mConnectionState = STATE_CONNECTED;
            broadcastUpdate(intentAction);
            Log.i(TAG, "Connected to GATT server.");
            Log.i(TAG, "Attempting to start service discovery:" +
                    bluetoothGatt.discoverServices());

        } else if (newState == BluetoothProfile.STATE_DISCONNECTED) {
            intentAction = ACTION_GATT_DISCONNECTED;
            mConnectionState = STATE_DISCONNECTED;
            Log.i(TAG, "Disconnected from GATT server.");
            broadcastUpdate(intentAction);
        }
    }

    @Override
    // New services discovered
    public void onServicesDiscovered(BluetoothGatt gatt, int status) {
        if (status == BluetoothGatt.GATT_SUCCESS) {
            broadcastUpdate(ACTION_GATT_SERVICES_DISCOVERED);
        } else {
            Log.w(TAG, "onServicesDiscovered received: " + status);
        }
    }
    
    @Override
    // Result of a characteristic read operation
    public void onCharacteristicRead(BluetoothGatt gatt,
            BluetoothGattCharacteristic characteristic,
            int status) {
        if (status == BluetoothGatt.GATT_SUCCESS) {
            broadcastUpdate(ACTION_DATA_AVAILABLE, characteristic);
        }
    }
};

@Override
public IBinder onBind(Intent intent) {
	// TODO Auto-generated method stub
	return null;
}*/

	   
	
	}

