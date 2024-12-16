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
    
    public void DeleteScore(int points)
    {
        currentScore -= points;
        UpdateScoreText();
    }
    
    public void ResetScore()
    {
        currentScore = 0; // Reset the score variable
        UpdateScoreText();

    }


    // Method to get the current score
    public int GetCurrentScore()
    {
        return currentScore;
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