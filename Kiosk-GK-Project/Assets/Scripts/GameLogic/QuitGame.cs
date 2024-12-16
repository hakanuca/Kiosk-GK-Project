using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    // This method quits the game
    public void Quit()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    
}