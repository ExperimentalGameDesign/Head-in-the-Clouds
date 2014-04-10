using UnityEngine;
using System.Collections;

public class SpaceSound : MonoBehaviour {
	public GameObject mainCam;
	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCam.transform.position.y > transform.position.y) {
			transform.position = new Vector3(transform.position.x, mainCam.transform.position.y, 0.0f);
		}
	}
}
