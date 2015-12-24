#pragma warning disable 0108  // Use new keyword if hiding was intended
using UnityEngine;
using System.Collections;

public class BunnyScript : CreatureScript {
    
    // TODO: jumping
    //public float jumpStrength;
    //public float jumpCooldown;  // Cooldown before bunny can jump again, in seconds.
    //private float jumpTimer = 0;  // Used to time jumping.

    // Walking time variables.
    // Bunny will walk in a cycle, walking a bit, then waiting a bit, etc...
    public float minWalkSpeed;
    public float maxWalkSpeed;
    private float walkSpeed;
    public float minWalkTime;  // All time variables will be in seconds.
    public float maxWalkTime;
    public float minWalkWaitTime;
    public float maxWalkWaitTime;
    private bool isWalking = false;
    private float walkTimer = 0;
    private int walkDirection = 1;

    // Aggro bunny variables
    private bool aggroSwitch = false;  // Used to do things when switching to aggro only once.
    private float jumpTimer = 0;
    public float jumpTime;
    public float jumpForce;
    public float jumpAngle;  // In degrees

    public Transform groundChecker;  // A point on the bunny, used to check for ground.
    public LayerMask groundLayerMask;  // Only check for ground using ground, and nothing else.
    private float groundCheckRadius;  // How large of a circle the checker is.
    private bool grounded;

    public Sprite passiveSprite;
    public Sprite aggroSprite;

    SpriteRenderer spriteRenderer;

    
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
        foreach (Transform child in transform) {
            if (child.tag == "GroundCheck") {
                groundCheckRadius = child.GetComponent<CircleCollider2D>().radius;
                break;
            }
        }
        //print(Mathf.Cos(Mathf.Deg2Rad * 60));
	}
	
	// Update is called once per frame
	void Update () {
        
        if (flowerScript.saturationPoints > 0) {  // If flower has stuffs in it.
            if (!isAggro) {
                isAggro = true;  // Bunny becomes aggro.
                aggroSwitch = true;
            }
        } else {
            if (isAggro) {
                isAggro = false;
                aggroSwitch = true;
            }
        }
        
        if (!isAggro) {
            // Passive mode
            walkTimer -= Time.deltaTime;  // Decrement timer
            if (aggroSwitch) {
                // Execute once when switching states
                aggroSwitch = false;
                spriteRenderer.sprite = passiveSprite;
            }
            if (walkTimer < 0) {
                // Timer reset
                isWalking = !isWalking;  // Switch walking/waiting state.
                if (isWalking) {
                    walkTimer = Random.Range(minWalkTime, maxWalkTime);
                    walkDirection = (Random.value < 0.5f) ? -1 : 1;  // Randomize walk direction (-1 = left, 1 = right)
                    walkSpeed = Random.Range(minWalkSpeed, maxWalkSpeed);

                } else {
                    walkTimer = Random.Range(minWalkWaitTime, maxWalkWaitTime);
                }
            }
            if (!isWalking) {
                // Waiting
                rigidbody2D.velocity = new Vector3(0, rigidbody2D.velocity.y, 0);
            } else {
                rigidbody2D.velocity = new Vector3(walkSpeed * walkDirection, rigidbody2D.velocity.y, 0);
            }
        } else { 
            // Aggro
            grounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayerMask);
            if (aggroSwitch) {
                // Execute once when switching states
                aggroSwitch = false;
                rigidbody2D.velocity = Vector3.zero;
                spriteRenderer.sprite = aggroSprite;
            }
            jumpTimer -= Time.deltaTime;
            if (grounded) {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }
            if (jumpTimer < 0) {
                jumpTimer = jumpTime;
                //float dist = Vector2.Distance(transform.position, player.position);  // Distance between bunny and player.
                bool dirFromPlayer = transform.position.x < player.position.x;  // Where player is from bunny, true is right, false is left.
                float forceX = Mathf.Cos(Mathf.Deg2Rad * (dirFromPlayer? jumpAngle : 180 + jumpAngle)) * jumpForce;
                float forceY = Mathf.Sin(Mathf.Deg2Rad * jumpAngle) * jumpForce;
                rigidbody2D.AddForce(new Vector2(forceX, forceY));
            }
        }
	}
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" && isAggro) { 
            
        }
    }
}
