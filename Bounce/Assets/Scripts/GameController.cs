using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private GameObject player; 
	public GameObject playerChoice, threadPickup, whiteCloud, darkCloud;
	public bool playerPicked = false;
	public bool isGameOver, tester;
	public float actualScore, thread, spawnPoint;
	public List<GameObject> threadList, whiteCloudList, darkCloudList;
	public GameObject leaderboard;
	public bool leaderboardCreated;
	public GameObject actualLeaderboard;


	// Use this for initialization
	void Start () {
		leaderboardCreated = false;
		thread = 100;
		actualScore = 0.0f;
		spawnPoint = 0.0f;
		//threadPickup = (GameObject)Instantiate(Resources.Load("Thread"));
	}
	void OnCollisionEnter2D(Collision2D thing)
	{

	}
	// Update is called once per frame
	void Update () {
		if (playerChoice != null && playerPicked == true) {
			player = GameObject.Find (playerChoice.name);
			
			if (player.name == "Face(Clone)") {
				player.transform.position = new Vector3 (0.0f, 0.0f, -1.0f);
				player.transform.rotation = new Quaternion(0.0f, -180.0f, 0.0f, 0.0f);
				this.GetComponent<cameraScript>().picturePlane = GameObject.Find(player.name);
			}
			
			else {
				player.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			}
			
			player.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
			this.GetComponent<CameraMove>().enabled = true;
			playerPicked = false;
		}
		if (playerChoice != null) {
			if (thread > 0 && isGameOver == false)
				this.GetComponent<draw>().enabled = true;
			else if (tester)
				thread = 100;
			else	 
				this.GetComponent<draw>().enabled = false;
			if (player.transform.position.y >= spawnPoint) {
				threadPickup = (GameObject)Instantiate(Resources.Load("Thread"), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				whiteCloud = (GameObject)Instantiate(Resources.Load("WhiteCloud"), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				darkCloud = (GameObject)Instantiate(Resources.Load("DarkCloud"), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				threadList.Add(threadPickup);
				whiteCloudList.Add(whiteCloud);
				darkCloudList.Add(darkCloud);
				spawnPoint = player.transform.position.y + 80;
			}
		}
		if (isGameOver) {
			if (Input.GetMouseButtonDown(0)) { 
				//Application.LoadLevel(0);
			}
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
				//GUI.Box (new Rect (0.0f, Screen.height/2, Screen.width, Screen.height/4), "Game Over");
				if(!leaderboardCreated){
					leaderboardCreated = true;
					actualLeaderboard = (GameObject)Instantiate (leaderboard);
					actualLeaderboard.transform.GetComponent<LeaderboardScript>().score = actualScore;
				}

				this.GetComponent<draw>().enabled = false;
			}
		}
	}
}
