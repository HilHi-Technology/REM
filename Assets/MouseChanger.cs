using UnityEngine;
using System.Collections;

public class MouseChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	float x = Input.mousePosition.x;
	
	if(x >= Screen.width/2)
	{
		GetComponent<SpriteRenderer>().color = Color.red;

	}
		else
		{
			GetComponent<SpriteRenderer>().color = Color.blue;

		}	
	
	}
}
