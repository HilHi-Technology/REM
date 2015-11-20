using UnityEngine;
using System.Collections;

public class MouseChaser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	float x = Input.mousePosition.x;
	
	print(x);
	}
}