using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive; // communication for python
    public GameObject[] handPoints; // list of the hand joints

    public GameObject WholeHand;
    void Update()
    {
        string data = udpReceive.data; // taking the python gesture capture data

        data = data.Remove(0, 1);
        data= data.Remove(data.Length - 1, 1);
        print(data);
        string[] points = data.Split(',');
        print(points[0]);
        
        //float x0 = float.Parse(points[5*3+0]) - float.Parse(points[17*3+0]);
        //float y0 = float.Parse(points[5*3+1]) - float.Parse(points[17*3+1]);
        //float z0 = float.Parse(points[5*3+2]) - float.Parse(points[17*3+2]);

        
        //float width_pix = Mathf.Sqrt(Mathf.Pow(x0, 2) + Mathf.Pow(y0, 2) + Mathf.Pow(z0, 2));

        //float z_hand = 0.05f * width_pix + 7.5f;
        //Debug.Log("DEGER" +width_pix); // TEMIZLE

        //float hand_size = 200 / width_pix;
        //WholeHand.transform.localScale = new Vector3(hand_size, hand_size, hand_size);
        //WholeHand.transform.position = new Vector3(0, 0, z_hand);
        
        

        for (int i = 0; i < 21; i++)
        {
            float x = 5 - float.Parse(points[i*3])/100;
            float y = float.Parse(points[i*3+1])/100;
            float z = float.Parse(points[i*3+2])/100;
            
            handPoints[i].transform.localPosition = new Vector3(x, y, z);
            //handPoints[i].transform.localScale = new Vector3(0.5f / hand_size, 0.5f / hand_size, hand_size);
            
            
        }
    }
}
