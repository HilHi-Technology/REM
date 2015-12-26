using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {
    [System.NonSerialized]
    public bool triggered = false;
    protected Text textComponent;
    
    [System.NonSerialized]
    public string text;  // Actual text to be displayed. 
    // Text to be displayed, can be entered in the editor.
    public string defaultText;

	// Use this for initialization
	protected virtual void Start () {
        textComponent = GetComponent<Text>();
        textComponent.enabled = false;
        defaultText = defaultText.Replace("\\n", "\n");
        text = defaultText;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (triggered) {
            textComponent.enabled = true;
        }
	}
}
