using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {
    [System.NonSerialized]
    public bool triggered = false;
    protected Text textComponent;
	// Use this for initialization
	protected virtual void Start () {
        textComponent = GetComponent<Text>();
        textComponent.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
