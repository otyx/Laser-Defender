using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text myText = GetComponent<Text> ();
		myText.text = ScoreKeeper.GetInitialScore ().ToString ();
		ScoreKeeper.Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
