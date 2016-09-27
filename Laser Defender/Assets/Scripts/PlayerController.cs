using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// speed modifier
	public float shipSpeedFactor = 15.0f;
	public float padding;

	// constraints of movement
	float xmin = -5;
	float xmax = 5;

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
		}

		// restrict x movement
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}
}
