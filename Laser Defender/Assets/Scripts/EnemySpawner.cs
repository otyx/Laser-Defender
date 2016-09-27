using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	// the enemy prefab
	public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		GameObject enemy = Instantiate (enemyPrefab, new Vector3(), Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
		enemy.gameObject.name = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
