using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource Boing;
    public float speed = 10f;
    //public float jumpSpeed = 20f;
    private float movement;
    private float dirX;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public GameObject Inven;
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject objectToInstantiate;
    public Transform footDust;
    public Sprite Gear;
    public Sprite Blank;
    public bool isTouchingGround = false;
    public Transform spawnPoint;//Add empty gameobject as spawnPoint
    public float jumpVel = 4;
    public float fallMulti = 4;
    public GameObject player; //Add your player
    public GameObject LeftBullet, RightBullet;
    Transform FirePos;
    private bool isFacingRight = true;
    Animator anim;
    int jumpHash = Animator.StringToHash("Jump");
    int walkingHash = Animator.StringToHash("Walking");
    int fireHash = Animator.StringToHash("Fire");


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        
        FirePos = transform.Find("FirePos");
        anim = GetComponent<Animator>();
        
        //float v = CrossPlatformInputManager.GetAxis("Vertical");

    }

    // Update is called once per frame
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
        Debug.Log(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        Debug.Log("DirX: " + dirX);
        movement = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(movement));
        if (Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer))
        {
            isTouchingGround = true;
            anim.SetBool("Ground", true);
        }
        else
        {
            isTouchingGround = false;
            anim.SetBool("Ground", false);
        }
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }



        

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("You'd Better Run!!!!");
            Run();
        }
        //if (Input.GetButtonDown("Cancel"))
        //{
        //    Debug.Log("???");
        //    pauseMenu.SetActive(true);
        //    Time.timeScale = 0f;
        //    HUD.SetActive(false);
        //}
        // if (Input.GetKeyUp(KeyCode.RightShift))
        //{
        //     Debug.Log("You'd Better Stop!!!!");
        //     StopRun();
        //}

        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    private void FixedUpdate()
    {
        if (movement > 0.1f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            isFacingRight = true;
            anim.SetTrigger("Walking");
            anim.SetBool("Stopped", false);
            // if (isTouchingGround == true)
            // {
            //   Instantiate(objectToInstantiate, footDust.position, Quaternion.identity);
            // }
        }
        else if (movement < -0.1f)
        {

            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            isFacingRight = false;
            anim.SetTrigger("Walking");
            anim.SetBool("Stopped", false);
            //          if (isTouchingGround == true)
            // {
            //     Instantiate(objectToInstantiate, footDust.position, Quaternion.identity);
            // }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            anim.SetBool("Stopped", true);
        }
        if (dirX > 0.1f)
        {
            Debug.Log("I feel It!!");
            rigidBody.velocity = new Vector2(dirX * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            isFacingRight = true;
            anim.SetTrigger("Walking");
            anim.SetBool("Stopped", false);
            // if (isTouchingGround == true)
            // {
            //   Instantiate(objectToInstantiate, footDust.position, Quaternion.identity);
            // }
        }
        else if (dirX < -0.1f)
        {

            rigidBody.velocity = new Vector2(dirX * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            isFacingRight = false;
            anim.SetTrigger("Walking");
            anim.SetBool("Stopped", false);
            //          if (isTouchingGround == true)
            // {
            //     Instantiate(objectToInstantiate, footDust.position, Quaternion.identity);
            // }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            anim.SetBool("Stopped", true);
        }
    }
    //public void Left()
    //{
    //    movement = -1;
    //    Debug.Log("WOOOOORK!!!");
    //}
    //public void Right()
    //{
    //    movement = 1;
    //    Debug.Log("WORK Right!!!");
    //}
    //public void MoveLeft()
    //{
    //    rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
    //    transform.localScale = new Vector2(-1, 1);
    //    isFacingRight = false;
    //    anim.SetTrigger("Walking");
    //    anim.SetBool("Stopped", false);
    //}
    //public void MoveRight()
    //{
    //    rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
    //    transform.localScale = new Vector2(1, 1);
    //    isFacingRight = true;
    //    anim.SetTrigger("Walking");
    //    anim.SetBool("Stopped", false);
    //}
    public void Jump()
    {
        if (isTouchingGround == true)
        {
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVel;
            Boing.Play();
            anim.SetBool("Ground", false);
            anim.SetTrigger("Jump");
            isTouchingGround = false;

        }
    }
    public void Fire()
    {
        if (Inven.gameObject.GetComponent<Image>().sprite == Gear)
        {
            Inven.gameObject.GetComponent<Image>().sprite = Blank;
            
            anim.SetTrigger(fireHash);
            
            if (isFacingRight == false)
            {
                Instantiate(LeftBullet, FirePos.position, Quaternion.identity);
            }
            if (isFacingRight == true)
            {
                Instantiate(RightBullet, FirePos.position, Quaternion.identity);
            }
        }
    }
    public void Run()
    {
        speed = 3f;
        Debug.Log("Run Android");
    }
    //public void StopRun()
    //{
    //    speed = 2f;
    //    Debug.Log("Slow Down");
    //}


}
