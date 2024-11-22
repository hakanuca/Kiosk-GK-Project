using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 11, 6.80f);
    public float targetY = 1.2f;
    public float targetZ = 12.1f;
    public float ySpeed = 2f;
    public float zSpeed = 2f;
    public Button startButton;
    public Button quitButton;
    public GameObject goText;
    public GameObject ballSpawner;
    public GameObject title;
    public Button menuButton; 

    public float fadeDuration = 0.5f; // Duration for fade-out effect

    private CanvasGroup startButtonCanvasGroup;
    private CanvasGroup quitButtonCanvasGroup;
    private CanvasGroup titleCanvasGroup;
    private CanvasGroup menuButtonCanvasGroup; // New Menu button CanvasGroup
    private bool isMovementStarted = false;

    void Start()
    {
        transform.position = startPosition;
        goText.SetActive(false);
        ballSpawner.SetActive(false);

        // Get or add CanvasGroup components
        startButtonCanvasGroup = startButton.GetComponent<CanvasGroup>() ?? startButton.gameObject.AddComponent<CanvasGroup>();
        quitButtonCanvasGroup = quitButton.GetComponent<CanvasGroup>() ?? quitButton.gameObject.AddComponent<CanvasGroup>();
        titleCanvasGroup = title.GetComponent<CanvasGroup>() ?? title.AddComponent<CanvasGroup>();
        menuButtonCanvasGroup = menuButton.GetComponent<CanvasGroup>() ?? menuButton.gameObject.AddComponent<CanvasGroup>();

        // Set initial state for menu button
        menuButtonCanvasGroup.alpha = 0; // Invisible initially
        menuButton.gameObject.SetActive(false); // Inactive initially
    }

    void Update()
    {
        if (isMovementStarted)
        {
            StartCoroutine(MoveCamera());
            isMovementStarted = false; // Prevent restarting the coroutine
        }
    }

    public void StartCameraMovement()
    {
        isMovementStarted = true;
        StartCoroutine(FadeOutUI()); // Start the fade-out effect for the UI elements
    }

    private System.Collections.IEnumerator FadeOutUI()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);

            // Set alpha for each CanvasGroup
            startButtonCanvasGroup.alpha = alpha;
            quitButtonCanvasGroup.alpha = alpha;
            titleCanvasGroup.alpha = alpha;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Set alpha to 0 to ensure it's fully transparent
        startButtonCanvasGroup.alpha = 0;
        quitButtonCanvasGroup.alpha = 0;
        titleCanvasGroup.alpha = 0;

        // Optionally, disable the UI elements after fading out
        startButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        title.SetActive(false);

        // Fade in the Menu button
        StartCoroutine(FadeInMenuButton());
    }

    private System.Collections.IEnumerator FadeInMenuButton()
    {
        menuButton.gameObject.SetActive(true); // Activate the button
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);

            // Set alpha for Menu button
            menuButtonCanvasGroup.alpha = alpha;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is fully set to 1
        menuButtonCanvasGroup.alpha = 1;
    }

    System.Collections.IEnumerator MoveCamera()
    {
        while (transform.position.y > targetY)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPosition.x, targetY, startPosition.z), ySpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.z < targetZ)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPosition.x, targetY, targetZ), zSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(ShowResultObject());
    }

    System.Collections.IEnumerator ShowResultObject()
    {
        goText.SetActive(true);
        yield return new WaitForSeconds(2);
        goText.SetActive(false);
        ballSpawner.SetActive(true);
    }
}
