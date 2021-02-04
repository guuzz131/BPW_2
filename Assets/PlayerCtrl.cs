using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;
    public GameObject GameCtrl;
    public LayerMask ground;
    public List<GameObject> BlockPrefs = new List<GameObject>();

    bool facingRight = true;
    float moveDirection = 0;
    public bool isGrounded = false;
    Vector3 cameraPos;
    public Rigidbody2D r2d;
    Transform t;
    int i = 1;
    float legsPos;
    float time1;
    float time2;
    bool isPlaying1;
    bool isPlaying2;
    bool crouched;

    // Use this for initialization
    void Start()
    {
        newBlock();
        t = transform;

        facingRight = t.localScale.x > 0;
        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
        legsPos = this.gameObject.transform.GetChild(0).Find("Legs").gameObject.transform.position.y;


    }

    void Update()
    {
        //Debug.Log(r2d.velocity.y);
        // Movement controls
        if (r2d != null)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                //left or right
                moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            }
            else
            {
                if (isGrounded || r2d.velocity.magnitude < 0.01f)
                {
                    //standing still
                    moveDirection = 0;
                }
            }
            // Change facing direction
            if (moveDirection != 0)
            {
                if (moveDirection > 0 && !facingRight)
                {
                    facingRight = true;
                }
                if (moveDirection < 0 && facingRight)
                {
                    facingRight = false;
                }
            }

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject newBrickBlock = Instantiate(new GameObject("Block"), this.transform.position, Quaternion.Euler(0, 0, 0), GameCtrl.transform);
                newBrickBlock.AddComponent<Rigidbody2D>();
                newBrickBlock.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                newBrickBlock.AddComponent<FallingBlock>();
                for (int i = 0; i <= 3; i++)
                {
                    GameObject brick = this.gameObject.transform.GetChild(0).GetChild(i).gameObject;
                    GameObject newBrick = Instantiate(brick, brick.transform.position, Quaternion.Euler(0, 0, 0), newBrickBlock.transform);
                    newBrick.AddComponent<BoxCollider2D>();
                    newBrick.layer = 9;
                    if (i == 3)
                    {
                        newBlock();
                    }
                }


            }
            if (Input.GetKey(KeyCode.S))
            {
                crouched = true;
                GameObject feet = this.gameObject.transform.GetChild(0).Find("Legs").gameObject;
                legsPos = feet.transform.position.y;
                time2 = 0;
                if (!isPlaying1)
                {
                    feet.GetComponent<Animation>().clip = feet.GetComponent<Animation>().GetClip("LegCrouchAni");
                    feet.GetComponent<Animation>().Play();
                    isPlaying1 = true;
                    isPlaying2 = false;
                }
                
                
            }
            else if(crouched)
            {
                
                GameObject feet = this.gameObject.transform.GetChild(0).Find("Legs").gameObject;
                    feet.GetComponent<Animation>().clip = feet.GetComponent<Animation>().GetClip("LegStandFromCrouchAni");
                    feet.GetComponent<Animation>().Play();
                    isPlaying1 = false;
                    i = 1;
                crouched = false;
            }
        }
    }
        
    private void FixedUpdate()
    {
        // Apply movement velocity
        if (r2d != null)
        {
            r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
        }

    }

    public void Grounded()
    {
        //Debug.Log("Velocity " + r2d.velocity.y);
        isGrounded = true;

    }
    public void NotGrounded()
    {
        isGrounded = false;

    }
    public void newBlock()
    {
        int i = Random.Range(0, BlockPrefs.Count);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        GameObject newBlock = Instantiate(BlockPrefs[i], transform.position, Quaternion.Euler(0,0,0), this.gameObject.transform);
        r2d = newBlock.GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
    }
    
}
