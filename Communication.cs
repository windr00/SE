using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Reflection;
using Myproto;
using UnityEngine;
using ProtoBuf;


public class Communication
{
    public bool isNetworkAvailable = false;

	private static Communication _instance = null;

	public static Communication GetInstance() {
		if (_instance == null) {
			_instance = new Communication();
		}
		return _instance;
	}

	public void AddNetworkListeners(NetworkConnected netConCall, DataSent dataSentCall, DataReceived dataRecvCall, NetworkError netErrorCall) {
		OnNetworkConnected += netConCall;
		OnDataSent += dataSentCall;
		OnDataReceived += dataRecvCall;
		OnNetworkError += netErrorCall;
	}

    public enum NetworkType
    {
        TCP,
        HTTP
    }

    private NetworkType type;

    private string ipAddress;

    private int port;

    private TCPClient tcpInstance;



    public delegate void NetworkConnected();

    private event NetworkConnected OnNetworkConnected;

    public delegate void DataReceived(object data);

    private event DataReceived OnDataReceived;

    public delegate void DataSent();

    private event DataSent OnDataSent;

    public delegate void NetworkError(Exception e);

    private event NetworkError OnNetworkError;

	public void Initial(NetworkType netType, string ipAddress, int port) {
		
		type = netType;
		this.ipAddress = ipAddress;
		this.port = port;
	}

    private Communication(){}

    public string GetHostIP()
    {
        IPAddress[] addressList = Dns.GetHostAddresses(Dns.GetHostName());
        return addressList[0].ToString();
    }

    public void Connect()
    {
        switch (type)
        {
            case NetworkType.TCP:
                tcpInstance = new TCPClient(ipAddress, port, new AsyncCallback(ConnectionCallBack));
                Debug.Log("create tcp client");
                break;
            default:
                break;
        }
    }

    public void Send(byte[] data)
    {
        switch (type)
        {
            case NetworkType.TCP:
                tcpInstance.Send(data, new AsyncCallback(SendCallBack));
                break;
            default:

                break;
        }
    }

    public void Read()
    {
        switch (type)
        {
            case NetworkType.TCP:
                tcpInstance.Read(new AsyncCallback(ReadCallBack));
                break;
            default:
                break;
        }
    }

    private void ConnectedCallback()
    {
        if (OnNetworkConnected != null)
        {
            OnNetworkConnected();
        }
    }

    private void DataSentCallback()
    {
        if (OnDataSent != null)
        {
            OnDataSent();
        }
    }

    private void DataReceivedCallback(object data)
    {
        if (OnDataReceived != null)
        {
            OnDataReceived(data);
        }
    }

    private void ErrorCallback(Exception e)
    {
        if (OnNetworkError != null)
        {
            OnNetworkError(e);
        }
    }

    private void ConnectionCallBack(IAsyncResult arResult)
    {
        try
        {
            switch (type)
            {
                case NetworkType.TCP:
                    {
                        TcpClient client = arResult.AsyncState as TcpClient;
                        client.EndConnect(arResult);
                        ConnectedCallback();
                        break;
                    }
                case NetworkType.HTTP:
                    {

                        break;
                    }
            }
        }
        catch (Exception e)
        {
            ErrorCallback(e);
            Debug.Log("connection error: " + e.Message);
        }
        
    }

    private void SendCallBack(IAsyncResult arResult)
    {
        NetworkStream stream = arResult.AsyncState as NetworkStream;
        try
        {
            switch (type)
            {
                case NetworkType.TCP:
                    {
                        stream.EndWrite(arResult);
                        DataSentCallback();
                        break;
                    }
                case NetworkType.HTTP:
                    {

                        break;
                    }
            }
        }
        catch (Exception e)
        {
            ErrorCallback(e);
            Debug.Log("send error: " + e.Message);
        }
    }

    

    private void ReadCallBack(IAsyncResult arResult)
    {
        try
        {
            switch (type)
            {
                case NetworkType.TCP:
                    {
                        TCPClient.TCPStreamState state = arResult.AsyncState as TCPClient.TCPStreamState;
                        state.WorkStream.EndRead(arResult);
                        OnDataReceived(state.RecvBytes);
                        break;
                    }
                case NetworkType.HTTP:
                    {

                        break;
                    }
            }
        }
        catch (Exception e)
        {
            ErrorCallback(e);
            Debug.Log("read error: " + e + ": " +  e.Message);
        }
    }


}
