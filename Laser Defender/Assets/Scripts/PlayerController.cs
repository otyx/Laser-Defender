using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {
	// debug mode
	public bool INDESTRUCTABLE;

	// speed modifier
	public float shipSpeedFactor = 15.0f;

	// acceleraometer sensitivity
	public float SENSITIVITY;
	public float ACCEL_SHIPSPEEDFACTOR;

	public float StartHealthLevel;

	// player health level
	private float playerHealth;

	// laser bolt
	public GameObject boltprefab;
	public AudioClip fireClip;

	// the explosion effect
	public ParticleSystem explosion;

	// the player hit effect
	public ParticleSystem PlayerHitParticles;

	// the sound effects
	public AudioClip playerHitClip;
	public AudioClip playerDestroyed;

	public float boltSpeed;
	public float fireDelay;

	// the shield system
	public ShieldSystem shieldSystem;

	// constraints of movement
	private float xmin = -5;
	private float xmax = 5;
	private float padding;
	private Vector3 boltOffset = new Vector3 (0, -0.5f);
	private GameObject bolt;

	private LevelManager levelManager;
	private ParticlesManager particlesManager;

	private Vector3 direction;

	// the scorekeeper
	private ScoreKeeper scoreKeeper;

	void Start() {
		padding = GetComponent<SpriteRenderer> ().bounds.extents.x;
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;

		playerHealth = StartHealthLevel;

		scoreKeeper = FindObjectOfType<ScoreKeeper> ();
		ScoreKeeper.Reset ();
		scoreKeeper.UpdateScoreText ();

		levelManager = GameObject.FindObjectOfType<LevelManager> ();

		shieldSystem = GetComponentInChildren<ShieldSystem>();
	}

	// Update is called once per frame
	void Update () {
		// read accelerometer input
		direction = Input.acceleration;

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			// left arrow pressed
			transform.position += Vector3.left * shipSpeedFactor * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			// Right arrow pressed
			transform.position += Vector3.right * shipSpeedFactor * Time.deltaTime;
		} else if (direction.x > SENSITIVITY || direction.x < SENSITIVITY * -1) {
			direction *= Time.deltaTime;
			direction *= shipSpeedFactor;
			transform.Translate (new Vector3(direction.x, 0 , 0));
		} 
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				// firing the laser
				Fire ();
			}
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) {
			// firing the laser
			InvokeRepeating ("Fire", 0f, fireDelay); 
		} else if (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.RightControl) || Input.GetKeyUp(KeyCode.LeftControl)) {
			// stop firing the laser
			CancelInvoke ("Fire"); 
		}

		// restrict x movement
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void Fire() {
		AudioSource.PlayClipAtPoint (fireClip, transform.position);
		bolt = Instantiate(boltprefab, transform.position - boltOffset, Quaternion.identity) as GameObject;
		bolt.name = Tags.PLAYER_BOLT;
		bolt.tag = Tags.PLAYER_BOLT;
		bolt.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, boltSpeed, 0);

		scoreKeeper.RegisterFiring (Tags.PLAYER_BOLT_SCORE_COST);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.layer == Tags.ENEMY_FIRE_LAYER || col.gameObject.layer == Tags.ENEMY_TORPEDO_LAYER || col.gameObject.layer == Tags.ENEMY_SHIPS_LAYER) {
			// we have been hit by enemy fire
			AudioSource.PlayClipAtPoint (playerHitClip, transform.position);
			float damage = 0;
			if (col.gameObject.layer == Tags.ENEMY_SHIPS_LAYER) {
				// collided with a ship
				damage = col.gameObject.GetComponent<Enemy>().enemyValue;
				//eliminate the enemy
				col.gameObject.GetComponent<Enemy>().Die();
			} else {
				// activate hit on bolt
				EnemyBolt bolt = col.gameObject.GetComponent<EnemyBolt> ();
				damage = bolt.GetDamage ();
				// eliminate the bolt
				bolt.Hit ();
			}

			if (!INDESTRUCTABLE || !shieldSystem.ShieldsAreUp()) {
				playerHealth -= damage;
			}

			ParticlesManager.CreateParticleEffect (PlayerHitParticles, col.transform.position, transform);

			if (playerHealth <= 0) {
				Die ();
			} else {
				// change colour of the sprite
				SpriteRenderer sr = GetComponent<SpriteRenderer> ();
				float healthTintLevel = playerHealth / StartHealthLevel;
				sr.color = new Color (sr.color.r, healthTintLevel, healthTintLevel);
				AudioSource.PlayClipAtPoint (playerHitClip, transform.position);
			}

		}
	}

	void Die() {
		levelManager.Invoke ("LoadLoseScreen", 3);
		ParticlesManager.CreateParticleEffect (explosion, transform.position, Quaternion.identity, Tags.PLAYER_EXPLOSION, 5);
		AudioSource.PlayClipAtPoint (playerDestroyed, transform.position);
		Destroy (gameObject);
	}
}
