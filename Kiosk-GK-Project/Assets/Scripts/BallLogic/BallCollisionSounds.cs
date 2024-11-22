using UnityEngine;

public class BallCollisionSounds : MonoBehaviour
{
    public AudioClip collisionSound; // Drag your sound clip here in the inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component to the ball
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collides with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.Play();
        }
    }
}