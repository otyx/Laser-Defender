using UnityEngine;
using System.Collections;

public class EnemyBolt : MonoBehaviour {

	public float damage;
	public int value;

	public bool destructable;
	public ParticleSystem explosion;

	private ScoreKeeper scoreKeeper;

	public float GetDamage() {
		return damage;
	}

	public void Hit() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.layer == Tags.PLAYER_FIRE_LAYER && destructable) {
			// get the scoreKeeper
			ScoreKeeper scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
			scoreKeeper.Score (value);

			// we've been hit by player fire
			ParticlesManager.CreateParticleEffect (explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);

		}
	}
}
