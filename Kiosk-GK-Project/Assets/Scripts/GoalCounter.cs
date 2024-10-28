using UnityEngine;
using UnityEngine.UI;

public class GoalCounter : MonoBehaviour
{
    public Text goalText;  // Reference to UI Text to display the goal count
    private int goalCount = 0;

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
    }
}