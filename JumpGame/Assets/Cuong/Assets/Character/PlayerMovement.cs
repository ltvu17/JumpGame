using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    // Start is called before the first frame update
    void Start()
    {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * 3f, rb.velocity.y);

        if(Input.GetButtonDown("Jump")){
           rb.velocity = new Vector2(rb.velocity.x,7f); 
        }
        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate(){
        if(dirX > 0f){
            anim.SetBool("Running", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }else if(dirX < 0f){
            anim.SetBool("Running", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }else{
            anim.SetBool("Running", false);
        }
    }
}
