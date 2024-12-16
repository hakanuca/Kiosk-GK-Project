using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalCounter : MonoBehaviour
{
    public TextMeshProUGUI goalText; // Reference to UI Text to display the goal count
    private int goalCount = 0;
    public GameObject gameOverText;
    public GameObject ballSpawner;
    public AudioClip goalSound; // Drag your sound clip here in the inspector
    public GameObject firstPlaceImage; // Reference to 1st place image
    public GameObject secondPlaceImage; // Reference to 2nd place image
    public GameObject thirdPlaceImage; // Reference to 3rd place image
    public Button menuButton; // Reference to the menu button
    public Button menuButtonLogo; // Reference to the menu button
    public Button menuButtonReplay; // Reference to the menu button
    public GameObject goText;
    public CanvasGroup menuButtonCanvasGroup; // New Menu button CanvasGroup

    

    private AudioSource audioSource;

    private SaveManager saveManager; // Reference to SaveManager

    // Start is called before the first frame update
    void Start()
    {
        // Get or add an AudioSource component to the ball
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = goalSound;

        // Hide the images and menu button initially
        firstPlaceImage.SetActive(false);
        secondPlaceImage.SetActive(false);
        thirdPlaceImage.SetActive(false);
        menuButton.gameObject.SetActive(false);

        // Add listener to the menu button
        menuButton.onClick.AddListener(ReloadScene);
        menuButtonReplay.onClick.AddListener(ReplayScene);

        // Get reference to the SaveManager
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager not found in the scene!");
        }
    }

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) // Check if the colliding object is the Ball
        {
            goalCount++; // Increase the goal count
            UpdateGoalText(); // Update the displayed text

            // Add points to the score in SaveManager
            saveManager.DeleteScore(100); // Example: each goal is worth 100 points

            // Play the goal sound when the ball enters the goal
            audioSource.Play();
        }
    }

    // Update the goal count text in the UI
    private void UpdateGoalText()
    {
        goalText.text = "GOALS: " + goalCount;
        if (goalCount == 10)
        {
            CallGameOverFunction();
        }
    }

    // The function to handle game over
    private void CallGameOverFunction()
    {
        ballSpawner.SetActive(false);
        gameOverText.SetActive(true); // Display "Game Over"

        int finalScore = saveManager.GetCurrentScore(); // Get final score from SaveManager

        // Show the appropriate image based on the score range
        if (finalScore < 800)
        {
            thirdPlaceImage.SetActive(true);
            menuButtonLogo.gameObject.SetActive(true);
            menuButtonReplay.gameObject.SetActive(true);
        }
        else if (finalScore < 1500)
        {
            secondPlaceImage.SetActive(true);
            menuButtonLogo.gameObject.SetActive(true);
            menuButtonReplay.gameObject.SetActive(true);
        }
        else
        {
            firstPlaceImage.SetActive(true);
            menuButtonLogo.gameObject.SetActive(true);
            menuButtonReplay.gameObject.SetActive(true);
        }

        // Show the menu button
        menuButton.gameObject.SetActive(true);
    }

    // Reload the current scene when the menu button is pressed
    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reloads the current scene
    }
    
    private void ReplayScene()
    {
        // Reset the goal count
        goalCount = 0;
        UpdateGoalText(); // Update the displayed text to reflect the reset

        // Reset the score in SaveManager
        if (saveManager != null)
        {
            saveManager.ResetScore(); // Ensure ResetScore is a method in your SaveManager
        }

        // Hide game over text and images
        gameOverText.SetActive(false);
        firstPlaceImage.SetActive(false);
        secondPlaceImage.SetActive(false);
        thirdPlaceImage.SetActive(false);

        // Reset the ball spawner to active
        ballSpawner.SetActive(true);

        // Hide menu buttons
        menuButton.gameObject.SetActive(false);
        menuButtonLogo.gameObject.SetActive(false);
        menuButtonReplay.gameObject.SetActive(false);

        // Reset other game elements to their initial state, if needed

        StartCoroutine(GoText());
    }

    System.Collections.IEnumerator GoText()
    {
        goText.SetActive(true);
        yield return new WaitForSeconds(2);
        goText.SetActive(false);
        ballSpawner.SetActive(true);
        menuButton.gameObject.SetActive(true);
        menuButtonCanvasGroup.alpha = 1;
    }
}
