using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;
    public Transform upPoint;
    
    private bool canStand;
    public bool crouching;
    public BoxCollider2D stand;
    public CircleCollider2D crouch;
    public CircleCollider2D bottom;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        stand.enabled = true;
        crouch.enabled = false;
        bottom.enabled = true;
        crouching = false;

    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            Jump();
        }
        canStand = Physics2D.OverlapCircle(upPoint.position, groundCheckRadius, groundLayer);
        
        if (Input.GetAxis ("Horizontal") == 0)
        {
            anim.SetInteger("animation", 1);
        }
        else if(isTouchingGround == false){
            Flip();
            anim.SetInteger("animation", 3);
        }
        else
        {
            Flip();
            anim.SetInteger("animation", 2);
        }
        Crouch();
    }

    void FixedUpdate(){
        rb.velocity = new Vector2(Input.GetAxis ("Horizontal") * 3f, rb.velocity.y);

     
    }

    void Jump(){
        rb.AddForce (transform.up * 3f,  ForceMode2D.Impulse);
    }

    void Crouch()
    {
        if ((Input.GetKey(KeyCode.LeftControl) || canStand) && isTouchingGround == true)
        {
            Flip();
            anim.SetInteger("animation", 4);
            stand.enabled = false;
            crouch.enabled = true;
            bottom.enabled = true;
        }
        else if (!canStand)
        {
            stand.enabled = true;
            crouch.enabled = false;
            bottom.enabled = true;
        }
    }   
 
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);

    }
}
    