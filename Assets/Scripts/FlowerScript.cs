using UnityEngine;
using System.Collections;

public class FlowerScript : MonoBehaviour {
    [System.NonSerialized]
    public float saturation;  // 0 is completely gray.
    public float particleAttractionRadius;  // Only particles within a certain radius to the flower can be sucked in.
    public float particleAbsorbRadius;  // Particles close to the flower will disappear (absorbed).

    [System.NonSerialized]
    public int saturationPoints;  // Current saturation points gained by absorbing saturation particles, each particle = 1 point.
    public int maxSaturationPoints;  // Points needed to be fully saturated.

    private Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        saturationPoints = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (saturationPoints > maxSaturationPoints) {
            saturationPoints = maxSaturationPoints;
        }
        if (saturationPoints < 0) {
            saturationPoints = 0;
        }
        saturation = (float)saturationPoints / maxSaturationPoints;
        renderer.material.SetFloat("_EffectAmount", saturation);
	}
}
