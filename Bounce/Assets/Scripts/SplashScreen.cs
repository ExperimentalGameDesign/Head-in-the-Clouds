﻿using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {


	public GameObject splashSprite, selectSprite, faceSprite;
	private GameObject temp, temp2;
	private float fadeValue = 1.0f;
	private float currentTime = 0.0f;
	private const float timeItTakesToFade = 0.5f;
	private bool isFading = false;
	private SelectScreen selecter;
//	private bool isAppearing = false;

	// Use this for initialization
	void Start () {
		temp = GameObject.Find ("StartButton");
		//temp.transform.localScale = new Vector3 (5.939844f, 5.939858f, 0.0f);
		//temp2 = GameObject.Instantiate (selectSprite, new Vector3 (10.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		//temp2.transform.localScale = new Vector3 (10.0f, 19.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0) && isFading == false) {
			//StartFade ();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			if (hit.collider != null) {
				if(hit.collider.name == temp.name) {
					GameObject.Destroy(splashSprite);
					GetComponent<SelectScreen>().enabled = true;
					selectSprite.GetComponent<SpriteRenderer>().enabled = true;
					faceSprite.GetComponent<SpriteRenderer>().enabled = true;
					enabled = false;
				}
			}
		}
	}
		   
}
