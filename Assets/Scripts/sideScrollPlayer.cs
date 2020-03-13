using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class sideScrollPlayer : MonoBehaviour
{
    public float fallMulti = 4;
    public float jumpVel = 4;
    public float speed = 10f;
    public bool isTouchingGround = false;
    private float LRmove;
    private bool isFacingRight = true;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheckPoint;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;

    //CharacterController controller;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        LRmove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * speed;
        if (Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer))
        {
            isTouchingGround = true;
            //anim.SetBool("Ground", true);
        }
        else
        {
            isTouchingGround = false;
            //anim.SetBool("Ground", false);
        }
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        //controller.Move(LRmove * Time.fixedDeltaTime, false, false);
        if (LRmove > 0.1f)
        {
            rigidBody.velocity = new Vector2(LRmove * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
            //anim.SetTrigger("Walking");
           // anim.SetBool("Stopped", false);
            //if (isTouchingGround == true)
            //{
               // Instantiate(objectToInstantiate, footDust.position, Quaternion.identity);
            //}
        }
        else if (LRmove < -0.1f)
        {
            rigidBody.velocity = new Vector2(LRmove * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
           // anim.SetTrigger("Walking");
           // anim.SetBool("Stopped", false);
           // if (isTouchingGround == true)
           // {
            //    Instantiate(objectToInstantiate, footDust.position, Quaternion.identity);
           // }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
           // anim.SetBool("Stopped", true);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    public void Jump()
    {
        if (isTouchingGround == true)
        {
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVel;
            //Boing.Play();
            //anim.SetBool("Ground", false);
            //anim.SetTrigger("Jump");
            isTouchingGround = false;

        }
    }
}
