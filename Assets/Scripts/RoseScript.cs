using UnityEngine;
using System.Collections;

public class RoseScript : MonoBehaviour {
    public float grayScale;
    public Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Mathf.Clamp(grayScale, 0, 1);
        renderer.material.SetFloat("_EffectAmount", grayScale);
	}
}
