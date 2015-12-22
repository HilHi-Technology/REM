using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowerParticleScript : MonoBehaviour {
    private ParticleSystem particleSystem;
    [System.NonSerialized]
    public Transform target;  // Target for the particles to attract to. 
    private ParticleScript targetScript;
    private float targetAbsorbtionRadius;  // Particle will be absorbed by the target within this radius of the target.
    public float particleTurnSpeed;
	// Use this for initialization
	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        targetScript = target.GetComponent<ParticleScript>();
        targetAbsorbtionRadius = target.GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () {
        if (targetScript.saturationPoints == targetScript.maxSaturationPoints) {
            GameObject[] ColorNodeList = GameObject.FindGameObjectsWithTag("ColorNode");
            float shortestDist = 99999;
            GameObject closestNode = null;
            foreach (GameObject obj in ColorNodeList) {
                float dist = Vector2.Distance(transform.position, obj.transform.position);
                if (dist < shortestDist) {
                    ParticleScript tempObjScript = obj.GetComponent<ParticleScript>();
                    if (tempObjScript.saturationPoints != tempObjScript.maxSaturationPoints) { 
                        shortestDist = dist;
                        closestNode = obj;
                    }
                }
            }
            target = closestNode.transform;
            targetScript = target.GetComponent<ParticleScript>();
            targetAbsorbtionRadius = target.GetComponent<CircleCollider2D>().radius;
        }
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(particleList);
        for (int i = 0; i < particleList.Length; i++) {
            particleList[i].velocity = Vector3.Lerp(particleList[i].velocity, new Vector3(target.position.x - particleList[i].position.x, target.position.y - particleList[i].position.y, -1), Time.deltaTime * particleTurnSpeed);
        }
        List<ParticleSystem.Particle> tempList = new List<ParticleSystem.Particle>(particleList);
        for (int i = 0; i < tempList.Count; i++) {
            // Check every particle if it is in absorption radius.
            if (Vector2.Distance(tempList[i].position, target.position) < targetAbsorbtionRadius) {
                // Remove the particle.
                tempList.RemoveAt(i);
                i--;
                // Add saturation to the flower.
                targetScript.saturationPoints += 1;
            }
        }
        // Convert the list back into an array.
        particleList = tempList.ToArray();
        // Apply the particle changes.
        particleSystem.SetParticles(particleList, particleList.Length);
        if (particleSystem.particleCount == 0) {
            Destroy(gameObject);
        }
	}
}
