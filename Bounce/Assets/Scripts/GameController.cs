using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GUISkin customSkin;
	public GameObject player; 
	public GameObject playerChoice, threadPickup, whiteCloud, darkCloud, birdy;
	public bool playerPicked = false, facePicked = false;
	public bool isGameOver, tester;
	public float actualScore, thread, spawnPoint;
	public List<GameObject> threadList, whiteCloudList, darkCloudList;
	public GameObject leaderboard;
	public bool leaderboardCreated;
	public GameObject actualLeaderboard;
	public TilingBackground tiler;


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
			tiler.player = player;
			if (player.name == "Face(Clone)") {
				player.transform.position = new Vector3 (0.0f, 0.0f, -1.0f);
				player.transform.rotation = new Quaternion(180.0f, -180.0f, 0.0f, 0.0f);
				player.transform.localScale = player.transform.localScale * 5;
				
			}
			
			else {
				player.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
				player.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
			}
			
			//player.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
			//			this.GetComponent<CameraMove>().enabled = true;
			playerPicked = false;
		}
		if (player != null) {
			if (facePicked) {
				//tempPlayer.GetComponentInChildren<gameObject>().renderer.material.mainTexture = 
				//GameObject tempPlayer = (GameObject)Instantiate(Resources.Load("FaceSprite"));
				//Destroy(player);
				//player = tempPlayer;

				//player.GetComponent<SpriteRenderer>().sprite = (Sprite)(Resources.Load ("testIMAGE"));
				//player.GetComponent<SpriteRenderer>().sprite = //(Sprite)(Resources.Load ("testIMAGE"));

				//player.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
				//player.GetComponent<bounce_script>().enabled = false;
				//player.GetComponent<Rigidbody2D>().Sleep();
				//player.GetComponent<CircleCollider2D>().enabled = false;
				GameObject tempPlayer = (GameObject)Instantiate(Resources.Load("FaceSprite 1"), new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
				player.transform.localScale = player.transform.localScale / 5;
				player.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
				player.transform.parent = tempPlayer.transform;
				player = tempPlayer;
				tempPlayer.GetComponent<Rigidbody2D>().gravityScale = 5.0f;

				facePicked = false;
			}
			if (thread > 0 && isGameOver == false)
			{
				this.GetComponent<draw>().enabled = true;
				//player.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
			}
			else if (tester)
				thread = 100;
			else	 
				this.GetComponent<draw>().enabled = false;
			if (player.transform.position.y >= spawnPoint) {

				//threadPickup = (GameObject)Instantiate(Resources.Load("Thread"), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				whiteCloud = (GameObject)Instantiate(Resources.Load("WhiteCloud"), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				darkCloud = (GameObject)Instantiate(Resources.Load("DarkCloud"), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				int randDirection = Random.Range (0,2);
				if (randDirection == 0) //Bird comes from left of screen
				{
					birdy = (GameObject)Instantiate(Resources.Load("Bird"), new Vector3(/*Screen.width*/ -35, Random.Range(player.transform.position.y + 25, player.transform.position.y + 50), 0.0f), Quaternion.identity);
					birdy.GetComponent<Bird>().screenStart = "left";
				}
				else //Bird comes from right of screen
				{
					birdy = (GameObject)Instantiate(Resources.Load("Bird"), new Vector3(/*Screen.width*/ 35, Random.Range(player.transform.position.y + 25, player.transform.position.y + 50), 0.0f), Quaternion.identity);
					birdy.GetComponent<Bird>().screenStart = "right";
				}

				//threadList.Add(threadPickup);
				whiteCloudList.Add(whiteCloud);
				darkCloudList.Add(darkCloud);
				spawnPoint = player.transform.position.y + 80;
			}
			if (player.transform.position.y > transform.position.y) {
				transform.position = Vector3.Lerp(transform.position, new Vector3 (transform.position.x, player.transform.position.y, transform.position.z), Time.deltaTime*2);
			}
		}

		if (isGameOver) {
			if (Input.GetMouseButtonDown(0)) { 
				//Application.LoadLevel(0);
			}
		}
	}

	void OnGUI(){
		GUI.skin = customSkin;

		if(player != null){
			if(player.transform.position.y > actualScore){
				actualScore = player.transform.position.y;
			}
			int tempThread = (int)thread;
			string theScore = "Score: " + actualScore.ToString ("F2");
			string threadLeft = "H20: " + tempThread.ToString();

			GUI.Box (new Rect (0.0f, 0.0f, Screen.width / 3.0f, Screen.height / 20.0f), theScore);
			GUI.Box (new Rect (0.0f, 20.0f, Screen.width / 3.0f, Screen.height / 20.0f), threadLeft);
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
