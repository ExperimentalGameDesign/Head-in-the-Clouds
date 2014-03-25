using UnityEngine;
using System.Collections;

public class bounce_script : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter2D(Collision2D thing)
	{
		if(thing.transform.name != "floor") 
			rigidbody2D.AddForce(new Vector2(0,1750));
		//if (thing.transform.name == "floor")
		//	game.isGameOver = true;
		//Debug.Log (thing.transform.name);
	}
	// Update is called once per frame
	void Update () {
		//constantForce.force = Vector3.up *1;

	}
}
