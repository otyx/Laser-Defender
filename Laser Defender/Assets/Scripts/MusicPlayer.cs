using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource> ();
			playClip(startClip);
		}

		// registerthe delegate with the scenemanager
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	// the delegate function named as a delegate in the scenemanager
	void OnSceneLoad(Scene scene, LoadSceneMode mode) {
		int level = scene.buildIndex;
		Debug.Log ("MUSICPLAYER: LevelLoaded is " + level);
		if (music) {
			music.Stop ();
		}
		if (level == 0) {
			playClip(startClip);
		} else if (level == 1) {
			playClip(gameClip);
		} else if (level == 2) {
			playClip(endClip);
		} 

	}

	private void playClip(AudioClip c) {
		if (music) {
			music.clip = c;
			music.loop = true;
			music.Play ();
		}
	}
}
