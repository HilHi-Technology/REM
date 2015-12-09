using UnityEngine;
using System.Collections;

public class BunnyScript : MonoBehaviour {
    public float speed;
    public float jumpStrength;
    public float jumpCooldown;  // Cooldown before bunny can jump again, in seconds.
    private float jumpTimer;  // Used to time jumping.
    private Rigidbody2D rigidbody2D;
    
	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        jumpTimer = jumpCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0) {
            rigidbody2D.velocity = new Vector3(speed, jumpStrength, 0);
            jumpTimer = jumpCooldown;
        }
	}
}
