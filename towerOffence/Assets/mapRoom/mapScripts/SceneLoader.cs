using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject loadScreen;
    [SerializeField] Slider progressBar;
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar) progressBar.value = progress;
            yield return null;
        }

        // Optional: wait for player input to finish
        if (progressBar) progressBar.value = 1f;
        yield return new WaitForSeconds(0.5f); // optional buffer

        // Auto-activate scene
        operation.allowSceneActivation = true;
    }
}
