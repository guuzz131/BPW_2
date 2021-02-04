using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;
    public GameObject groundCheck;
    public GameObject waterSpawner;
    public GameObject eyes;
    public GameObject slime;
    public GameObject slime2;
    public GameObject GameCtrl;
    public LayerMask ground;
    public GameObject slimeBlockPref;
    public GameObject blockDetector;
    public GameObject block;
    public bool blockInFront;
    public GameObject eatenBlock;

    bool facingRight = true;
    bool startWobble = false;
    bool lookingUp;
    bool lookingDown;
    float moveDirection = 0;
    float WobbleValue = 1;
    int doubleJump = 0;
    int totalBlocks = 0;
    public bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
    int i = 1;
    private PhysicsMaterial2D material;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        material = mainCollider.sharedMaterial;
        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    void Update()
    {
        //Debug.Log(r2d.velocity.y);
        // Movement controls
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            //left or right
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                //standing still
                moveDirection = 0;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //look up to launch higher with less gravity
            lookingUp =true;
            lookingDown = false;
            mainCollider.sharedMaterial = material;
            if (facingRight)
            {
                eyes.transform.rotation = Quaternion.Euler(eyes.transform.rotation.x, eyes.transform.rotation.y, 15);
            }
            else
            {
                eyes.transform.rotation = Quaternion.Euler(eyes.transform.rotation.x, eyes.transform.rotation.y, -15);
            }
            if (!isGrounded)
            {
                r2d.gravityScale = .9f;
            }

        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            //fall down faster with more gravity
            lookingDown = true;
            lookingUp = false;
            if (facingRight)
            {
                eyes.transform.rotation = Quaternion.Euler(eyes.transform.rotation.x, eyes.transform.rotation.y, -15);
            }
            else
            {
                eyes.transform.rotation = Quaternion.Euler(eyes.transform.rotation.x, eyes.transform.rotation.y, 15);
            }
            if (!isGrounded)
            {
                r2d.gravityScale = 3;
                mainCollider.sharedMaterial = null;
            }
        }
        else
        {
            mainCollider.sharedMaterial = material;
            lookingUp = false;
            lookingDown = false;
            eyes.transform.rotation = Quaternion.Euler(eyes.transform.rotation.x, eyes.transform.rotation.y, 0);
            r2d.gravityScale = 1.5f;
        }
        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && doubleJump == 0)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            doubleJump = 1;

        }else if (Input.GetKeyDown(KeyCode.Space) && doubleJump == 1)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            doubleJump = 2;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //use button, pickup block or place slime block

            Vector2 i2 = Vector2.right;
            if (!facingRight && !lookingDown && !lookingUp)
            {
                i2 = Vector2.left;
            }else if (lookingDown)
            {
                i2 = Vector2.down;
            }
            else if (lookingUp)
            {
                i2 = Vector2.up;
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position, i2, 1f, ground);
            Debug.DrawRay(transform.position, Vector2.right, Color.green, 1f);
            if (hit.collider != null && totalBlocks < 1 && hit.transform.tag != "Unbreakable")
            {
                Destroy(hit.collider.gameObject);
                totalBlocks += 1;
                eatenBlock.SetActive(true);
            }
             else if (totalBlocks > 0)
            {
                eatenBlock.SetActive(false);
                float i = 2;
                if (!facingRight)
                {
                    i = -2;
                }
                Vector2 gridLoc = new Vector2(Mathf.Floor(blockDetector.transform.position.x) + .5f, Mathf.Floor(blockDetector.transform.position.y) +.5f);
                Vector2 newPosition = new Vector2(transform.position.x + i, transform.position.y);
                //Transform newTransform = new Transform(newPosition, Quaternion.Euler(0,0,0), new Vector3(0,0,0));
                var cloneSlimeBlock = Instantiate(slimeBlockPref, blockDetector.transform);
                cloneSlimeBlock.transform.position = gridLoc;
                cloneSlimeBlock.transform.parent = GameCtrl.transform;
                cloneSlimeBlock.transform.localScale = new Vector3(1,1,1);
                totalBlocks -= 1;
            }
            
        }
        if (startWobble)
        {
            //for a coded wobbling animation
            Wobble();
        }
    }
    private void FixedUpdate()
    {
        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
    }

    public void Grounded()
    {
        //Debug.Log("Velocity " + r2d.velocity.y);
        isGrounded = true;
        doubleJump = 0;
        if (startWobble == false)
        {
            float VelocityPrior = r2d.velocity.y * -1;
            float VelocityPriorClamped = Mathf.Clamp(VelocityPrior, 0, 5);
            float VelocityValue = VelocityPriorClamped / 5;
            WobbleValue = Mathf.Lerp(1, 0.8f, VelocityValue);
            //Debug.Log("Wobble Value: " + WobbleValue);
            i = 1;
            startWobble = true;
        }
        
    }
    public void NotGrounded()
    {
        isGrounded = false;

    }
    public void Wobble()
    {

        //Debug.Log("Wobbling: " + slime.transform.localScale.y + ", i = " + i);
        if (slime.transform.localScale.y <= 1 && slime.transform.localScale.y > WobbleValue && i == 1)
        {
            slime.transform.localScale = new Vector3(slime.transform.localScale.x, slime.transform.localScale.y - Time.deltaTime, slime.transform.localScale.z);
            slime2.transform.localScale = new Vector3(slime2.transform.localScale.x, slime2.transform.localScale.y - Time.deltaTime, slime2.transform.localScale.z);
        }
        else if (slime.transform.localScale.y < 1 && i < 3)
        {
            i = 2;
            slime.transform.localScale = new Vector3(slime.transform.localScale.x, slime.transform.localScale.y + Time.deltaTime * 1.5f, slime.transform.localScale.z);
            slime2.transform.localScale = new Vector3(slime2.transform.localScale.x, slime2.transform.localScale.y + Time.deltaTime * 1.5f, slime2.transform.localScale.z);
        }
        else if (slime.transform.localScale.y >= 1)
        {
            i = 3;
            slime.transform.localScale = new Vector3(slime.transform.localScale.x, 1, slime.transform.localScale.z);
            slime2.transform.localScale = new Vector3(slime2.transform.localScale.x, 1, slime2.transform.localScale.z);
            startWobble = false;
        }
    }

}





































/*void FixedUpdate()
{
    Bounds colliderBounds = mainCollider.bounds;
    float colliderRadius = mainCollider.size.x * .8f * Mathf.Abs(transform.localScale.x);
    Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * .3f, 0);
    // Check if player is grounded
    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
    //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
    isGrounded = false;
    if (colliders.Length > 0)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != mainCollider)
            {
                isGrounded = true;
                doubleJump = 0;
                break;
            }
        }
    }



    // Simple debug
    Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
    Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
}*/