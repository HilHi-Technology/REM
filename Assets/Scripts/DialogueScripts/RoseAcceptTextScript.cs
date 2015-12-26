using UnityEngine;
using System.Collections;

public class RoseAcceptTextScript : TextScript {
    public string failureText;
    private FlowerScript flowerScript;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        flowerScript = GameObject.FindGameObjectWithTag("Flower").GetComponent<FlowerScript>();
        failureText = failureText.Replace("\\n", "\n");
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (flowerScript.saturationPoints < flowerScript.maxSaturationPoints) {
            text = failureText;
        } else {
            text = defaultText;
        }
	}
}
