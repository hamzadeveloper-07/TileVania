using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class playerExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 0.8f;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Player"){
        StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<scenePersist>().resetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
