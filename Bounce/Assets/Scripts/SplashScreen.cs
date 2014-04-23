using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {


	public GameObject splashSprite, faceSprite, iArt, actualIArt;
	private GameObject startButton, instructionsButton;
	private bool didInstructions;
	//private SelectScreen selecter;
//	private bool isAppearing = false;

	// Use this for initialization
	void Start () {
		didInstructions = false;
		startButton = GameObject.Find ("StartButton");
		instructionsButton = GameObject.Find ("InstructionsButton");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape) && !didInstructions) {
			Application.Quit();
		}
		if (Input.GetMouseButtonUp(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			if (hit.collider != null) {
				if(hit.collider.name == startButton.name) {		//Player touched the Start Button
					GameObject.Destroy (startButton);
					if(!didInstructions){
						GameObject.Destroy(splashSprite);
						GameObject.Destroy (instructionsButton);
						didInstructions = true;
					}

					GameObject.Destroy (actualIArt);

					GetComponent<SelectScreen>().enabled = true;
					enabled = false;
				}else if(hit.collider.name == instructionsButton.name){	//Player touched the Instructions Button
					//Debug.Log ("instructions");
					startButton.transform.position = new Vector3(0.0f, -29.0f, startButton.transform.position.z);
					actualIArt = (GameObject)Instantiate (iArt, new Vector3(12.9f, -1.0f, -1.0f), Quaternion.identity);
					GameObject.Destroy (splashSprite);
					GameObject.Destroy (instructionsButton);
					if (Input.GetKey(KeyCode.Escape)) {
						Application.LoadLevel(0);
					}
				}
			}
		}
	}
		   
}
