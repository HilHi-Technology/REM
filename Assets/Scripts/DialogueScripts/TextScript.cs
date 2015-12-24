using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {
    [System.NonSerialized]
    public bool triggered = false;
    protected Text textComponent;
    public string text;  // Actual display text for the textbox, the one in the editor is just for show.
	// Use this for initialization
	protected virtual void Start () {
        textComponent = GetComponent<Text>();
        textComponent.enabled = false;
        text = text.Replace("\\n", "\n");
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (triggered) {
            textComponent.enabled = true;
        }
	}
}
