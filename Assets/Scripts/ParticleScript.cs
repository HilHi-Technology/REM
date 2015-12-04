using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleScript : MonoBehaviour {
    public Transform target;  // The target for the particles to chase.

    public GameObject particleSystemObject;
    private ParticleSystem particleSystem;  // The original particle system.
    //private ParticleSystem.Particle[] particleList;  // The list that will contain all the particles

    public float particleAbsorbRadius;  // How far away from target will the particle disappear

    // Each particle absorbed will cause flower to be more saturated and the node to be less saturated
    public float pointsPerParticleAbsorbed;

    public GameObject flower;
    private FlowerScript flowerScript;

	// Use this for initialization
	void Start () {
        particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;  // Make the particles play in world space.
        flowerScript = flower.GetComponent<FlowerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[particleSystem.particleCount];
        List<int> particleRemoveIndexList = new List<int>();
        particleSystem.GetParticles(particleList);
        if (Input.GetKey("z")) {
            // Pressed the button to absorb particle
            for (int i = 0; i < particleList.Length; i++) {
                // All particles move toward target.
                particleList[i].velocity = new Vector3(target.position.x - particleList[i].position.x, target.position.y - particleList[i].position.y, -1);
                print(Vector2.Distance(particleList[i].position, target.position));
            }
        }
        for (int i = 0; i < particleList.Length; i++) {
            // Check every particle if it is in absorption radius.
            if (Vector2.Distance(particleList[i].position, target.position) < particleAbsorbRadius) {
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
            flowerScript.grayScale -= pointsPerParticleAbsorbed;
        }
        // Convert the list back into an array.
        particleList = tempList.ToArray();
        // Apply the particle changes.
        particleSystem.SetParticles(particleList, particleList.Length);

	}
}
