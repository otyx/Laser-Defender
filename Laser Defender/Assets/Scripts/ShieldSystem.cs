using UnityEngine;
using System.Collections;

public class ShieldSystem : MonoBehaviour {

	// the shield sprite renderer
	private SpriteRenderer shieldSpriteRenderer;

	// the shield strength
	private float currentShieldStrength;

	// the shield sprite status images
	public Sprite[] ShieldSprites;

	// the initial shield strength value
	public int MaxShieldStrength;

	// the particle effect prefab
	public ParticleSystem shieldParticles;
	public ParticleSystem shieldsUp;
	public ParticleSystem shieldsDown;

	private ParticlesManager particlesManager;

	// Use this for initialization
	void Start () {
		// get the shield sprite renderer
		shieldSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		currentShieldStrength = MaxShieldStrength;
		UpdateShieldSprite ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.layer == Tags.ENEMY_FIRE_LAYER) {
			// we have been hit by enemy fire

			// activate hit on bolt
			EnemyBolt bolt = col.gameObject.GetComponent<EnemyBolt> ();

			// take the hit
			// to do remove health
			TakeHit (bolt.GetDamage ());

			// fire the particle effect and prime it for destruction
			ParticlesManager.CreateParticleEffect(shieldParticles, col.transform.position, transform, Tags.SHIELD_PARTICLES, 3);

			// eliminate the bolt
			bolt.Hit ();

		} else {
			// TODO deal with enemy collisions
		}
	}

	public void TakeHit(float hit) {
		currentShieldStrength -= hit;
		if (currentShieldStrength <= 0) {
			currentShieldStrength = 0;
			ShieldsDown ();
		} else {
			// shield still exists so update sprite if necessary
			UpdateShieldSprite ();
		}
	}

	private void ShieldsDown() {
		//shield destroyed
		shieldSpriteRenderer.GetComponent<Renderer> ().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		// show the shields down particles
		ParticlesManager.CreateParticleEffect(shieldsDown, transform.position, transform);
	}

	private void ShieldsUp() {
		shieldSpriteRenderer.GetComponent<Renderer> ().enabled = true;
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
		// show the shield up particles
		ParticlesManager.CreateParticleEffect(shieldsUp, transform.position, transform);
	}

	// recharge the shields
	public void chargeShield(float recharge) {
		if (currentShieldStrength <= 0) {
			ShieldsUp ();
		}

		currentShieldStrength += recharge;
		currentShieldStrength = (currentShieldStrength > MaxShieldStrength) ? MaxShieldStrength : currentShieldStrength;
		UpdateShieldSprite ();
	}
	
	private void UpdateShieldSprite() {
		// change the spriterenderer's sprite depending on shield strength
		int spriteIndex = Mathf.FloorToInt(currentShieldStrength/MaxShieldStrength * (ShieldSprites.Length -1));
		if (spriteIndex >= 0 && spriteIndex < ShieldSprites.Length) {
			shieldSpriteRenderer.sprite = ShieldSprites [spriteIndex];
		} else {
			Debug.LogError ("ShieldSystem: Can't set sprite index of " + spriteIndex);
		}
	}
}
