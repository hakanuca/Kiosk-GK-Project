using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive1 : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    public int port = 5053;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;

    private static UDPReceive1 instance; // Singleton instance

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate objects
        }
    }

    public void Start()
    {
        if (receiveThread == null)
        {
            receiveThread = new Thread(ReceiveData)
            {
                IsBackground = true
            };
            receiveThread.Start();
        }
    }

    private void OnDestroy()
    {
        StopReceiving();
    }

    private void OnApplicationQuit()
    {
        StopReceiving();
    }

    // Gracefully stop the thread
    private void StopReceiving()
    {
        startReceiving = false;
        if (client != null)
        {
            client.Close(); // Close the UDP connection
        }
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort(); // Terminate the thread
        }
    }

    // Receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if (printToConsole)
                {
                    Debug.Log(data);
                }
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }
    }
}
