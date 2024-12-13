using UnityEngine;

public class HandTracking1 : MonoBehaviour
{
    public UDPReceive1 udpReceive; // Communication for Python
    public GameObject[] handPoints; // List of the hand joints
    public GameObject WholeHand;

    private void Start()
    {
        if (udpReceive == null)
        {
            // Dynamically find the UDPReceive instance
            udpReceive = FindObjectOfType<UDPReceive1>();
            if (udpReceive == null)
            {
                Debug.LogError("UDPReceive instance not found! Make sure it exists in the scene.");
            }
        }
    }

    void Update()
    {
        if (udpReceive == null || string.IsNullOrEmpty(udpReceive.data))
        {
            Debug.LogWarning("No UDP data received or UDPReceive is null.");
            return;
        }

        string data = udpReceive.data; // Take Python gesture capture data

        try
        {
            // Remove brackets
            data = data.Remove(0, 1);
            data = data.Remove(data.Length - 1, 1);

            // Split data points
            string[] points = data.Split(',');

            for (int i = 0; i < 21; i++)
            {
                float x = 3 - float.Parse(points[i * 3]) / 200;
                float y = float.Parse(points[i * 3 + 1]) / 200;
                float z = -float.Parse(points[i * 3 + 2]) / 200 - 4; // Adjusted for better hand simulation

                // Update hand points
                handPoints[i].transform.localPosition = new Vector3(x, y, z);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error parsing UDP data: " + ex.Message);
        }
    }
}