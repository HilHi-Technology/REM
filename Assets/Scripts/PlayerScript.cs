#pragma warning disable 0108  // Use new keyword if hiding was intended
using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float speed;
    public float jumpStrength;
    public Transform groundChecker;  // A point on the player, used to check for ground.
    public LayerMask groundLayerMask;  // Only check for ground using ground, and nothing else.
    
    private float groundCheckRadius;  // How large of a circle the checker is.
    private bool grounded;  // Whether the player is on the ground, for jump checking.
    private Rigidbody2D rigidbody2D;
    private Transform flower;
    private FlowerScript flowerScript;

    public float invulnernableTime;  // Invulnerability period after damaged, in seconds.
    private float invulnerableTimer;
    private bool isInvulnerable = false;  // Player is invulnerable for a period after hit



	void Start () {
        // Get the player's rigidbody2D component for movement uses.
        rigidbody2D = GetComponent<Rigidbody2D>();
        foreach (Transform child in transform) {
            if (child.tag == "GroundCollider") {
                groundCheckRadius = child.GetComponent<CircleCollider2D>().radius;
                break;
            }
        }
        foreach (Transform child in transform) {
            if (child.tag == "Flower") {
                flower = child.transform;
                flowerScript = flower.GetComponent<FlowerScript>();
                break;
            }
        }
        //print(groundCheckerRadius);
	}
	

	// Update is called once per frame.
	void Update () {
        if (isInvulnerable) {
            invulnerableTimer -= Time.deltaTime;
        }
        if (invulnerableTimer < 0) {
            isInvulnerable = false;
        }

        // Make player move left or right, depending on which key is pressed.
        rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rigidbody2D.velocity.y);

        grounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayerMask);
        //print(grounded);
        if (grounded && Input.GetButton("Jump")) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpStrength);
            grounded = false;
        }
	}

    void OnTriggerStay2D(Collider2D col) {
        if (col.tag == "CreatureCollider") {
            CreatureScript creatureScript = col.GetComponentInParent<CreatureScript>();  // WARNING: Code bottle neck.
            if (creatureScript.isAggro && !isInvulnerable) {
                isInvulnerable = true;
                invulnerableTimer = invulnernableTime;
                flowerScript.damageQueue = creatureScript.damage;
            }
        }
    }
}
