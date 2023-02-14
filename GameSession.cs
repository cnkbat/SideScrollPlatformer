using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{   
    //Variables

    int CollectedCoins;
    [SerializeField] int PlayerLives=3;

    [SerializeField] TextMeshProUGUI LivesText;

    [SerializeField] TextMeshProUGUI ScoreText;

    
    void Awake() 
    {
        int NumOfGameSessions = FindObjectsOfType<GameSession>().Length; 
        
        if(NumOfGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = CollectedCoins.ToString();
        
    }

    public void ProcessPlayerDeath()
    {
        if(PlayerLives>1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
            CollectedCoins = 0;
        }
    }

    public void AddToCoins(int coin)
    {
        CollectedCoins+=coin;
        ScoreText.text = CollectedCoins.ToString();
    }

    IEnumerator TakeLife()
    {
        yield return new WaitForSecondsRealtime(1);
        PlayerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LivesText.text = PlayerLives.ToString();
    }
    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(1);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
