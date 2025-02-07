using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    public LightShowManager lightShowManager; // Reference to the light show manager

    public float fadeDuration = 0.5f;

    private CanvasGroup startButtonCanvasGroup;
    private CanvasGroup quitButtonCanvasGroup;
    private CanvasGroup titleCanvasGroup;
    private CanvasGroup menuButtonCanvasGroup;
    private bool isMovementStarted = false;

    void Start()
    {
        transform.position = startPosition;
        goText.SetActive(false);
        ballSpawner.SetActive(false);

        startButtonCanvasGroup = startButton.GetComponent<CanvasGroup>() ?? startButton.gameObject.AddComponent<CanvasGroup>();
        quitButtonCanvasGroup = quitButton.GetComponent<CanvasGroup>() ?? quitButton.gameObject.AddComponent<CanvasGroup>();
        titleCanvasGroup = title.GetComponent<CanvasGroup>() ?? title.AddComponent<CanvasGroup>();
        menuButtonCanvasGroup = menuButton.GetComponent<CanvasGroup>() ?? menuButton.gameObject.AddComponent<CanvasGroup>();

        menuButtonCanvasGroup.alpha = 0;
        menuButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isMovementStarted)
        {
            StartCoroutine(MoveCamera());
            isMovementStarted = false;
        }
    }

    public void StartCameraMovement()
    {
        isMovementStarted = true;
        StartCoroutine(FadeOutUI());

        // Stop the light show when Play button is pressed
        if (lightShowManager != null)
        {
            lightShowManager.StopLightShow();
        }
    }




    private IEnumerator FadeOutUI()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);

            startButtonCanvasGroup.alpha = alpha;
            quitButtonCanvasGroup.alpha = alpha;
            titleCanvasGroup.alpha = alpha;

            elapsed += Time.deltaTime;
            yield return null;
        }

        startButtonCanvasGroup.alpha = 0;
        quitButtonCanvasGroup.alpha = 0;
        titleCanvasGroup.alpha = 0;

        startButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        title.SetActive(false);

        StartCoroutine(FadeInMenuButton());
    }

    private IEnumerator FadeInMenuButton()
    {
        menuButton.gameObject.SetActive(true);
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            menuButtonCanvasGroup.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        menuButtonCanvasGroup.alpha = 1;
    }

    private IEnumerator MoveCamera()
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

    private IEnumerator ShowResultObject()
    {
        goText.SetActive(true); // Show "GO" text
        yield return new WaitForSeconds(2); // Wait for 2 seconds

        goText.SetActive(false);
        ballSpawner.SetActive(true);

        // Stop the light show when the "GO" text appears
        if (lightShowManager != null)
        {
            lightShowManager.StopLightShow();
        }
    }

}
