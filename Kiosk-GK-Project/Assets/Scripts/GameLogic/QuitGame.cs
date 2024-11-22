using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // This method quits the game
    public void Quit()
    {
        // Log a message in the editor to confirm button press (only appears in the editor)
        Debug.Log("Game is quitting...");
        
        // Quit the application
        Application.Quit();
    }
}