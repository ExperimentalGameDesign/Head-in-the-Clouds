﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectScreen : MonoBehaviour {
	
	public GameObject selectSprite;//, redBall, blueBall, greenBall;
	public GameController game;
	private GameObject temp, ballTemp, ground;
	public List<GameObject> ballFabs;
	public List<GameObject> balls;
	private GameObject[] selectors;

		
	// Use this for initialization
	void Start () {
		balls = new List<GameObject>();
		//temp = GameObject.Instantiate (selectSprite, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		//temp.transform.localScale = new Vector3 (10.0f, 19.0f, 0.0f);
		float tempY = 0.0f;
		for (int i = 0; i < ballFabs.Count; i++) {
			if (i%2 == 0) {
				if (ballFabs[i].name == "FaceSelectButton" || ballFabs[i].name == "SelectaBallText") {
					ballTemp = GameObject.Instantiate (ballFabs[i], camera.ScreenToWorldPoint(new Vector3 ((Screen.width/2), (Screen.height - (Screen.height/(ballFabs.Count+5)) * (i + 2)), 10.0f)), Quaternion.identity) as GameObject;
					balls.Add (ballTemp);
				}
				else {
					ballTemp = GameObject.Instantiate (ballFabs[i], camera.ScreenToWorldPoint(new Vector3 ((Screen.width/(3) * (i%2 + 1)), (Screen.height - (Screen.height/(ballFabs.Count+5)) * (i + 2)), 10.0f)), Quaternion.identity) as GameObject;
					balls.Add (ballTemp);
				}

			}
			else {
				ballTemp = GameObject.Instantiate (ballFabs[i], camera.ScreenToWorldPoint(new Vector3 ((Screen.width/(3) * (i%2 + 1)), (Screen.height - (Screen.height/(ballFabs.Count+5)) * (i + 1)), 10.0f)), Quaternion.identity) as GameObject;
				balls.Add (ballTemp);
			}
		}
		//GameObject.Find("FaceSelect").transform.position = new Vector3 (0.0f, (-Screen.height/3) * (7), 0.0f);

	}
	
	// Update is called once per frame
	void Update () {
		//mousebuttonup because it will start drawing a line otherwise.
		if (Input.GetMouseButtonUp (0)) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if (hit.collider != null && hit.collider.name != "GroundTexture" && hit.collider.name != "PauseButton"
			    && hit.collider.name != "PickNewBallButton" && hit.collider.name != "PlayAgainButton" && hit.collider.name != "QuitButton") {
				GameObject Ad = GameObject.Find("AdvertisementManager");
				//Ad.GetComponent<AdvertisementManager>().hide();
				if(hit.collider.name == "FaceSelectButton(Clone)") {
					GetComponent<cameraScript>().enabled = true;
					game.playerChoice = (GameObject)Instantiate(Resources.Load("Face"), new Vector3 (0.0f, 0.0f, -1.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
					GetComponent<cameraScript>().picturePlane = game.playerChoice;
				}
				else {
					game.playerChoice = GameObject.Find(hit.collider.gameObject.name);
					GameObject.Find("greyDashedLine").GetComponent<SpriteRenderer>().enabled = true;
					GameObject.Find("DrawALineText").GetComponent<SpriteRenderer>().enabled = true;
					for (int i = 0; i < balls.Count; i++)
					game.playerPicked = true;
					GetComponent<draw>().enabled = true;
					//GameObject.Find("PauseButton").GetComponent<PauseMenu> ().enabled = true;
				}
				for (int i = 0; i < balls.Count; i++) 
					if (balls[i].name != game.playerChoice.name)
						GameObject.Destroy(balls[i]);
				
				selectors = GameObject.FindGameObjectsWithTag("Select Stuff");

				for (int i = 0; i < selectors.Length; i++)
					GameObject.Destroy(selectors[i]);


				//ground = (GameObject)Instantiate(Resources.Load("GroundTexture"));
				enabled = false;
			}
		}
	}
	
}
