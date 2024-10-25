using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 11, 6.80f);
    public float targetY = 1.2f;
    public float targetZ = 12.1f;
    public float ySpeed = 2f;
    public float zSpeed = 2f;
    public Button startButton;           // Reference to the start button
    public GameObject resultObject;      // Game object to display after movement
    public GameObject nextObject;        // Game object to activate after resultObject closes

    private bool yMovementCompleted = false;
    private bool isMovementStarted = false;

    void Start()
    {
        transform.position = startPosition;
        resultObject.SetActive(false);   // Hide the result object at the start
        nextObject.SetActive(false);     // Hide the next object at the start
    }

    void Update()
    {
        if (isMovementStarted)
        {
            StartCoroutine(MoveCamera());
            isMovementStarted = false; // Prevent restarting the coroutine
        }
    }

    public void StartCameraMovement()
    {
        isMovementStarted = true;
        startButton.gameObject.SetActive(false); // Hide the button after clicking
    }

    System.Collections.IEnumerator MoveCamera()
    {
        // Move in the Y direction first
        while (transform.position.y > targetY)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPosition.x, targetY, startPosition.z), ySpeed * Time.deltaTime);
            yield return null;
        }
        
        yMovementCompleted = true;

        // Move in the Z direction
        while (transform.position.z < targetZ)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPosition.x, targetY, targetZ), zSpeed * Time.deltaTime);
            yield return null;
        }

        // Show the result object after camera movement completes
        StartCoroutine(ShowResultObject());
    }

    System.Collections.IEnumerator ShowResultObject()
    {
        resultObject.SetActive(true); // Show the object
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        resultObject.SetActive(false); // Hide the object after 2 seconds

        // Activate the next object after resultObject is hidden
        nextObject.SetActive(true);
    }
}
