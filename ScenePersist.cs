using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake() 
    {
        int NumOfScenePersists = FindObjectsOfType<ScenePersist>().Length; 
        
        if(NumOfScenePersists>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
