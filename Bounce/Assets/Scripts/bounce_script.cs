using UnityEngine;
using System.Collections;

public class bounce_script : MonoBehaviour {
	
	public float bounceForce;
	public GameController game;

	void Start () {
		bounceForce = 2000;
		GameObject temp = GameObject.Find ("Main Camera");
		game = temp.GetComponent<GameController> ();
	}

	void OnCollisionEnter2D(Collision2D thing)
	{
		bounceForce = 35000 / thing.transform.localScale.x;
		if(thing.transform.name != "floor" && thing.transform.name != "Thread") // && thing.transform.name != "left_wall" && thing.transform.name != "right_wall") 
		{
			Vector3 temp = thing.contacts[0].normal;
			if (thing.transform.name == "left_wall" || thing.transform.name == "right_wall")
				temp = temp.normalized;
				temp = temp*bounceForce;
				rigidbody2D.AddForce(new Vector2(temp.x,temp.y));


		}

	}
	
	void OnTriggerEnter2D(Collider2D thing) {
		if (thing.transform.name == "Thread(Clone)") {
			if (game.thread < 50.0f)
				game.thread += 50.0f;
			else
				game.thread = 100.0f;
			for (int i = 0; i < game.threadList.Count; i++) {
				if (thing.gameObject == game.threadList[i])
					GameObject.Destroy(game.threadList[i]);
			}
		}
		if (thing.name == "WhiteCloud(Clone)") {
			rigidbody2D.AddForce(new Vector2(0.0f, 3000.0f));
		}
		if (thing.name == "DarkCould(Clone)") {
						rigidbody2D.AddForce (new Vector2 (0.0f, -1000.0f));
		}
	}

	void Update () {

	}
}
