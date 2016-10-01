﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// the formation array
	public  GameObject[] EnemyFormations;
	private GameObject currentFormationPrefab = null;
	private GameObject activeFormation;

	// Use this for initialization
	void Start () {
		SpawnFormation ();
	}
		
	// Update is called once per frame
	void Update () {
		if (activeFormation.GetComponent<EnemyFormation> ().AllEnemiesDead ()) {
			Debug.Log ("All enemies dead!");
			Destroy (activeFormation);
			SpawnFormation ();
		}
	}

	private void SpawnFormation() {
		currentFormationPrefab = EnemyFormations[Random.Range(0, EnemyFormations.Length)];
		activeFormation = Instantiate (currentFormationPrefab, new Vector3(0,0,-1), Quaternion.identity) as GameObject;
		activeFormation.transform.parent = transform;
		activeFormation.name = Tags.ENEMY_FORMATION;
		activeFormation.GetComponent<EnemyFormation> ().SpawnWave ();
	}
}
