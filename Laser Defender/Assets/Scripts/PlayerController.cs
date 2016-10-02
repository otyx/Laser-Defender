using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {
	// TODO remove Quick and dirty score hack
	public float score = 0;
	public Text scoretext;

	// speed modifier
	public float shipSpeedFactor = 15.0f;

	// player health level
	public float playerHealth;

	// laser bolt
	public GameObject boltprefab;

	// the explosion effect
	public GameObject explosion;

	// the sound effect
	public float boltSpeed;
	public float fireDelay;

	// constraints of movement
	private float xmin = -5;
	private float xmax = 5;
	private float padding;
	private Vector3 boltOffset = new Vector3 (0, -0.5f);
	private GameObject bolt;

	void Start() {
		padding = GetComponent<SpriteRenderer> ().bounds.extents.x;
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			// left arrow pressed
			transform.position += Vector3.left * shipSpeedFactor * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			// Right arrow pressed
			transform.position += Vector3.right * shipSpeedFactor * Time.deltaTime;
		} 

		if (Input.GetKeyDown (KeyCode.Space)) {
			// firing the laser
			InvokeRepeating ("Fire", 0f, fireDelay); 
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			// stop firing the laser
			CancelInvoke ("Fire"); 
		}

		scoretext.text = score.ToString();
		// restrict x movement
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void Fire() {
		bolt = Instantiate(boltprefab, transform.position - boltOffset, Quaternion.identity) as GameObject;
		bolt.name = Tags.PLAYER_BOLT;
		bolt.tag = Tags.PLAYER_BOLT;
		bolt.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, boltSpeed, 0);

		// DIRTY HACK
		score -= 1;
		score = (score < 0)? 0 : score;

	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag.Equals (Tags.ENEMY_BOLT)) {
			
			// activate hit on bolt
			EnemyBolt bolt = col.gameObject.GetComponent<EnemyBolt>();

			// take the hit
			// to do remove health
			playerHealth -= bolt.GetDamage();

			if (playerHealth <= 0) {
				Die ();
			}

			// eliminate the bolt
			bolt.Hit();

		}
	}

	void Die() {
		Destroy(Instantiate (explosion, transform.position, Quaternion.identity), 1.0f);
		Destroy (gameObject);
	}
}
