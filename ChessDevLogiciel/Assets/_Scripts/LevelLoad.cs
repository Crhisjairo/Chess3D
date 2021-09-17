using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{ 
    public GameObject mainMenu;
    public Slider slider;
    public GameObject loading;

    public void LoadLevel(int sceneIndex)
    {
        mainMenu.SetActive(false);
        loading.SetActive(true);
        StartCoroutine(LoadAsynchornously(sceneIndex));
    }
    IEnumerator LoadAsynchornously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }
}
