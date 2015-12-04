using UnityEngine;
using System.Collections;

public class FlowerScript : MonoBehaviour {
    [System.NonSerialized]
    public float grayScale;

    private Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        grayScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Mathf.Clamp(grayScale, 0, 1);
        renderer.material.SetFloat("_EffectAmount", grayScale);
	}
}
