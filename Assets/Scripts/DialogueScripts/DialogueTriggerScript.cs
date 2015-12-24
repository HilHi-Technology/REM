using UnityEngine;
using System.Collections;

public class DialogueTriggerScript : MonoBehaviour {
    public Transform dialogueToTrigger;
    private TextScript textScript;
	// Use this for initialization
	void Start () {
        textScript = dialogueToTrigger.GetComponent<TextScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D col) {
        print("Triggered");
        if (col.tag == "PlayerCollider") {
            textScript.triggered = true;
            Destroy(gameObject);
        }
    }
}
