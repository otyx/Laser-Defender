using UnityEngine;
using System.Collections;

public class EnemyBolt : MonoBehaviour {

	public float damage = 500f;

	public float GetDamage() {
		return damage;
	}

	public void Hit() {
		Destroy (gameObject);
	}
}
