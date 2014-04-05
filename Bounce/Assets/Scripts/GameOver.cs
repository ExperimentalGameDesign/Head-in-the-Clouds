using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameController game;
	// Use this for initialization
	void Start () {
	
	}
	//void OnCollisionEnter2D (Collision2D other) {
		//if (other.transform.name == game.playerChoice.name)
		//	game.isGameOver = true;

	//}
	void OnTriggerEnter2D (Collider2D thing) {
		if (thing.transform.name == game.playerChoice.name)
			game.isGameOver = true;
		else {
			Debug.Log(thing);
			GameObject.Destroy (thing.gameObject);
		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}
