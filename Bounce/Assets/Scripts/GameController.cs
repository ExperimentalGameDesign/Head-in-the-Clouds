using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GUISkin customSkin;
	public GameObject player; 
	public GameObject playerChoice, threadPickup, whiteCloud, darkCloud, birdy;
	public bool playerPicked = false, facePicked = false;
	public bool isGameOver, started;
	public float actualScore, thread, spawnPoint;
	public List<GameObject> threadList, whiteCloudList, darkCloudList;
	public GameObject leaderboard;
	public bool leaderboardCreated;
	public GameObject actualLeaderboard;
	public TilingBackground tiler;
	public GUIStyle customStyle;
	private GameObject[] deletables;
	private bool toggleButton;
	private string whiteCloudType;
	private string darkCloudType;


	// Use this for initialization
	void Start () {
		leaderboardCreated = false;
		toggleButton = false;
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
		if (thread < 0)
			thread = 0;
		if (playerChoice != null && playerPicked == true) {
			player = GameObject.Find (playerChoice.name);
			tiler.player = player;
			if (GameObject.Find("ModularSpriteKing(Clone)")) {
				GameObject newTile = GameObject.Find("ModularSpriteKing(Clone)");
				newTile.GetComponentInChildren<TilingBackground> ().player = player;
			}
			if (player.name == "Face(Clone)") {
//				continue;
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
			started = true;
			if (started && !isGameOver && GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused == false)
				GetComponent<draw>().enabled = true;
			if (facePicked) {
				GameObject tempPlayer = (GameObject)Instantiate(Resources.Load("FaceSprite 1"), new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
				player.transform.localScale = player.transform.localScale / 5;
				player.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
				player.transform.parent = tempPlayer.transform;
				player = tempPlayer;
				tempPlayer.GetComponent<Rigidbody2D>().gravityScale = 5.0f;
				facePicked = false;
			}
			if (player.transform.position.y >= 1200)
			{
				GetComponent<draw>().inSpace = true;
				whiteCloudType = "SpaceWhiteCloud";
				darkCloudType = "SpaceDarkCloud";
			}
			else
			{
				whiteCloudType = "WhiteCloud";
				darkCloudType = "DarkCloud";
			}
			//this is what turns off drawing if out of h2o
			if (thread <= 0)
			{
				GetComponent<draw>().enabled = false;
			}
			if (player.transform.position.y >= spawnPoint) {
				whiteCloud = (GameObject)Instantiate(Resources.Load(whiteCloudType), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				darkCloud = (GameObject)Instantiate(Resources.Load(darkCloudType), new Vector3(Random.Range(-30, 30), Random.Range(player.transform.position.y + 80, player.transform.position.y + 150), 0.0f), Quaternion.identity);
				int randDirection = Random.Range (0,2);
				if (randDirection == 0) //Bird comes from left of screen
				{
					if (player.transform.position.y < 1200) {
						birdy = (GameObject)Instantiate(Resources.Load("Bird"), new Vector3(/*Screen.width*/ -35, Random.Range(player.transform.position.y + 25, player.transform.position.y + 50), 0.0f), Quaternion.identity);
						birdy.GetComponent<Bird>().screenStart = "left";
					}
					else {
						birdy = (GameObject)Instantiate(Resources.Load("AstroBird"), new Vector3(/*Screen.width*/ -35, Random.Range(player.transform.position.y + 25, player.transform.position.y + 50), 0.0f), Quaternion.identity);
						birdy.GetComponent<Bird>().screenStart = "left";
					}
				}
				else //Bird comes from right of screen
				{
					if (player.transform.position.y < 1200) {
						birdy = (GameObject)Instantiate(Resources.Load("Bird"), new Vector3(/*Screen.width*/ 35, Random.Range(player.transform.position.y + 25, player.transform.position.y + 50), 0.0f), Quaternion.identity);
						birdy.GetComponent<Bird>().screenStart = "right";
					}
					else {
						birdy = (GameObject)Instantiate(Resources.Load("AstroBird"), new Vector3(/*Screen.width*/ 35, Random.Range(player.transform.position.y + 25, player.transform.position.y + 50), 0.0f), Quaternion.identity);
						birdy.GetComponent<Bird>().screenStart = "right";
					}
				}

				//threadList.Add(threadPickup);
				whiteCloudList.Add(whiteCloud);
				darkCloudList.Add(darkCloud);
				spawnPoint = player.transform.position.y + 80;
			}
			if (player.transform.position.y > transform.position.y && !isGameOver) {
				transform.position = Vector3.Lerp(transform.position, new Vector3 (transform.position.x, player.transform.position.y, transform.position.z), Time.deltaTime*2);
			}
		}

	}

	void OnGUI(){
		//GUI.skin = customSkin;
		//customStyle = new GUIStyle("box");
		//customStyle.font = fontType;
		customStyle.alignment = TextAnchor.UpperLeft;
		Vector3 camVect = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		Vector2 resolution = new Vector2(Screen.width, Screen.height);
		float resx = resolution.x/265.0f; // 1280 is the x value of the working resolution
		float resy = resolution.y/398.0f; // 800 is the y value of the working resolution
		//print ("screen width = " + Screen.width);
		//print ("screen height = " + Screen.height);

		int fontSize = (int)(15.0f*resx);
		customStyle.fontSize = fontSize;
		customSkin.customStyles [12].fontSize = fontSize;
			//(int)(camVect.x/2.0f);
		//customStyle.normal = null;

		if(player != null){
			if(player.transform.position.y > actualScore && isGameOver == false && started == true){
				actualScore = player.transform.position.y;
			}
			int tempThread = (int)thread;
			string theScore = "Score: " + actualScore.ToString ("F2");
			string threadLeft = "H20: " + tempThread.ToString();

			//GUI.Box (new Rect (0.0f, 0.0f, camVect.x, camVect.y / 2.0f), theScore, customStyle);
			//GUI.Box (new Rect (0.0f, fontSize+5.0f, camVect.x, camVect.y / 2.0f), threadLeft, customStyle);
			string firstText = "";

			if (toggleButton == false && !isGameOver) //displaying Pause button
			{
				if(GUI.Button (new Rect (((Screen.width+(861.0f/5.0f*resx))/2.0f) , 10.0f*resy, 35*resx, 30.0f*resy), firstText, customSkin.customStyles[10]) || (Input.GetKey(KeyCode.Escape))){
					//if(GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused == false) {
					GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused = true;
					GetComponent<draw>().enabled = false;
					//}
					/*else if (GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused == true) {
						GetComponent<draw>().enabled = false;
						GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused = false;
					}*/
					toggleButton = true;
				}
			}
			else if (!isGameOver)//toggleButton == true, displaying Play button
			{
				if(GUI.Button (new Rect (((Screen.width+(861.0f/5.0f*resx))/2.0f) , 10.0f*resy, 35*resx, 30.0f*resy), firstText, customSkin.customStyles[11]) || (Input.GetKey(KeyCode.Escape))){
					/*if(GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused == false) {
						GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused = true;
					}
					else if (GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused == true) {*/
					GetComponent<draw>().enabled = false;
					GameObject.Find("PauseButton").GetComponent<PauseMenu>().isPaused = false;

					toggleButton = false;

					//}
				}

			}


			if(!isGameOver) {
				GUI.Box (new Rect (0.0f, 0.0f, 70*resx, 15*resy), theScore, customStyle);
				GUI.Box (new Rect (0.0f, fontSize+5.0f, 70*resx, 15*resy), threadLeft, customStyle);
			}
			else if (isGameOver) {
				//customSkin.customStyles[12].overflow.left = theScore.Length + 30;
				//customSkin.customStyles[12].overflow.right = theScore.Length + 30;
				player.rigidbody2D.gravityScale = 0;
				player.rigidbody2D.velocity = Vector2.zero;
				player.rigidbody2D.isKinematic = true;
				player.transform.rotation = Quaternion.Euler(0,0,0);
				player.transform.localScale = new Vector3(1.4f,1.4f,1.0f);
				player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2 + (127*resy), 10));

				GUI.Box (new Rect (Screen.width/2 - (((theScore.Length*fontSize)/2 + fontSize*(2*resx))/2), fontSize+10.0f, ((theScore.Length*fontSize)/2 + fontSize*(2*resx)), 20*resy), theScore, customSkin.customStyles[12]);
				//GUI.Box (new Rect (Screen.width/2 - 70, fontSize+5.0f, 70*resx, 15*resy), threadLeft, customStyle);
				//GUI.Box (new Rect (0.0f, Screen.height/2, Screen.width, Screen.height/4), "Game Over");
				if(!leaderboardCreated){
					leaderboardCreated = true;
					actualLeaderboard = (GameObject)Instantiate (leaderboard);
					actualLeaderboard.transform.GetComponent<LeaderboardScript>().score = actualScore;
					GameObject Ad = GameObject.Find("AdvertisementManager");
					//Ad.GetComponent<AdvertisementManager>().show ();
				}

				GetComponent<draw>().enabled = false;
				toggleButton = false;
			}
		}
	}
	public void ResetGame() {
		GameObject.Find ("GroundMusic").GetComponent<AudioSource> ().enabled = false;
		GameObject.Find ("GroundMusic").GetComponent<AudioSource> ().enabled = true;
		GetComponent<draw>().inSpace = false;
		player.rigidbody2D.isKinematic = false;
		player.rigidbody2D.gravityScale = 5;
		player.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		deletables = GameObject.FindGameObjectsWithTag("Deletables");
		for (int i = 0; i < deletables.Length; i++)
			GameObject.Destroy(deletables[i]);
		player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		player.transform.rotation = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
		player.rigidbody2D.velocity = new Vector2 (0.0f, 0.0f);
		player.rigidbody2D.angularVelocity = 0.0f;
		transform.position = new Vector3(0.0f, 0.0f, -10.0f);
		actualScore = 0.0f;
		thread = 100.0f;
		spawnPoint = 0.0f;
		leaderboardCreated = false;
		GameObject.Find ("floor").GetComponent<GameOver> ().audio.Stop ();
		GameObject.Find ("left_wall").GetComponent<GameOver> ().audio.Stop ();
		GameObject.Find ("right_wall").GetComponent<GameOver> ().audio.Stop ();
		GameObject.Find("floor").GetComponent<GameOver>().didFall = false;
		GameObject.Find("floor").GetComponent<GameOver>(). hasExploded = false;
		GameObject.Find("left_wall").GetComponent<GameOver>().didFall = false;
		GameObject.Find("left_wall").GetComponent<GameOver>().hasExploded = false;
		GameObject.Find("right_wall").GetComponent<GameOver>().didFall = false;
		GameObject.Find("right_wall").GetComponent<GameOver>().hasExploded = false;
		GameObject newTile = (GameObject)Instantiate(Resources.Load("ModularSpriteKing"), new Vector3(0.0f, 1707.0f, 0.0f), Quaternion.identity);
		newTile.GetComponentInChildren<TilingBackground> ().player = player;
		//GetComponent<draw> ().enabled = true;
		toggleButton = false;
		isGameOver = false;
	}
	public void PickNewBall() {
		ResetGame ();
		playerPicked = false;
		playerChoice = null;
		GameObject.Destroy (player);
		player = null;
		GameObject.Find("greyDashedLine").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find("DrawALineText").GetComponent<SpriteRenderer>().enabled = false;
		//GameObject.Find("PauseButton").GetComponent<SpriteRenderer> ().enabled = false;
		//GameObject.Find("PauseButton").GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<SelectScreen> ().enabled = true;
		GetComponent<SelectScreen> ().Init ();
		GetComponent<draw> ().enabled = false;
	}
}