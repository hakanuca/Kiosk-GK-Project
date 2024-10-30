using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoalCounter : MonoBehaviour
{
    public Text goalText;  // Reference to UI Text to display the goal count
    private int goalCount = 0;
    public GameObject gameOverText;
    public GameObject ballSpawner;
    
    
    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")  // Check if the colliding object is the Ball
        {
            goalCount++;  // Increase the goal count
            UpdateGoalText();  // Update the displayed text
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

    // The function close the game after the reached maximum goal
    private void CallGameOverFunction()
    {
        ballSpawner.SetActive(false);
        gameOverText.SetActive(true); // Sets the game over text
        StartCoroutine(ReloadSceneAfterDelay(3f)); // Starts coroutine with 3 seconds delay
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Waits for specified delay
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reloads the current scene
    }
}