using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject player; 
	public GameObject playerChoice;
	public bool playerPicked = false;
	public bool isGameOver;
	public float actualScore, thread;

	// Use this for initialization
	void Start () {
		thread = 100;
		actualScore = 0.0f;
		//player = GameObject.Instantiate(playerChoice, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
	}
	void OnCollisionEnter2D(Collision2D thing)
	{
		//rigidbody2D.AddForce(new Vector2(0,1750));
		//if (thing.transform.name == "floor")
		//	game.isGameOver = true;
		Debug.Log (thing.transform.name);
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log (playerChoice);
		if (playerChoice != null && playerPicked == true) {
			player = GameObject.Find (playerChoice.name); 
			player.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			player.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
			this.GetComponent<CameraMove>().enabled = true;
			playerPicked = false;
		}
		if (playerChoice != null) {
			if (thread > 0 && isGameOver == false)
				this.GetComponent<draw>().enabled = true;
			else
				this.GetComponent<draw>().enabled = false;
		}
	}

	void OnGUI(){
		if(playerChoice != null){
			if(player.transform.position.y > actualScore){
				actualScore = player.transform.position.y;
			}
			string theScore = "Score: " + actualScore.ToString ("F2");
			string threadLeft = "Thread: " + thread.ToString("F2");
			GUI.Box (new Rect (0.0f, 0.0f, Screen.width / 3.0f, Screen.height / 20.0f), theScore);
			GUI.Box (new Rect (0.0f, 50.0f, Screen.width / 3.0f, Screen.height / 20.0f), threadLeft);
			if (isGameOver) {
				GUI.Box (new Rect (0.0f, Screen.height/2, Screen.width, Screen.height/4), "Game Over");
				this.GetComponent<draw>().enabled = false;
			}
		}
	}
}
