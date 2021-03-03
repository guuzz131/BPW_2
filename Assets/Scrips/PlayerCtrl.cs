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
    public bool isGrounded = false;
    public Rigidbody2D r2d;
    public bool objectIsGrounded = false;
    public Material blueDissolveMat;
    public Material grayDissolvedMat;
    public bool isDissolving;
    public float dissolve;
    public Canvas canvas;

    bool facingRight = true;
    float moveDirection = 0;
    Vector3 cameraPos;
    Transform t;
    int i = 1;
    float legsPos;
    float time1;
    bool hasPlayer = false;
    bool canSpawn = true;
    bool isPlaying1;
    bool justStarted = true;

    bool crouched;
    List<GameObject> dissolvingObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
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
        if (isDissolving && blueDissolveMat.GetFloat("_DissolveAmount") < 1)
        {
            dissolve = Mathf.Clamp01(dissolve + Time.deltaTime);
            blueDissolveMat.SetFloat("_DissolveAmount", dissolve);
        } else if (isDissolving)
        {
            blueDissolveMat.SetFloat("_DissolveAmount", 0);
            isDissolving = false;
            dissolve = 0;
            for (int i = 0; i < dissolvingObjects.Count; i++)
            {
                dissolvingObjects[i].GetComponent<SpriteRenderer>().material = grayDissolvedMat;
            }
            dissolvingObjects.Clear();
        }
        
        if (r2d != null && hasPlayer)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                //left or right
                moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            }
            else
            {
                if (isGrounded || r2d.velocity.magnitude < 0.01f || objectIsGrounded)
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
                        isDissolving = true;
                        newBrick.AddComponent<BoxCollider2D>();
                        newBrick.layer = 9;
                        dissolvingObjects.Add(newBrick);
                        if (i == 3)
                        {
                            Destroy(gameObject.transform.GetChild(0).gameObject);
                            GameObject.Find("Cameras").GetComponent<CameraFollow>().moveCam = true;
                            hasPlayer = false;
                            Invoke("timer", 1f);
                        }
                    }
            }
            if (Input.GetKey(KeyCode.S) && isGrounded)
            {
                crouched = true;
                GameObject feet = this.gameObject.transform.GetChild(0).Find("Legs").gameObject;
                legsPos = feet.transform.position.y;
                if (!isPlaying1)
                {
                    feet.GetComponent<Animation>().clip = feet.GetComponent<Animation>().GetClip("LegCrouchAni");
                    feet.GetComponent<Animation>().Play();
                    isPlaying1 = true;
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
        if (Input.GetKeyDown(KeyCode.E) && !hasPlayer && GameObject.Find("Cameras").GetComponent<CameraFollow>().camHasArrived && canSpawn)
        {
            GameObject.Find("Cameras").GetComponent<CameraFollow>().moveCam = false;
            GameObject.Find("Cameras").GetComponent<CameraFollow>().camHasArrived = false;
            newBlock();
            canSpawn = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && justStarted && !hasPlayer && canSpawn)
        {

            GameObject.Find("Cameras").GetComponent<CameraFollow>().moveCam = false;
            GameObject.Find("Cameras").GetComponent<CameraFollow>().camHasArrived = false;
            newBlock();
            canSpawn = false;

            justStarted = false;
        }
    }
        
    private void FixedUpdate()
    {
        // Apply movement velocity
        if (r2d != null)
        {
            if (!objectIsGrounded)
            {
                r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
            }
            else
            {
                r2d.velocity = new Vector2((moveDirection) * maxSpeed/2, r2d.velocity.y);
            }

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
        GameObject FirstInList = canvas.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<DragObject>().spawnEntity;
        canvas.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<DragObject>().GiveNewPlayer();
        GameObject newBlock = Instantiate(FirstInList, transform.position, Quaternion.Euler(0,0,0), this.gameObject.transform);
        r2d = newBlock.GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        hasPlayer = true;
    }
    private void timer()
    {
        canSpawn = true;
    }
}
