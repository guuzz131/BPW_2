using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalls : MonoBehaviour
{
    public GameObject nextBall;
    public Player player;
    public Transform playerPoint;
    public Rigidbody2D rb2d;
    public float distance = 0.3f;
    public float strength = 1000f;
    public Material slimeMat;
    public Color fill = new Color(0f, 112 / 255f, 1f);
    public Color stroke = new Color(4 / 255f, 156 / 255f, 1f);

    private bool _starttimer;
    private float _i;
    private float _t;
    float distanceBetween;
    float rb2dMass;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2dMass = rb2d.mass;
    }

    // Update is called once per frame
    void Update()
    {

        /*Vector2 direction = nextBall.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction);
        if (hit.collider != null)
        {
            Debug.Log("Distance " + hit.distance);
            distanceBetween = hit.distance * 10;
        }
        transform.position = Vector2.MoveTowards(transform.position, nextBall.transform.position, Time.deltaTime * distanceBetween);
        if (player.isGrounded == false)
        {
            rb2d.mass = 0f;
        } else
        {
            rb2d.mass = rb2dMass;
        }*/
        
        if ((Vector2.Distance(transform.position, playerPoint.position) > distance))
        {
            
             transform.position = Vector2.MoveTowards(transform.position, playerPoint.position, Time.deltaTime * strength);
             rb2d.mass = 0f;

        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPoint.position, Time.deltaTime * 3);
            rb2d.mass = 5f;
        }
        /*else if((Vector3.Distance(transform.position, nextBall.transform.position) > distance/ 2) || (Vector3.Distance(transform.position, playerPoint.position) > distance))
        {
            transform.position = Vector3.MoveTowards(transform.position, nextBall.transform.position, Time.deltaTime * strength / 2);
            rb2d.mass = 2.5f;
        }*/
        slimeMat.SetColor("_Color", fill);
        slimeMat.SetColor("_StrokeColor", stroke);
    }
}
