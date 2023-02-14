using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")] 
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpSpeed;
    [SerializeField] float ClimbSpeed;

    [SerializeField] GameObject Bullet;

    [SerializeField] Transform Gun;
    [SerializeField] Vector2 DeathKick= new Vector2(10f,10f);
    
   
    Vector2 MoveInput;
    Rigidbody2D Rigidbody2D;
    Animator Animator;
    LayerMask LayerMask;
    CapsuleCollider2D CapsuleCollider;
    BoxCollider2D BoxCollider2D;

    //Variables
    bool IsAlive=true;
    float GravityScaleStart;
    void Start()
    {
        Rigidbody2D= GetComponent<Rigidbody2D>();
        Animator= GetComponent<Animator>();
        CapsuleCollider= GetComponent<CapsuleCollider2D>();
        GravityScaleStart= Rigidbody2D.gravityScale;
        BoxCollider2D= GetComponent<BoxCollider2D>();

        
    }
    void Update()
    {
        if(!IsAlive)
        {
            return;
        }
        Run();

        FlipSprite();

        Climb();

        Die();

    }
    
    //Input System
    void OnMove(InputValue value)
    {
        CheckIsDead();
        MoveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        CheckIsDead();
        if (BoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(value.isPressed)
            {
                Rigidbody2D.velocity += new Vector2 (0f,JumpSpeed);
            }
        }
    }
    void OnFire(InputValue value)
    {
        CheckIsDead();

        Instantiate(Bullet,Gun.position,transform.rotation);
    }
    //PlayerHealth
    void CheckIsDead()
    {
        if (!IsAlive)
        {
            return;
        }
    }

    //Movement
    void Climb()
    {
        if(!IsAlive)
        {
            return;
        }
        bool PlayerHasVerticalSpeed = Mathf.Abs(Rigidbody2D.velocity.y) >Mathf.Epsilon; 
        if (BoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 ClimbVelocity= new Vector2 (Rigidbody2D.velocity.x,MoveInput.y*ClimbSpeed);
            Rigidbody2D.velocity=ClimbVelocity;
            Animator.SetBool("IsClimbing?",true);
            Rigidbody2D.gravityScale=0;
            if(!PlayerHasVerticalSpeed)
            {
                Animator.SetBool("IsClimbing?",false);
            }
        }
        else
        {
            Animator.SetBool("IsClimbing?",false);
            Rigidbody2D.gravityScale=GravityScaleStart;
        }
    }
    
    void Run()
    {
        Vector2 PlayerVelocity = new Vector2 (MoveInput.x*RunSpeed,Rigidbody2D.velocity.y);
        Rigidbody2D.velocity=PlayerVelocity;
    }
   //Sprite
    void FlipSprite()
    {   
        bool PlayerHasHorizontalSpeed = Mathf.Abs(Rigidbody2D.velocity.x) > Mathf.Epsilon;

        if(PlayerHasHorizontalSpeed)
        {
            transform.localScale=  new Vector2 (Mathf.Sign(Rigidbody2D.velocity.x),1f);

            Animator.SetBool("IsRunning?",true);
        }
        else
        {
            Animator.SetBool("IsRunning?",false);
        }
         
    }

    void Die() 
    {
        if(CapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazards","Enemy")))
        {
            IsAlive=false;
            Animator.SetTrigger("bDead");

            Rigidbody2D.velocity= DeathKick;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();

        }

    }
}
