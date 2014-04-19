﻿using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameController game;
	public AudioClip falling, sploding;
	public bool hasExploded = false, didFall = false;
	// Use this for initialization
	void Start () {
	
	}
	//void OnCollisionEnter2D (Collision2D other) {
		//if (other.transform.name == game.playerChoice.name)
		//	game.isGameOver = true;

	//}
	void OnTriggerEnter2D (Collider2D thing) {
		if (thing.transform.name == game.player.name) {
			if(didFall == false) {
				audio.Play();
				didFall = true;
			}
			GameObject.Find("PauseButton").GetComponent<PauseMenu>().enabled = false;
			game.isGameOver = true;

		}
		else {
			if (thing.name != "ModularSprite") {
				Destroy (thing.gameObject);
				if (thing.transform.parent != null) {
					Destroy (thing.transform.parent.gameObject);
				}
			}
		}

	}
	void OnCollisionEnter2D (Collision2D thing)
	{
		print (thing);
		Destroy (thing.gameObject);
	}
	// Update is called once per frame
	void Update () {
		if (game.isGameOver && !hasExploded && didFall == true) {
			if (audio.isPlaying == false) {
				AudioSource.PlayClipAtPoint(sploding, transform.position);
				hasExploded = true;

			}
		}
	}
}
