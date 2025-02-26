using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Time in seconds before the scene changes
    [SerializeField] private float timeToChange = 5f;

    // Name of the scene to load
    public string sceneToLoad;

    private void Start()
    {
        // Start the coroutine to change the scene after a delay
        StartCoroutine(ChangeSceneAfterDelay());
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeToChange);

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}