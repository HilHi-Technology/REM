using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleScript : MonoBehaviour {
    public GameObject particleSystemObject;
    private ParticleSystem particleSystem;  // The original particle system.

    private Transform flower;
    private FlowerScript flowerScript;

    private float saturation;  // 0 is gray
    private int saturationPoints;  // Current saturation points gained by absorbing saturation particles, each particle = 1 point.
    public int maxSaturationPoints;  // Points needed to be fully saturated.
    private Renderer renderer;

    // Emission min and max scaled with grayscale.
    public float emissionMax; 
    public float emissionMin;

    public float particleTurnSpeed;
	// Use this for initialization
	void Start () {
        flower = GameObject.FindWithTag("Flower").transform;
        saturationPoints = maxSaturationPoints;
        particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;  // Make the particles play in world space.
        flowerScript = flower.GetComponent<FlowerScript>();
        renderer = GetComponent<Renderer>();
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
        particleSystem.emissionRate = ((emissionMax - emissionMin) * (saturation)) + emissionMin;
        if (saturation == 0) {
            particleSystem.emissionRate = 0;
        }

        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[particleSystem.particleCount];
        List<int> particleRemoveIndexList = new List<int>();  // Used to destroy the particles.
        particleSystem.GetParticles(particleList);
        if (Input.GetKey("z")) {
            // Pressed the button to attract particles
            for (int i = 0; i < particleList.Length; i++) {
                if (Vector2.Distance(particleList[i].position, flower.position) < flowerScript.particleAttractionRadius) {
                    // Particles move toward target.
                    particleList[i].velocity = Vector3.Lerp(particleList[i].velocity, new Vector3(flower.position.x - particleList[i].position.x, flower.position.y - particleList[i].position.y, -1), Time.deltaTime * particleTurnSpeed);
                }
            }
        }
        List<ParticleSystem.Particle> tempList = new List<ParticleSystem.Particle>(particleList);
        for (int i = 0; i < tempList.Count; i++) {
            // Check every particle if it is in absorption radius.
            if (Vector2.Distance(tempList[i].position, flower.position) < flowerScript.particleAbsorbRadius) {
                // Remove the particle.
                tempList.RemoveAt(i);
                i--;
                // Add saturation to the flower.
                flowerScript.saturationPoints += 1;
                // Subtract saturation from this node.
                saturationPoints -= 1;
            }
        }
        // Convert the list back into an array.
        particleList = tempList.ToArray();
        // Apply the particle changes.
        particleSystem.SetParticles(particleList, particleList.Length);

	}
}
