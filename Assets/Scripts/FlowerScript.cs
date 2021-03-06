﻿#pragma warning disable 0108  // Use new keyword if hiding was intended
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
    [System.NonSerialized]
    public int damageQueue;  // Damage queued up to be dealt in the form of losing particle.

    private Renderer renderer;
    public GameObject particleSystem;  // The particle system to be instantiated everytime flower takes damage.

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
        if (damageQueue > 0) {
            damageQueue = Mathf.Clamp(damageQueue, 1, saturationPoints);  // Can't lose more saturation than you have.
            // Creates a particle system which has a script that will direct particles to the nearest node.
            GameObject particleSystemObject = Instantiate(particleSystem, transform.position, Quaternion.identity) as GameObject;
            FlowerParticleScript particleScript = particleSystemObject.GetComponent<FlowerParticleScript>();
            GameObject[] ColorNodeList = GameObject.FindGameObjectsWithTag("ColorNode");  // Grabs all the color nodes.

            // Find the nearest color node
            float shortestDist = 99999;
            GameObject closestNode = default(GameObject);
            foreach (GameObject obj in ColorNodeList) {
                float dist = Vector2.Distance(transform.position, obj.transform.position);
                if ( dist < shortestDist) { 
                    shortestDist = dist;
                    closestNode = obj;
                }
            }

            ParticleSystem tempParticleSystem = particleSystemObject.GetComponent<ParticleSystem>();
            ParticleSystem nodeParticleSystem = default(ParticleSystem);
            foreach (Transform child in closestNode.transform) {
                if (child.tag == "ParticleSystem") {
                    nodeParticleSystem = child.GetComponent<ParticleSystem>();  // WARNING: Possible bottleneck.
                    break;
                }
            }
            tempParticleSystem.startColor = nodeParticleSystem.startColor;  
            tempParticleSystem.Emit(damageQueue);
            saturationPoints -= damageQueue;
            
            particleScript.target = closestNode.transform;
            
            damageQueue = 0;
        }
	}
}
