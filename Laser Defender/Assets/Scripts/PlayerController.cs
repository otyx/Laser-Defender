using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// speed modifier
	public float shipSpeedFactor = 15.0f;

	// laser bolt
	public GameObject boltprefab;
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
			Debug.Log ("Pressed Left");
			transform.position += Vector3.left * shipSpeedFactor * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			// Right arrow pressed
			Debug.Log ("Pressed Right");
			transform.position += Vector3.right * shipSpeedFactor * Time.deltaTime;
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			// firing the laser
			InvokeRepeating ("Fire", 0f, fireDelay); 
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			// stop firing the laser
			CancelInvoke ("Fire"); 
		}

		// restrict x movement
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void Fire() {
		bolt = Instantiate(boltprefab, transform.position - boltOffset, Quaternion.identity) as GameObject;
		bolt.name = Tags.PLAYER_BOLT;
		bolt.tag = Tags.PLAYER_BOLT;
		bolt.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, boltSpeed, 0);
	}
}
