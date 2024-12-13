using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    // Current score
    private int currentScore = 0;

    // Reference to the TextMeshPro UI to display the score
    public TextMeshProUGUI scoreText;

    // Method to add points to the score
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    // Method to update the score text on the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("Score TextMeshPro object is not assigned!");
        }
    }
}