using UnityEngine;
using System.Collections;

public class LightShowManager : MonoBehaviour
{
    public Light[] spotLights; // Assign 8 spotlights in the Inspector
    public float interval = 0.5f; // Change lights every 0.5 seconds
    private bool isLightShowActive = true; // Light show starts by default
    private Coroutine lightShowCoroutine;

    void Start()
    {
        // Start the light show immediately
        lightShowCoroutine = StartCoroutine(LightShowRoutine());
    }

    public void StopLightShow()
    {
        isLightShowActive = false;

        if (lightShowCoroutine != null)
        {
            StopCoroutine(lightShowCoroutine);
        }

        // Ensure all spotlights reset to normal intensity
        foreach (Light spot in spotLights)
        {
            spot.intensity = 3;
        }
    }

    private IEnumerator LightShowRoutine()
    {
        while (isLightShowActive)
        {
            // Set a new random intensity for all lights
            foreach (Light spot in spotLights)
            {
                spot.intensity = Random.Range(1f, 3f);
            }

            yield return new WaitForSeconds(interval);
        }

        // Ensure all lights are reset when the loop exits
        foreach (Light spot in spotLights)
        {
            spot.intensity = 1;
        }
    }
}