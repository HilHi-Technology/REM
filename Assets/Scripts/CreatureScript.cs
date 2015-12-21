using UnityEngine;
using System.Collections;

public class CreatureScript : MonoBehaviour {
    protected Transform player;
    protected Rigidbody2D rigidbody2D;

    [System.NonSerialized]
    public bool isAggro = false;

    protected FlowerScript flowerScript;

	// Use this for initialization
	protected void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        flowerScript = GameObject.FindWithTag("Flower").GetComponent<FlowerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
