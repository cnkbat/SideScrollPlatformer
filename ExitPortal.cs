using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitPortal : MonoBehaviour
{
    [SerializeField] float LevelDelay =1;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            StartCoroutine(StartNewLevel());
        }   
    }
    IEnumerator StartNewLevel()
    {
        yield return new WaitForSecondsRealtime(LevelDelay);
        
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex+1;

        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex=0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(NextSceneIndex);
    }
}
