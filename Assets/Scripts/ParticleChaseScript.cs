using UnityEngine;
using System.Collections;

public class ParticleChaseScript : MonoBehaviour {
    public Transform target;  // The target for the particles to chase.

    private ParticleSystem.Particle[] particleList;
    private ParticleSystem particleSystem;  // The particle system 

	// Use this for initialization
	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("z")) {
            absorbParticle();
        }
	}

    void absorbParticle() {
        particleList = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(particleList);
        for (int i = 0; i < particleList.Length; i++) {
            particleList[i].velocity = new Vector3(target.position.x - particleList[i].position.x, target.position.y - particleList[i].position.y, 0);
        }
        particleSystem.SetParticles(particleList, particleList.Length);
    }
}
