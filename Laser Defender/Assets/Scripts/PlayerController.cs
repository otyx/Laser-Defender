using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// speed modifier
	[Range (0f, 1000f)]
	public float shipSpeedFactor;

	// the movement adjustment
	private Vector2 delta = new Vector2();
	private Vector2 STOP_VECTOR = new Vector2 ();

	// the rigidbody for physics
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			// up arrow pressed
			Debug.Log ("Pressed Up");
			move (0, 1);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			// down arrow pressed
			Debug.Log ("Pressed Down");
			move (0, -1);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			// left arrow pressed
			Debug.Log ("Pressed Left");
			move (-1, 0);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			// Right arrow pressed
			Debug.Log ("Pressed Right");
			move (1, 0);
		} else {
			// no movement key pressed
			Debug.Log("No Key pressed");
			StopMovement ();
		}

		ClampPosition ();
	}

	private void move (int x, int y) {
		delta.x = x * shipSpeedFactor * Time.deltaTime;
		delta.y = y * shipSpeedFactor * Time.deltaTime;

		rb.velocity = delta;
	}

	private void ClampPosition() {
		rb.position = new Vector2 (Mathf.Clamp (rb.position.x, -6f, 6f), Mathf.Clamp (rb.position.y, -4.5f, 4.5f));
	}

	private void StopMovement() {
		rb.velocity = STOP_VECTOR;
	}
}
