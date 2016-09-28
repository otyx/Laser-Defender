using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	// the enemy prefab
	public GameObject enemyPrefab;

	// the enemy speed factor
	public float enemySpeedFactor = 1f;

	// the formation
	public float width = 10f;
	public float height = 5f;


	// constraints of movement
	public float xmin = -5;
	public float xmax = 5;

	// changes direction if flipped
	private int directionModifier = 1;
	private float padding;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
			enemy.gameObject.name = "Enemy";
		}

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		padding = width * 0.5f;
		xmin = leftBoundary.x + padding;
		xmax = rightBoundary.x - padding;
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width,height));
	}

	// Update is called once per frame
	void Update () {
		transform.position += Vector3.left * enemySpeedFactor * Time.deltaTime * directionModifier;

		// restrict x movement
		if (transform.position.x <= xmin || transform.position.x >= xmax) {
			directionModifier *= -1; 
			float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		}

	}
}
