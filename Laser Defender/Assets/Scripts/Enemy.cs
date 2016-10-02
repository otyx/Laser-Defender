using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// enemy health level
	public float health = 100f;

	// the enemy bolt
	public GameObject enemyBolt;

	// the enemy explosion
	public GameObject explosion;
	public AudioClip explosionclip;

	// enemy bolt speed
	public float enemyBoltSpeed = -5f;

	// fire frequency modifier
	public float fireFrequencyModifier = 2f;

	// the enemy's blt
	private GameObject bolt;
	private Animator anim;

	void Start() {
		anim = gameObject.GetComponent<Animator> ();

	}

	void Update() {
		// fire one bolt at a time.
		if ((Time.deltaTime * fireFrequencyModifier) > Random.value) {
			Fire ();
			if (Random.value < 0.1) {
				anim.Play (Tags.ENEMY_SPIN_ANIM);
			}
		} 
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag.Equals (Tags.PLAYER_BOLT)) {
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

	void Fire() {
		bolt = Instantiate (enemyBolt, transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.AngleAxis(180, Vector3.forward)) as GameObject;
		bolt.gameObject.name = Tags.ENEMY_BOLT;
		bolt.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, enemyBoltSpeed, 0);
	}

	void Die() {
		Destroy(Instantiate (explosion, transform.position, Quaternion.identity), 1.0f);
		Destroy (gameObject);
		GameObject plyr = GameObject.FindGameObjectWithTag (Tags.PLAYER) ;
		plyr.gameObject.GetComponent<PlayerController> ().score += 20;
	}
}
