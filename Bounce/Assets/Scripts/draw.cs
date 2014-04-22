using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class draw : MonoBehaviour {
	public bool inSpace = false;
	private bool firstClick = true;
	private float distanceBetweenPos = 0;	
	Vector3 lastPos = new Vector3(-9999,-9999,-9999);
	Vector3 newPos;
	Vector3 firstPos;
	Vector3 firstCamPos;
	Vector3 lastCamPos;
	Vector3 newCamPos;
	float deltaCamPos;
	bool lastPosExists = false;
	bool ready = false;
	GameObject shape;
	//GameObject[] textureArray;
	List<GameObject> textureArray;
	Quaternion finalRotation;
	//public GameController game;
	
	// Use this for initialization
	void Start () {
		
		textureArray = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		//MouseUp means a line has ended
		if (Input.GetMouseButtonUp(0))
		{
			lastPosExists = false;		
			//print ("released");
			
			DrawTexture();
			textureArray = new List<GameObject>();
		}
		//MouseDown means a new line is being started
		if (Input.GetMouseButtonDown(0))
		{
			lastPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,50));
			firstCamPos = Camera.main.transform.position;
			lastCamPos = Camera.main.transform.position;
			firstPos = lastPos;
			lastPosExists = true;
			//firstClick = true;
			MakeShape (lastPos);
			//print ("here");
		}
		//Mouse is drawing!
		if (Input.GetMouseButton(0))
		{			
			//Create a ray from the previous and current mouse positions
			//Calculate distance between positions
			//Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			newPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y,0));//mouseRay.origin - mouseRay.direction / mouseRay.direction.y * mouseRay.origin.y;
			newCamPos = Camera.main.transform.position;
			deltaCamPos = newCamPos.y - lastCamPos.y;
			firstPos.y +=  + deltaCamPos;
			lastCamPos = newCamPos;
			//float yDist = newPos.y - lastPos.y; Ignoring y because it is unnecessary
			float xDist = newPos.x - lastPos.x;
			float zDist = newPos.y - lastPos.y;			
			distanceBetweenPos = Mathf.Sqrt((xDist*xDist) + (zDist*zDist)); //(yDist*yDist)
			// (distanceBetweenPos);
			
			if (distanceBetweenPos < 0)
			{
				distanceBetweenPos = -distanceBetweenPos;
			}	
			if (newPos != lastPos)
			{
				EditShape();
			}
			
		}
		
	}
	//Here's where rendering happens
	void MakeShape(Vector3 newPos)
	{
		string shapeType = "Cube";
		if (inSpace)
			shapeType = "SpaceCube";
		shape = (GameObject)Instantiate(Resources.Load(shapeType));
		shape.collider2D.enabled = false;
		lastPos = newPos;
	}
	
	void AddShape(Vector3 newPos)
	{
		GameObject textureSquare = new GameObject(); //(GameObject)Instantiate(Resources.Load("box"));
		textureSquare.transform.position = newPos;
		textureArray.Add(textureSquare);
		
		
	}
	void DrawTexture()
	{
		float xDist = lastPos.x - firstPos.x;
		float yDist = lastPos.y - firstPos.y;
		distanceBetweenPos = Mathf.Sqrt((xDist*xDist) + (yDist*yDist));
		GetComponent<GameController>().thread -= distanceBetweenPos;
		//print ("dist2 = " + distanceBetweenPos);
		Vector3 move_direction = new Vector3(lastPos.x-firstPos.x, lastPos.y-firstPos.y, 0);
		
		GameObject test;
		
		Quaternion new_finalRotation = new Quaternion(0,0,0,0);
		float tempZ = 0;
		bool adjusted = false;
		string endRight = "DawCloudEndRight";
		string endLeft = "DawCloudEndLeft";
		string tileMid = "tilingCloudTexture2";
		if (inSpace)
		{
			endRight = "TilingNebulaEndRight";
			endLeft = "TilingNebulaEndLeft";
			tileMid = "TilingNebula2";
		}
		
		
		for (float i=0.0f; i<distanceBetweenPos; i+=1.28f)
		{
			//GameObject test = (GameObject)Instantiate(Resources.Load("box"));
			//test.transform.rotation = finalRotation;
			if (finalRotation.eulerAngles.z >= 90 && finalRotation.eulerAngles.z <= 270)
			{
				tempZ = finalRotation.eulerAngles.z - 180;
				//Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, new_value));
				adjusted = true;
				new_finalRotation = Quaternion.Euler(new Vector3(0f, 0f, tempZ));
			}
			else
				new_finalRotation = finalRotation;
			
			//first node
			if (i==0.0f)
			{
				test = (GameObject)Instantiate(Resources.Load(endRight)); //tilingCloudTexture2"));
				if (adjusted)
					new_finalRotation = Quaternion.Euler(new Vector3(0f, 180.0f, 360.0f-tempZ));
				
				//test.transform.rotation = new Quaternion(0,180,0,0);
				//new_finalRotation = new Quaternion(0, 0, finalRotation.z, 0);
				//print ("first" + finalRotation.z);
			}
			//last node 
			else if (i+1.28f >= distanceBetweenPos)
			{
				test = (GameObject)Instantiate(Resources.Load(endLeft));
				if (adjusted)
					new_finalRotation = Quaternion.Euler(new Vector3(0f, 180.0f, 360.0f - tempZ));
				//new_finalRotation = new Quaternion(0, 180, finalRotation.z, 0);
				//print ("last" + finalRotation.z);
			}
			//middle node
			else
			{
				test = (GameObject)Instantiate(Resources.Load(tileMid));
			}
			
			
			test.transform.rotation = new_finalRotation;
			
			
			float angleInDeg = Mathf.Atan2 (move_direction.y, move_direction.x) * 180 / Mathf.PI;
			float angleInRad = Mathf.Deg2Rad*angleInDeg;
			//	print ("angle = " + angleInDeg);
			float rx2 = firstPos.x + Mathf.Cos(angleInRad) * i;
			float ry2 = firstPos.y + Mathf.Sin(angleInRad) * i;
			Vector3 drawPoint = new Vector3(rx2, ry2, 0);
			
			test.transform.position = drawPoint;
			//test.collider2D.enabled = false;
		}
		if (distanceBetweenPos != 0) 
			shape.collider2D.enabled = true;
		shape.renderer.enabled = false;
		
		
		
		/*

//Calculate point at end of raycast so we can draw the line
		angleInDeg = Mathf.Atan2 (move_direction.y, move_direction.x) * 180 / Mathf.PI;
		float angleInRad = Mathf.Deg2Rad*angleInDeg;
	//	print ("angle = " + angleInDeg);
		float rx2 = rightOrigin.x + Mathf.Cos(angleInRad) * 1.0f;
		float ry2 = rightOrigin.y + Mathf.Sin(angleInRad) * 1.0f;
		rayPoint = new Vector3(rx2, ry2, 0);

*/}
	void EditShape()
	{
		if (lastPosExists)
		{	
			//float lengthNow = shape.transform.localScale.x;
			//float desiredLength = lengthNow + distanceBetweenPos;
			Vector3 zAxis = new Vector3(0,0,1);
			float xDist = newPos.x - firstPos.x;
			float yDist = newPos.y - firstPos.y;
			distanceBetweenPos = Mathf.Sqrt((xDist*xDist) + (yDist*yDist));
			//print ("dist = " + distanceBetweenPos);
			shape.transform.localScale = new Vector3(distanceBetweenPos, 1, 1);
			
			shape.transform.position = new Vector3((newPos.x + firstPos.x)/2, (newPos.y+firstPos.y)/2, 0);
			
			Vector2 mousePosition = new Vector2(newPos.x, newPos.y);
			Vector2 origPosition = new Vector2(firstPos.x, firstPos.y);
			Vector2 dPos = origPosition - mousePosition; //_arrow.Position - mousePosition;
			float angle = Mathf.Atan2 (dPos.y, dPos.x);
			float new_value = angle * Mathf.Rad2Deg;
			
			Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, new_value));
			//shape.transform.rotation = rotation2;
			//shape.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,new_value)); //_arrow.Rotation = (float)Math.Atan2(dPos.Y, dPos.X);
			
			//float angle = Mathf.Atan2(diff.y, diff.x);
			//transform.rotation = Quaternion.Euler(0f, 0f, RadToDeg(angle));
			
			shape.transform.rotation = rotation2;
			finalRotation = rotation2;
			/*
			float deltaY = firstPos.y - newPos.y; //P2_y - P1_y;
			float deltaX = firstPos.x - newPos.x;//P2_x - P1_x;
			float angleInDegrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;

			shape.transform.RotateAround(firstPos, zAxis, angleInDegrees);

*/
			
			//shape.transform.localScale.x = (float)( desiredLength / lengthNow );
			//shape.transform.localPosition.x += (float)(0.5 * desiredLength);
			
			//shape.transform.localScale = new Vector3(( desiredLength / lengthNow ), shape.transform.localScale.y, shape.transform.localScale.z);
			//shape.transform.localPosition = new Vector3( (shape.transform.localPosition.x + (0.5 * desiredLength)), shape.transform.localPosition.y, shape.transform.localPosition.z);
			/*
			Vector3 scale = shape.transform.localScale;
			scale.x = desiredLength/lengthNow; // your new value
			shape.transform.localScale = scale;

			Vector3 locPos = shape.transform.localPosition;
			locPos.x += (0.5f * desiredLength); // your new value
			shape.transform.localPosition = locPos;*/
			
			
			//Orient the shape toward the new position
			//	shape.transform.LookAt(newPos);
			
		}       
		//Reset stuff
		lastPos = newPos; 
		lastPosExists = true;
	}
	
}
