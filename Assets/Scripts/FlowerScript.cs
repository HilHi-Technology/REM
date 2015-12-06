using UnityEngine;
using System.Collections;

public class FlowerScript : MonoBehaviour {
    [System.NonSerialized]
    public float grayScale;
    public float particleAttractionRadius;  // Only particles within a certain radius to the flower can be sucked in.
    public float particleAbsorbRadius;  // Particles close to the flower will disappear (absorbed).

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
