using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleScript : MonoBehaviour {
    public GameObject particleSystemObject;
    private ParticleSystem particleSystem;  // The original particle system.

    private Transform flower;
    private FlowerScript flowerScript;

    private float grayScale;
    public float pointsDrainedPerParticle;  // Saturation is drained away from the node when particles are absorbed.
    private Renderer renderer;

    // Emission min and max scaled with grayscale.
    public float emissionMax; 
    public float emissionMin;

	// Use this for initialization
	void Start () {
        flower = GameObject.FindWithTag("Flower").transform;
        grayScale = 1;
        particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;  // Make the particles play in world space.
        flowerScript = flower.GetComponent<FlowerScript>();
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        grayScale = Mathf.Clamp01(grayScale);
        renderer.material.SetFloat("_EffectAmount", grayScale);
        particleSystem.emissionRate = ((emissionMax - emissionMin) * (grayScale)) + emissionMin;
        if (grayScale == 0) {
            particleSystem.emissionRate = 0;
        }

        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[particleSystem.particleCount];
        List<int> particleRemoveIndexList = new List<int>();
        particleSystem.GetParticles(particleList);
        if (Input.GetKey("z")) {
            // Pressed the button to attract particles
            for (int i = 0; i < particleList.Length; i++) {
                if (Vector2.Distance(particleList[i].position, flower.position) < flowerScript.particleAttractionRadius) {
                    // Particles move toward target.
                    particleList[i].velocity = new Vector3(flower.position.x - particleList[i].position.x, flower.position.y - particleList[i].position.y, -1);
                }
            }
        }
        for (int i = 0; i < particleList.Length; i++) {
            // Check every particle if it is in absorption radius.
            if (Vector2.Distance(particleList[i].position, flower.position) < flowerScript.particleAbsorbRadius) {
                // Slate particle for removal if it is in target's absorbtion radius.
                particleRemoveIndexList.Add(i);
            }
        }
        // Sort the particles removal list largest to smallest so when removing it won't change the index of the list.
        particleRemoveIndexList.Sort();
        particleRemoveIndexList.Reverse();
        // Create temp list of particles so you can do removal easily.
        List<ParticleSystem.Particle> tempList = new List<ParticleSystem.Particle>(particleList);
        for (int i = 0; i < particleRemoveIndexList.Count; i++) {
            // Remove all particles that are slated to be removed.
            tempList.RemoveAt((int)particleRemoveIndexList[i]);
            // Add saturation to the flower.
            flowerScript.grayScale += flowerScript.pointsPerParticleAbsorbed;
            grayScale -= pointsDrainedPerParticle;
        }
        // Convert the list back into an array.
        particleList = tempList.ToArray();
        // Apply the particle changes.
        particleSystem.SetParticles(particleList, particleList.Length);

	}
}
