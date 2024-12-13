using UnityEngine;

public class BallCollisionWithSaveManager : MonoBehaviour
{
    // Reference to the SaveManager script
    public SaveManager saveManager;

    // Points awarded for each hand point collision
    private const int pointsPerCollision = 21;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object it collided with has the tag "HandPoint"
        if (collision.gameObject.CompareTag("HandPoint"))
        {
            // Increase the score via the SaveManager
            if (saveManager != null)
            {
                saveManager.AddScore(pointsPerCollision);
            }
            else
            {
                Debug.LogWarning("SaveManager is not assigned!");
            }
        }
    }
}