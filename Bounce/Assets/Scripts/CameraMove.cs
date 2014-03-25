using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	//private GameObject player;
	public GameObject ball, player;
	// Use this for initialization
	void Start () {
		player = ball;

	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y > transform.position.y) {
			transform.position = Vector3.Lerp(transform.position, new Vector3 (transform.position.x, player.transform.position.y, transform.position.z), Time.deltaTime*2);
		}
	}
}
