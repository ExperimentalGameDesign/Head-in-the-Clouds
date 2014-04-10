using UnityEngine;
using System.Collections;
using System.IO;

public class cameraScript : MonoBehaviour {

	public WebCamTexture cameraFront, cameraBack;
	public WebCamTexture finalCamTexture;
	public GUITexture BackgroundTexture;
	public string frontCamName, backCamName;
	public GameObject picturePlane;
	public bool isFront;
	public Vector3 frontRotation, backRotation, frontScale, backScale;
	public Texture finalPicture;

	// Use this for initialization
	void Start () {
		isFront = true;

		//picturePlane = (GameObject)Instantiate(Resources.Load("Thread"), new Vector3 (0.0f, 0.0f, -1.0f), new Quaternion( 0.0f, -180.0f, 0.0f, 0.0f));
		picturePlane.transform.position = new Vector3 (0.0f, 0.0f, -1.0f);
		picturePlane.transform.rotation = new Quaternion(180.0f, -180.0f, 0.0f, 0.0f);
		picturePlane.transform.localScale = picturePlane.transform.localScale * 5;
		//picturePlane.transform.eulerAngles = frontRotation;

		BackgroundTexture = gameObject.AddComponent<GUITexture>();
		BackgroundTexture.pixelInset = new Rect(0 ,0,Screen.width,Screen.height);

		WebCamDevice[] devices = WebCamTexture.devices;
		string frontCamName = "";
		string backCamName = "";
		cameraFront = new WebCamTexture();
		cameraBack = new WebCamTexture ();
		for(int i = 0; i < devices.Length; i++) {		//Finds front facing camera, if there is one
			//Debug.Log("Device:"+devices[i].name+ "IS FRONT FACING:"+devices[i].isFrontFacing);
			
			if (devices[i].isFrontFacing) {
				frontCamName = devices[i].name;
			}else{
				backCamName = devices[i].name;
			}
		}
		cameraFront = new WebCamTexture (frontCamName);//,10000,10000,1);
		cameraFront.requestedHeight = 10000;
		cameraFront.requestedWidth = 50;
		cameraFront.Play();
		picturePlane.renderer.material.mainTexture = cameraFront;

		cameraBack = new WebCamTexture (backCamName);//,10000,10000,1);
		cameraBack.requestedHeight = 10000;
		cameraBack.requestedWidth = 50;
		//BackgroundTexture.texture = camera;
	}
	
	// Update is called once per frame
	void Update () {

	}
	/*
	void TakeSnapshot()
	{
		Texture2D snap = new Texture2D(wct.width,wct.height);
		snap.SetPixels(wct.GetPixels());
		snap.Apply();
		byte[] bytes = snap.EncodeToPNG();
		string filename = fileName(Convert.ToInt32(snap.width),Convert.ToInt32(snap.height));
		path = Application.persistentDataPath + "/Snapshots/" + filename;
		System.IO.File.WriteAllBytes(path,bytes);
	}*/
	
	void SaveTextureToFile( Texture2D text, string fileName)
	{
		byte[] bytes = text.EncodeToPNG();
		//File file2 = new File();
		FileStream file2 = File.Open(Application.dataPath + "/Resources/"+fileName,FileMode.Create);
		BinaryWriter binary= new BinaryWriter(file2);
		binary.Write(bytes);
		file2.Close();
	}

	void OnGUI(){
		//Stops the picture
		Texture2D CamIcon = (Texture2D)(Resources.Load ("CameraIconFORASHLEY"));
		//GUI.backgroundColor = Color.clear;
		if(GUI.Button(new Rect(0.0f, (Screen.height / 4.0f) * 3.0f, Screen.width, Screen.height / 4), CamIcon)){
			if (isFront)
				finalCamTexture = cameraFront;
			else
				finalCamTexture = cameraBack;

			picturePlane.renderer.material.mainTexture = finalCamTexture;
			//picturePlane.renderer.material.SetFloat("_Cutoff", 1.0f);
			// Ashley messing with camera stuff
			//finalPicture = picturePlane.renderer.material.mainTexture;
			Texture2D text = new Texture2D(finalCamTexture.width, finalCamTexture.height, TextureFormat.ARGB32, false); //finalCamTexture.width, finalCamTexture.height, TextureFormat.ARGB32, false);
			Color[] textureData = finalCamTexture.GetPixels();
			text.SetPixels(textureData);
			text.Apply();
			//START CIRCLE STUFF
			/*
			Color[] colors = new Color[3];
			colors[0] = new Color(0.0f,0.0f,0.0f,0.1f);
			colors[1] = Color.green;
			colors[2] = Color.blue;
			float mipCount = Mathf.Min( 3, text.mipmapCount );
			// tint each mip level
			for( int mip = 0; mip < mipCount; ++mip ) {
				Color[] cols = text.GetPixels( mip );
				for( int i = 0; i < cols.Length; ++i ) {
					cols[i] = colors[0];
				}
				text.SetPixels( cols, mip );
			}
			// actually apply all SetPixels, don't recalculate mip levels
			text.Apply( false );*/
			//END OF CIRCLE STUFF
			//Texture text = picturePlane.renderer.material.mainTexture;
			string fileName = "testIMAGE.png";
			SaveTextureToFile(text, fileName);
			//Ashley done messing with camera stuff
			cameraFront.Stop ();
			cameraBack.Stop ();
			GetComponent<GameController>().playerPicked = true;
			GetComponent<GameController>().facePicked = true;


			enabled = false;
		}

		//Switches camera
		Texture2D SwitchIcon = (Texture2D)(Resources.Load ("SwitchCameraIcon"));
		if(GUI.Button (new Rect(0.0f, 0.0f, Screen.width, Screen.height / 4), SwitchIcon)){
			if(isFront == true){
				cameraFront.Stop();
				cameraBack.Play ();
				picturePlane.renderer.material.mainTexture = cameraBack;

				//picturePlane.transform.eulerAngles = backRotation;
				//picturePlane.transform.localScale = backScale;
				isFront = false;
			}else{
				cameraBack.Stop ();
				cameraFront.Play ();
				picturePlane.renderer.material.mainTexture = cameraFront;

				//picturePlane.transform.eulerAngles = frontRotation;
				//picturePlane.transform.localScale = frontScale;
				isFront = true;
			}
		}
	}
	
}
