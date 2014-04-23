using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
	public GUISkin customSkin;
	private int isMuted;
	public GameObject splashSprite, faceSprite, iArt, actualIArt;
	private GameObject startButton, instructionsButton;
	private bool didInstructions;
	//private SelectScreen selecter;
//	private bool isAppearing = false;

	// Use this for initialization
	void Start () {
		isMuted = PlayerPrefs.GetInt("Muted");
		if (isMuted == null)
		{
			PlayerPrefs.SetInt ("Muted", 0); //set it to unmuted 0
			isMuted = PlayerPrefs.GetInt("Muted");
		}
		else if (isMuted == 0)
		{
			AudioListener.volume = 1;
		}
		else if (isMuted == 1)
		{
			AudioListener.volume = 0;
		}

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
					didInstructions = true;
					startButton.transform.position = new Vector3(0.0f, -29.0f, startButton.transform.position.z);
					actualIArt = (GameObject)Instantiate (iArt, new Vector3(12.9f, -1.0f, -1.0f), Quaternion.identity);
					GameObject.Destroy (splashSprite);
					GameObject.Destroy (instructionsButton);
					didInstructions = true;
				}
			}
		}
	}
		
	void OnGUI()
	{
		Vector2 resolution = new Vector2(Screen.width, Screen.height);
		float resx = resolution.x/265.0f; // 1280 is the x value of the working resolution
		float resy = resolution.y/398.0f; // 800 is the y value of the working resolution
		//if the game is not Muted, display the Mute symbol
		isMuted = PlayerPrefs.GetInt ("Muted");
		if (isMuted == 0)
		{
			if(GUI.Button (new Rect (((Screen.width+(861.0f/5.0f*resx))/2.0f) , 10.0f*resy, 35*resx, 30*resy), "", customSkin.customStyles[13]))
			{
				AudioListener.volume = 0;
				PlayerPrefs.SetInt("Muted", 1);
				isMuted = PlayerPrefs.GetInt("Muted");
			}
		}
		//if the game is Muted, display the Unmute symbol
		else if (isMuted == 1)
		{
			if(GUI.Button (new Rect (((Screen.width+(861.0f/5.0f*resx))/2.0f) , 10.0f*resy, 35*resx, 30*resy), "", customSkin.customStyles[14]))
			{
				AudioListener.volume = 1;
				PlayerPrefs.SetInt("Muted", 0);
				isMuted = PlayerPrefs.GetInt("Muted");
			}
		}
	}
}
