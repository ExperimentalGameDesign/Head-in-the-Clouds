﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectScreen : MonoBehaviour {
			
	public GameObject selectSprite;//, redBall, blueBall, greenBall;
	public GameController game;
	private GameObject temp, ballTemp;
	private float fadeValue = 1.0f;
	private float currentTime = 0.0f;
	private const float timeItTakesToFade = 0.5f;
	private bool isFading = false;
	private SelectScreen selecter;
	public bool splashing = true;
	public List<GameObject> ballFabs;
	public List<GameObject> balls;
	private float pos = -10.0f;
		//	private bool isAppearing = false;
		
		// Use this for initialization
	void Start () {
		balls = new List<GameObject>();
		//temp = GameObject.Instantiate (selectSprite, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		//temp.transform.localScale = new Vector3 (10.0f, 19.0f, 0.0f);
		for (int i = 0; i < ballFabs.Count; i++) {
			ballTemp = GameObject.Instantiate (ballFabs[i], new Vector3 (pos, 0.0f, 0.0f), Quaternion.identity) as GameObject;
			pos += 10;
			balls.Add (ballTemp);
		}

		//blueB = GameObject.Instantiate (blueBall, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		//greenB = GameObject.Instantiate (greenBall, new Vector3 (10.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
			//temp2 = GameObject.Instantiate (selectSprite, new Vector3 (10.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
			//temp2.transform.localScale = new Vector3 (10.0f, 19.0f, 0.0f);
	}
		
		// Update is called once per frame
	void Update () {
		//mousebuttonup because it will start drawing a line otherwise.
		if (Input.GetMouseButtonUp (0) /*&& splashing == false*/ && isFading == false) {
			//StartFade ();
			//this.GetComponent<SelectScreen>().splashing = false;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			if (hit.collider != null) {
				Debug.Log(hit.collider);
				game.playerChoice = GameObject.Find(hit.collider.gameObject.name);
				for (int i = 0; i < balls.Count; i++)
					if (balls[i].name != game.playerChoice.name)
						GameObject.Destroy(balls[i]);
				game.playerPicked = true;
				this.GetComponent<CameraMove>().ball = GameObject.Find(hit.collider.gameObject.name);
				StartFade();

				//GameObject.Destroy(redB);
				//GameObject.Destroy(greenB);
				//GameObject.Destroy(blueB);
				//StartFade ();
			}
		}
		if (isFading) {
			currentTime += Time.deltaTime;
			if(currentTime <= timeItTakesToFade){
				fadeValue = 1f - (currentTime / timeItTakesToFade);
				//temp.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, fadeValue);
				//redB.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, fadeValue);
				//blueB.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, fadeValue);
				//greenB.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, fadeValue);
			}
			else {
				GameObject.Destroy(temp);
				//GameObject.Destroy(redB);
				//GameObject.Destroy(greenB);
				//GameObject.Destroy(blueB);
			}
		}
	}
	private void StartFade() {
		currentTime = 0;
		isFading = true;

	}
		
}
