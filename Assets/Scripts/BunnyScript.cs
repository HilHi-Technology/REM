using UnityEngine;
using System.Collections;

public class BunnyScript : MonoBehaviour {
    
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



    private Rigidbody2D rigidbody2D;
    
	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        walkTimer -= Time.deltaTime;  // Decrement timer
        if (walkTimer < 0) {
            // Timer reset
            isWalking = !isWalking;  // Switch walking/waiting state.
            print(isWalking);
            if (isWalking) {
                walkTimer = Random.Range(minWalkTime, maxWalkTime);
                walkDirection = (Random.value < 0.5f)? -1 : 1;  // Randomize walk direction (-1 = left, 1 = right)
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
	}
}
