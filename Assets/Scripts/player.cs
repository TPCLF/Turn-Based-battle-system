using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float speed = 10f;
    private float dirX;
    public float jumpVel = 4;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(dirX > 0.1f)
        {
            Debug.Log("I feel It!!");
            rigidBody.velocity = new Vector2(dirX * speed, rigidBody.velocity.y);
            //transform.localScale = new Vector2(1, 1);
        }
        if (dirX < 0.1f)
        {
            Debug.Log("I still feel It!!");
            rigidBody.velocity = new Vector2(dirX * speed, rigidBody.velocity.y);
            //transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            //anim.SetBool("Stopped", true);
        }
    }
}
