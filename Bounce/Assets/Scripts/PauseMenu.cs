using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseSprite, retrySprite, newBallSprite, quitSprite;
	public bool isPaused = false;
	// Use this for initialization
	void Start () {
		pauseSprite.transform.position = new Vector3 ( 26, 45, 0.0f);//Camera.main.ScreenToWorldPoint(new Vector3 (70*resx, 15*resy, 10.0f));
		pauseSprite.GetComponent<SpriteRenderer> ().enabled = true;
		pauseSprite.GetComponent<BoxCollider2D> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			
			if (hit.collider != null) {
				if (hit.collider.name == "PauseButton" && isPaused == false) {
					isPaused = true;
				}
				else if (hit.collider.name == "PauseButton" && isPaused == true) {
					isPaused = false;
				}
			}
			if (isPaused) {
				Time.timeScale = 0.0f;
				Camera.main.GetComponent<draw>().enabled = false;
				retrySprite.GetComponent<SpriteRenderer> ().enabled = true;
				retrySprite.GetComponent<BoxCollider2D> ().enabled = true;

				newBallSprite.GetComponent<SpriteRenderer> ().enabled = true;
				newBallSprite.GetComponent<BoxCollider2D> ().enabled = true;

				quitSprite.GetComponent<SpriteRenderer> ().enabled = true;
				quitSprite.GetComponent<BoxCollider2D> ().enabled = true;

			}
			if (isPaused == false) {
				Time.timeScale = 1.0f;
				Camera.main.GetComponent<draw>().enabled = true;
				retrySprite.GetComponent<SpriteRenderer> ().enabled = false;
				retrySprite.GetComponent<BoxCollider2D> ().enabled = false;
				
				newBallSprite.GetComponent<SpriteRenderer> ().enabled = false;
				newBallSprite.GetComponent<BoxCollider2D> ().enabled = false;
				
				quitSprite.GetComponent<SpriteRenderer> ().enabled = false;
				quitSprite.GetComponent<BoxCollider2D> ().enabled = false;
			}
		}
	}
}
