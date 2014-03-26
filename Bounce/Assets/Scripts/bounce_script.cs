using UnityEngine;
using System.Collections;

public class bounce_script : MonoBehaviour {
	
	public float bounceForce;
	// Use this for initialization
	void Start () {
		bounceForce = 2000;
	}
	void OnCollisionEnter2D(Collision2D thing)
	{
		bounceForce = 35000 / thing.transform.localScale.x;
		if(thing.transform.name != "floor") // && thing.transform.name != "left_wall" && thing.transform.name != "right_wall") 
		{
			Vector3 temp = thing.contacts[0].normal;
			if (thing.transform.name == "left_wall" || thing.transform.name == "right_wall") 
				print (temp);
			//temp*= 2000;
			//if (temp.magnitude >= 2000)
			//{
				temp = temp.normalized;
				temp = temp*bounceForce;
				rigidbody2D.AddForce(new Vector2(temp.x,temp.y));
			//}
			//else
			//	rigidbody2D.AddForce(new Vector2(temp.x,temp.y));

		}
	}
	// Update is called once per frame
	void Update () {
		//constantForce.force = Vector3.up *1;

	}
}
