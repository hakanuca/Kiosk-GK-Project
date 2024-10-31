using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // This function will load a scene by its build index
    public void LoadSceneTurkish()
    {
        int sceneIndex = 1;
        // Check if the scene index is within the build settings range
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index");
        }
    }
    
    public void LoadSceneEnglish()
    {
        int sceneIndex = 2;
        // Check if the scene index is within the build settings range
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index");
        }
    }
}