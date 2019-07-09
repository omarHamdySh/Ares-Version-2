using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Slider loading;
    [SerializeField] private TextMeshProUGUI loadingValue;

    /// <summary>
    /// Exit the game
    /// </summary>
    public void QuitTheGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Load the MainMenu Scene
    /// </summary>
    public void BackToMainMenu()
    {
        StartCoroutine(LoadAsynchronously(0));
    }

    /// <summary>
    /// Restart the current Scene
    /// </summary>
    public void RestartLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    /// <summary>
    /// Moving from scene to another
    /// </summary>
    /// <param name="sceneNum">name of the scene you will go to</param>
    public void LoadLevel(int sceneNum)
    {
        StartCoroutine(LoadAsynchronously(sceneNum));
    }

    private IEnumerator LoadAsynchronously(int sceneName)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            loading.value = operation.progress;
            loadingValue.text = (int)(operation.progress * 100) + " %";
            yield return null;
        }
    }
}
