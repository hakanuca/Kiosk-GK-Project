using UnityEngine;

public class LineCode : MonoBehaviour
{
    
    LineRenderer lineRenderer; // line renderer component for bone effect in hand similation

    public Transform origin; // starting point of the line renderer
    public Transform destination; // destination point of the line renderer
    
    void Start()
    {
        // This is a GoalKeeper Hand Simulation because of that the hand imitation should be look like hand simulation
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.12f; // this is the starting point width of the line renderer
        lineRenderer.endWidth = 0.12f; // this is the ending point width of the line renderer
    }

    void Update()
    {
        lineRenderer.SetPosition(0,origin.position); // setting the position of the line renderer starting point
        lineRenderer.SetPosition(1,destination.position); // setting the position of the line renderer ending point
    }
}
