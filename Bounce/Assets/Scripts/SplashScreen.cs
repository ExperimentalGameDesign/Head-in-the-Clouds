using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {


	public GameObject splashSprite;
	private GameObject temp, temp2;
	private float fadeValue = 1.0f;
	private float currentTime = 0.0f;
	private const float timeItTakesToFade = 0.5f;
	private bool isFading = false;
	private SelectScreen selecter;
//	private bool isAppearing = false;

	// Use this for initialization
	void Start () {
		//temp = GameObject.Instantiate (splashSprite, new Vector3 (0.0f, -3.0f, 0.0f), Quaternion.identity) as GameObject;
		//temp.transform.localScale = new Vector3 (5.939844f, 5.939858f, 0.0f);
		//temp2 = GameObject.Instantiate (selectSprite, new Vector3 (10.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		//temp2.transform.localScale = new Vector3 (10.0f, 19.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && isFading == false) {
			//StartFade ();
			GameObject.Destroy(splashSprite);
			this.GetComponent<SelectScreen>().enabled = true;
		}
		if (isFading) {
			currentTime += Time.deltaTime;
			if(currentTime <= timeItTakesToFade){
				fadeValue = 1f - (currentTime / timeItTakesToFade);
				splashSprite.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, fadeValue);
			}
			else {
				GameObject.Destroy(splashSprite);
			}
		}
	}
	private void StartFade() {
		currentTime = 0;
		isFading = true;

	}
   
}
