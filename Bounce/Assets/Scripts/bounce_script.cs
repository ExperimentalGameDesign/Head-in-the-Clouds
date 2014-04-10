using UnityEngine;
using System.Collections;

public class bounce_script : MonoBehaviour {
	
	public float bounceForce = 2000.0f, xbounceForce = 2000.0f, threadCount = 10.0f, maxThread = 100.0f, cloudForce = 3000.0f, blackCloudForce = 15.0f;
	public GameController game;
	public AudioClip happyCloud, sadCloud;
	void Start () {
		GameObject temp = GameObject.Find ("Main Camera");
		game = temp.GetComponent<GameController> ();
	}

	void OnCollisionEnter2D(Collision2D thing)
	{
		//change force based on string length
		bounceForce = 35000 / thing.transform.localScale.x;
		xbounceForce = 5000 / thing.transform.localScale.x;
		//xbounceForce = 5000 / thing.transform.localScale.x;
		if (thing.transform.name == "Bird(Clone)") {
			if (!thing.collider.audio.isPlaying) {
				thing.collider.audio.Play();
			}
		}
		//se
		if(thing.transform.name != "floor" && thing.transform.name != "Thread") // && thing.transform.name != "left_wall" && thing.transform.name != "right_wall") 
		{
			//thing.collider.rigidbody2D.isKinematic = true;
			Vector3 temp = thing.contacts[0].normal;
			if (thing.transform.name == "left_wall" || thing.transform.name == "right_wall")
				temp = temp.normalized;
				temp = new Vector3(temp.x*xbounceForce, temp.y*bounceForce, temp.z);
				rigidbody2D.AddForce(new Vector2(temp.x,temp.y));


		}

	}
	
	void OnTriggerEnter2D(Collider2D thing) {
		//running into a thread pickup
		if (thing.transform.name == "Bird(Clone)") {
			if (!thing.audio.isPlaying) {
				thing.audio.Play();
			}
		}
		//send character up if hit a white cloud
		if (thing.name == "WhiteCloud(Clone)") {
			if (!thing.audio.isPlaying) {
				thing.audio.Play();
			}
			thing.GetComponent<CloudScript>().shrink = true;
			rigidbody2D.AddForce(new Vector2(0.0f, cloudForce));
			if (game.thread < maxThread-threadCount)
				game.thread += threadCount;
			//if not then just make it the max thread so that you don't have more than the max
			else
				game.thread = maxThread;

		}
		if (thing.name == "DarkCloud(Clone)") {
			if (!thing.audio.isPlaying) {
				thing.audio.Play();
			}
			thing.GetComponent<CloudScript>().shrink = true;
			rigidbody2D.velocity = rigidbody2D.velocity - new Vector2(0.0f, rigidbody2D.velocity.y/blackCloudForce);
			if (game.thread < maxThread-threadCount)
				game.thread += threadCount;
			//if not then just make it the max thread so that you don't have more than the max
			else
				game.thread = maxThread;
		}
	}

	void Update () {
		float velocityDiff = 0.0f;
		//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / 1.1f, rigidbody2D.velocity.y);

		//Mathf.Min (value1, value2);
		//float vel;
		//Mathf.Clamp(vel, min, max);

		//velocityDiff = rigidbody2D.velocity.x - 15;
		if (rigidbody2D.velocity.x < 0)
			rigidbody2D.velocity = new Vector2 (Mathf.Max(rigidbody2D.velocity.x, -15.0f), rigidbody2D.velocity.y);
		else 
			rigidbody2D.velocity = new Vector2 (Mathf.Min(rigidbody2D.velocity.x, 15.0f), rigidbody2D.velocity.y);

		/*else if (rigidbody2D.velocity.x < -15)
		{
			rigidbody2D.velocity = new Vector2 (-15.0f, rigidbody2D.velocity.y);
		}*/
	}
}
