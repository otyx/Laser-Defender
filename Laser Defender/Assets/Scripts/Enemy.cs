using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// enemy health level
	public float health = 100f;

	// enemy value
	public int enemyValue = 20;

	// the enemy bolt
	public GameObject enemyBolt;

	// enemy hit effect
	public ParticleSystem hitParticles;

	// the enemy explosion
	public GameObject explosion;
	public AudioClip explosionclip;

	// enemy bolt speed
	public float enemyBoltSpeed = -5f;
	public AudioClip fireClip;

	// fire frequency modifier
	public float fireFrequencyModifier = 2f;

	// the enemy's bolt
	private GameObject bolt;
	private Animator anim;
	private ScoreKeeper scoreKeeper;

	void Start() {
		anim = gameObject.GetComponent<Animator> ();
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
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
			} else {
				ParticlesManager.CreateParticleEffect (hitParticles, col.transform.position, transform);
			}
			bolt.Hit();

		}
	}

	private void Fire() {
		bolt = Instantiate (enemyBolt, transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.AngleAxis(180, Vector3.forward)) as GameObject;
		bolt.gameObject.name = Tags.ENEMY_BOLT;
		bolt.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, enemyBoltSpeed, 0);
		AudioSource.PlayClipAtPoint (fireClip, transform.position);
	}

	public void Die() {
		Destroy(Instantiate (explosion, transform.position, Quaternion.identity), 1.0f);
		Destroy (gameObject);
		scoreKeeper.Score(enemyValue);
	}
}
