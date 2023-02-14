using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float MoveSpeed= 1f;
    Rigidbody2D Rigidbody2D;

    void Start()
    {
        Rigidbody2D= GetComponent<Rigidbody2D>();

        
    }

    
    void Update()
    {
        Rigidbody2D.velocity= new Vector2(MoveSpeed,0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            MoveSpeed= -MoveSpeed;
            FlipEnemyFacing();
        }
         
    }
    void FlipEnemyFacing()
    {
        transform.localScale=  new Vector2 (-(Mathf.Sign(Rigidbody2D.velocity.x)),1f);
    }
}
