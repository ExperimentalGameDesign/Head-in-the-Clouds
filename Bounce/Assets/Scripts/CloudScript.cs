using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {
	Vector3 midSize;
	float smallestSize;
	public bool shrink;
	// Use this for initialization
	void Start () {
		midSize = new Vector3(transform.localScale.x/2.0f, transform.localScale.y/2.0f, 1);
		smallestSize = 0.5f;
		shrink = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x <= 0.3f)
		{
			Destroy(gameObject);
		}
		if (shrink == true)
		{
			if (transform.localScale.x > midSize.x)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, midSize, 5.0f*Time.deltaTime);
				if (transform.localScale.x < midSize.x + 0.2f)
				{
					midSize = new Vector3(0.1f, 0.1f, 1.0f);
					shrink = false;
				}
			}
		}
	}
}
