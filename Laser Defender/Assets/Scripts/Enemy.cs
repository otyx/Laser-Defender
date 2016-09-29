using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float health = 100f;

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag.Equals (Tags.PLAYER_BOLT)) {
			Debug.Log ("Hit by player bolt ! " + col.gameObject.GetComponent<Collider2D>().GetInstanceID() );

			// activate hit on bolt
			LaserBolt bolt = col.gameObject.GetComponent<LaserBolt>();

			// take the hit
			health -= bolt.GetDamage();

			if (health <= 0) {
				Die ();
			}
			bolt.Hit();

		}
	}

	void Die() {
		Destroy (gameObject);
	}
}
