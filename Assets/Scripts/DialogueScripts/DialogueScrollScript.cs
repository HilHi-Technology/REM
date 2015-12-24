using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueScrollScript : MonoBehaviour {
    private string previousText;  // Used to check if the text has been changed.
    private bool previousState;  // Used to check if the text's enabled state has changed.
    private Text textComponent;
    private int counter = 0;
    public float scrollDelay;  // Time in seconds between each letter's appearance.
    private float timer;
    private TextScript textScript;
	// Use this for initialization
	void Start () {
        textScript = GetComponent<TextScript>();
        textComponent = GetComponent<Text>();
        previousText = textScript.text;
        previousState = textComponent.enabled;
        timer = scrollDelay;
	}
	
	// Update is called once per frame
	void Update () {
        
        timer -= Time.deltaTime;
        print(counter);
        if (timer <= 0 && counter < previousText.Length) {
            print("Something");
            print(counter);
            textComponent.text += previousText[counter];
            timer = scrollDelay;
            counter++;
        }
        if (textScript.text != previousText || textComponent.enabled != previousState) {
            previousText = textScript.text;
            previousState = textComponent.enabled;

            // Start scrolling again everytime the text has changed in anyway.
            counter = 0;
            timer = scrollDelay;
            textComponent.text = "";
        } else if(!textComponent.enabled){
            textComponent.text = "";
        }

	}
}
