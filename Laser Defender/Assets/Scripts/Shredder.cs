using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag.Equals(Tags.PLAYER_BOLT) || col.tag.Equals(Tags.ENEMY_BOLT)) {
			Destroy (col.gameObject);
		}
	}
}
