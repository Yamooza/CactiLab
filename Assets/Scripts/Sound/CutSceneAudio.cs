using UnityEngine;

public class CutSceneAudio : MonoBehaviour
{
    [Header("Audio Source Settings")]
    [Tooltip("The AudioSource component to enable.")]
    public AudioSource audioSource;

    [Header("Timing Settings")]
    [Tooltip("Time in seconds after which the AudioSource will be enabled.")]
    public float delayTime = 5f;

    private void Start()
    {
        // Start the coroutine to enable the AudioSource after the specified delay
        StartCoroutine(EnableAudioSourceCoroutine());
    }

    private System.Collections.IEnumerator EnableAudioSourceCoroutine()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Enable the AudioSource
        if (audioSource != null)
        {
            audioSource.enabled = true;
            Debug.Log("AudioSource has been enabled.");
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned in the inspector.");
        }
    }
}