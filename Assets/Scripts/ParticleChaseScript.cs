using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleChaseScript : MonoBehaviour {
    public Transform target;  // The target for the particles to chase.

    public GameObject particleSystemObject;
    private ParticleSystem particleSystem;  // The original particle system.
    //private ParticleSystem.Particle[] particleList;  // The list that will contain all the particles

    public float particleAbsorbRadius;  // How far away from target will the particle disappear
    

    public GameObject flower;
    private Renderer flowerRenderer;

	// Use this for initialization
	void Start () {
        particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;  // Make the particles play in world space.
        flowerRenderer = flower.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("z")) {
            absorbParticle();
            flowerRenderer.material.SetFloat("_EffectAmount", 0.9f);
        }
	}

    void absorbParticle() {
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[particleSystem.particleCount];
        List<int> particleRemoveIndexList = new List<int>();
        particleSystem.GetParticles(particleList);
        for (int i = 0; i < particleList.Length; i++) {
            // All particles move toward target.
            particleList[i].velocity = new Vector3(target.position.x - particleList[i].position.x, target.position.y - particleList[i].position.y, -1);
            print(Vector2.Distance(particleList[i].position, target.position));
            if (Vector2.Distance(particleList[i].position, target.position) < particleAbsorbRadius) {
                // Slate particle for removal if it is in target's absorbtion radius
                particleRemoveIndexList.Add(i);
            }
        }
        particleRemoveIndexList.Sort();
        particleRemoveIndexList.Reverse();
        List<ParticleSystem.Particle> tempList = new List<ParticleSystem.Particle>(particleList);
        for (int i = 0; i < particleRemoveIndexList.Count; i++) {
            tempList.RemoveAt((int)particleRemoveIndexList[i]);            
        }
        particleList = tempList.ToArray();

            particleSystem.SetParticles(particleList, particleList.Length);



    }
}
