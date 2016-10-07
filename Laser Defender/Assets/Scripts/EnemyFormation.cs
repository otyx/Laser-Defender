using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	// the enemy prefab
	public GameObject[] enemyPrefabs;

	// the enemyController
	public EnemyController enemyController;

	// the enemy speed factor
	public float enemySpeedFactor = 1f;

	// the formation
	public float width; 
	public float height; 

	// constraints of movement
	public float xmin, ymin;
	public float xmax, ymax;

	// changes direction if flipped
	private int xDirectionModifier = 1;
	private float yDirectionModifier = 1.3f;

	private float xPadding;
	private float yPadding;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		Vector3 topBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, distance));
		xPadding = width * 0.2f;
		yPadding = height * 1.5f;
		xmin = leftBoundary.x + xPadding;
		xmax = rightBoundary.x - xPadding;
		ymin = leftBoundary.y + yPadding;
		ymax = topBoundary.y;// - yPadding;
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width,height));
	}

	// Update is called once per frame
	void Update () {
		// adjust the x position
		transform.position += Vector3.right * enemySpeedFactor * Time.deltaTime * xDirectionModifier;

		// adjust the y position
		transform.position += Vector3.down * enemySpeedFactor * Time.deltaTime * yDirectionModifier;

		// restrict x movement
		if (transform.position.x <= xmin || transform.position.x >= xmax) {
			xDirectionModifier *= -1; 
			float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		}

		// restrict y movement
		if (transform.position.y <= ymin || transform.position.y >= ymax) {
			yDirectionModifier *= -1; 
			float newY = Mathf.Clamp (transform.position.y, ymin, ymax);
			transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
		}
	}

	public void SpawnWave() {
		SpawnUntilFull ();
	}

	public void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject prefab = enemyPrefabs [Random.Range (0, enemyPrefabs.Length)];
			GameObject enemy = Instantiate (prefab , freePosition.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
			enemy.gameObject.name = Tags.ENEMY;
			if (NextFreePosition ()) {
				Invoke ("SpawnUntilFull", 0.5f);
			}
		}

	}

	Transform NextFreePosition() {
		foreach (Transform child in transform) {
			if (child.childCount == 0) {
				return child;
			} 
		}
		return null;
	}

	public bool AllEnemiesDead() {
		foreach (Transform child in transform) {
			if (child.childCount > 0) {
				return false;
			} 
		}
		return true;
	}
}
