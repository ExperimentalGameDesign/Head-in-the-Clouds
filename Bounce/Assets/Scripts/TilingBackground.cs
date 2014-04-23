using UnityEngine;
using System.Collections;

public class TilingBackground : MonoBehaviour {
	public bool tiled = false;
	public GameObject tiler, player, newTile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D thing) {
		//Debug.Log (thing.name + " ***** " + player.name);
		if ((thing.name == player.name || thing.name == "FaceSprite 1(Clone)") && tiled == false) {

			newTile = (GameObject)Instantiate(Resources.Load("ModularSpriteKing"), new Vector3(0.0f, tiler.transform.position.y + 608, 0.0f), Quaternion.identity);
			newTile.GetComponentInChildren<TilingBackground>().player = player;
			tiler = newTile;
			tiled = true;
		}
	}
}
