using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{   
    GameSession GameSession;
    CircleCollider2D CircleCollider2D;

    [SerializeField] AudioClip CoinSFX;

    [SerializeField] float Volume;

    bool WasCollected=false;
    void Start()
    {
        CircleCollider2D= GetComponent<CircleCollider2D>();
        GameSession = FindObjectOfType<GameSession>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player"&&!WasCollected)
        {   
            WasCollected=true;
            GameSession.AddToCoins(10);
            AudioSource.PlayClipAtPoint(CoinSFX,Camera.main.transform.position,Volume);
            Object.Destroy(gameObject);
        }    
    }

    
}
