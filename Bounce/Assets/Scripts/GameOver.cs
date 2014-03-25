using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameController game;
	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter2D (Collision2D other) {
		if (other.transform.name == game.playerChoice.name)
			game.isGameOver = true;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
