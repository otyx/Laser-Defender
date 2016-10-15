using UnityEngine;
using System.Collections;

public class ParticlesManager : MonoBehaviour {

	public static void CreateParticleEffect(ParticleSystem ps, Vector3 position, Transform parent, string name="Particles", int timeToDie=3) {
		ParticleSystem particles = GameObject.Instantiate (ps, position, Quaternion.identity) as ParticleSystem;
		particles.Play ();
		particles.transform.SetParent (parent);
		particles.name = name;
		Destroy(particles.gameObject,timeToDie);
	}

	public static void CreateParticleEffect(ParticleSystem ps, Vector3 position, Quaternion q, string name="", int timeToDie=3) {
		ParticleSystem particles = GameObject.Instantiate (ps, position, q) as ParticleSystem;
		particles.name = name;
		particles.Play ();
		Destroy(particles.gameObject,timeToDie);
	}

}
