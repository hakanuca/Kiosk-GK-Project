using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The prefab of the sphere to spawn
    public GameObject spherePrefab;

    // Interval between each sphere spawn (in seconds)
    public float spawnInterval = 1.0f;

    // Force applied to throw the sphere in the -Z direction
    public float throwForce = 500.0f;

    // Timer to control spawn interval
    private float timer;

    void Start()
    {
        // Initialize timer to 0
        timer = 0.0f;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new sphere
        if (timer >= spawnInterval)
        {
            // Spawn and throw the sphere
            SpawnAndThrowSphere();

            // Reset the timer
            timer = 0.0f;
        }
    }

    void SpawnAndThrowSphere()
    {
        // Create a new position for the sphere (spawning at the spawner's current position)
        Vector3 spawnPosition = transform.position;

        // Instantiate the sphere prefab at the spawn position
        GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

        // Make sure the sphere has a Rigidbody to apply force
        Rigidbody sphereRigidbody = sphere.GetComponent<Rigidbody>();

        // If the sphere has a Rigidbody, apply force to throw it along the -Z axis
        if (sphereRigidbody != null)
        {
            // Apply a force in the reverse direction (opposite of the Z-axis)
            sphereRigidbody.AddForce(-transform.forward * throwForce);
        }
        else
        {
            Debug.LogWarning("The spawned sphere does not have a Rigidbody component!");
        }
    }
}