using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The prefab of the sphere to spawn
    public GameObject spherePrefab;

    // Interval between each sphere spawn (in seconds)
    public float spawnInterval = 1.0f;

    // Min and max force to throw the sphere
    public float minThrowForce = 700.0f;
    public float maxThrowForce = 1800.0f;

    // Min and max values for the curve force (random sideways force)
    public float minCurveForce = -50.0f;
    public float maxCurveForce = 50.0f;

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

        // Destroy the sphere after 5 seconds
        Destroy(sphere, 5f);

        // Make sure the sphere has a Rigidbody to apply force
        Rigidbody sphereRigidbody = sphere.GetComponent<Rigidbody>();

        // If the sphere has a Rigidbody, apply random force to throw it with a curve effect
        if (sphereRigidbody != null)
        {
            // Randomize the throw force within the defined range
            float randomizedThrowForce = Random.Range(minThrowForce, maxThrowForce);

            // Randomize the curve force within the defined range
            float randomizedCurveForce = Random.Range(minCurveForce, maxCurveForce);

            // Calculate the force direction with a curve effect
            Vector3 throwDirection = -transform.forward * randomizedThrowForce;
            throwDirection += transform.right * randomizedCurveForce; // Adds a random sideways curve effect

            // Apply the calculated force
            sphereRigidbody.AddForce(throwDirection);
        }
        else
        {
            Debug.LogWarning("The spawned sphere does not have a Rigidbody component!");
        }
    }
}
