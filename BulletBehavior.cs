using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] float BulletSpeed;
    Rigidbody2D Rigidbody2D;

    BoxCollider2D BoxCollider2D;
    PlayerMovement Player;

    float XVectorBulletSpeed;
    void Start()
    {
        Rigidbody2D= GetComponent<Rigidbody2D>();

        BoxCollider2D = GetComponent<BoxCollider2D>();

        Player= FindObjectOfType<PlayerMovement>();

        XVectorBulletSpeed = BulletSpeed *   Player.transform.localScale.x;  
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity= new Vector2(XVectorBulletSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
