using UnityEngine;
using System.Collections;

public class FlowerScript : MonoBehaviour {
    [System.NonSerialized]
    public float grayScale;
    public float particleAbsorbRadius;  // How far away from target will the particle disappear.

    // Each particle absorbed will cause flower to be more saturated and the node to be less saturated.
    public float pointsPerParticleAbsorbed;

    private Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        grayScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        grayScale = Mathf.Clamp01(grayScale);
        renderer.material.SetFloat("_EffectAmount", grayScale);
	}
}
