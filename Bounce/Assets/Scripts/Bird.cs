using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
	public Vector3 startPoint;
	public Vector3 endPoint;
	public string screenStart;
	float CurveX;
	float CurveY;
	float BezierTime;
	Vector3 controlPoint;
	float halfway;
	
	// Use this for initialization
	void Start () {
		startPoint = transform.position;
		BezierTime = 0;
		endPoint = new Vector3 (-transform.position.x, transform.position.y, 0); //+ new Vector3(50, 0, 0);
		halfway = startPoint.x + endPoint.x;
		controlPoint = new Vector3(halfway, transform.position.y - 15, 0);
		
		//destination = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
			if (hit.collider != null) {
				if(hit.collider.name == this.name) {
					Debug.Log (this.name);//Player touched the Start Button
					Destroy (gameObject);
				}
			}
		}
		BezierTime = BezierTime + Time.deltaTime;
		
		
		if (BezierTime >= 2)
		{
			BezierTime = 0;
			Destroy(gameObject);
		}
		
		CurveX = (((1-BezierTime)*(1-BezierTime)) * startPoint.x) + (2 * BezierTime * (1 - BezierTime) * controlPoint.x) + ((BezierTime * BezierTime) * endPoint.x);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * startPoint.y) + (2 * BezierTime * (1 - BezierTime) * controlPoint.y) + ((BezierTime * BezierTime) * endPoint.y);
		
		
		
		if (transform.position != endPoint)
		{
			if (screenStart == "right")
			{
				transform.rotation = new Quaternion(0,0,0,0);
			}
			else if (screenStart == "left")
			{
				transform.rotation = new Quaternion(0,180,0,0);
			}
			Vector3 temp = new Vector3(CurveX, CurveY, 0);
			transform.position = Vector3.MoveTowards(transform.position, temp, 50.0f*Time.deltaTime);
			//transform.position = new Vector3(CurveX, CurveY, 0);
		}
		
	}
}
