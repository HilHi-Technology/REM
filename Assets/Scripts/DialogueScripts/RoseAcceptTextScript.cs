using UnityEngine;
using System.Collections;

public class RoseAcceptTextScript : TextScript {

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        if (triggered) {
            textComponent.enabled = true;
        }
	}
}
