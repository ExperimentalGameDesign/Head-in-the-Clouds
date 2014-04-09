using UnityEngine;
using System.Collections;
using Parse;

public class LeaderboardScript : MonoBehaviour {
	public GUISkin customSkin;
	public string objectId;
	public float score;
	public float lastPlace;
	public string[] names;
	public float[] scores;
	bool changedHighScores;
	int phase;
	public string thePlayerName;
	public ParseObject namesScores;
	public float timePassed;
	public bool reDo;
	public bool changedLocal;
	public bool isGlobal;

	// Use this for initialization
	void Start () {
		isGlobal = true;
		timePassed = 0.0f;
		reDo = false;

		lastPlace = 0.0f;
		//print (score);
		thePlayerName = "Your Name Here";
		names = new string[] {"", "", "", "", "", "", "", "", "", ""};
		scores = new float[]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};

		/*ParseObject gameScore = new ParseObject("PlayerScores");
		gameScore["score0"] = 1.0f;
		gameScore["score1"] = 1.0f;
		gameScore["score2"] = 1.0f;
		gameScore["score3"] = 1.0f;
		gameScore["score4"] = 1.0f;
		gameScore["score5"] = 1.0f;
		gameScore["score6"] = 1.0f;
		gameScore["score7"] = 1.0f;
		gameScore["score8"] = 1.0f;
		gameScore["score9"] = 1.0f;

		gameScore["playerName0"] = "AAA";
		gameScore["playerName1"] = "AAA";
		gameScore["playerName2"] = "AAA";
		gameScore["playerName3"] = "AAA";
		gameScore["playerName4"] = "AAA";
		gameScore["playerName5"] = "AAA";
		gameScore["playerName6"] = "AAA";
		gameScore["playerName7"] = "AAA";
		gameScore["playerName8"] = "AAA";
		gameScore["playerName9"] = "AAA";
		gameScore.SaveAsync();*/
		//resetLeaderboards ();

		objectId = "poCITRovy1";

		//namesScores = null;
		ParseQuery<ParseObject> query = ParseObject.GetQuery("PlayerScores");
		query.GetAsync("poCITRovy1").ContinueWith(t =>
		                                          {
			namesScores = t.Result;
			//Changed Code

			//Get the names of the top 10 players
			names[0] = namesScores.Get<string>("playerName0");
			names[1] = namesScores.Get<string>("playerName1");
			names[2] = namesScores.Get<string>("playerName2");
			names[3] = namesScores.Get<string>("playerName3");
			names[4] = namesScores.Get<string>("playerName4");
			names[5] = namesScores.Get<string>("playerName5");
			names[6] = namesScores.Get<string>("playerName6");
			names[7] = namesScores.Get<string>("playerName7");
			names[8] = namesScores.Get<string>("playerName8");
			names[9] = namesScores.Get<string>("playerName9");
			//Get the scores of the top 10 players
			scores[0] = namesScores.Get<float>("score0");
			scores[1] = namesScores.Get<float>("score1");
			scores[2] = namesScores.Get<float>("score2");
			scores[3] = namesScores.Get<float>("score3");
			scores[4] = namesScores.Get<float>("score4");
			scores[5] = namesScores.Get<float>("score5");
			scores[6] = namesScores.Get<float>("score6");
			scores[7] = namesScores.Get<float>("score7");
			scores[8] = namesScores.Get<float>("score8");
			scores[9] = namesScores.Get<float>("score9");

			lastPlace = namesScores.Get<float> ("score9");
		});

		changedHighScores = false;
		phase = 0;
		//print (tempFloat);
	}
	
	// Update is called once per frame
	void Update () {

		/*timePassed += Time.deltaTime;
		if(timePassed > 3.0f && !reDo){
			resetLeaderboards();
			reDo = true;
		}*/

		//print (names[0]);
		if(!changedHighScores && score > lastPlace && lastPlace != 0.0f){
			phase = 1;	//put a new score in the leaderboard
		}else{
			phase = 2;	//player didn't score high enough for leaderboard
		}
	}
	
	//Function creates a new leaderboard
	void createNewLeaderboard(string playerName){
		string curName = playerName;
		float curScore = score;

		for(int i = 0; i < 10; i++){
			if(curScore > scores[i]){
				float tempScore = scores[i];
				string tempName = names[i];

				scores[i] = curScore;
				names[i] = curName;

				curScore = tempScore;
				curName = tempName;
			}
		}

		writeNewHighScores ();				//	THIS WILL BE BACK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//print(playerName);
		changedHighScores = true;
		phase = 2;
		return;
	}

	void resetLeaderboards(){
		ParseQuery<ParseObject> query = ParseObject.GetQuery("PlayerScores");
		query.GetAsync("poCITRovy1").ContinueWith(t =>
		                                          {
			namesScores = t.Result;
		//namesScores.SaveAsync().ContinueWith(t =>
		  //                                   {
			namesScores["playerName0"] = "AAA";
			namesScores["playerName1"] = "AAA";
			namesScores["playerName2"] = "AAA";
			namesScores["playerName3"] = "AAA";
			namesScores["playerName4"] = "AAA";
			namesScores["playerName5"] = "AAA";
			namesScores["playerName6"] = "AAA";
			namesScores["playerName7"] = "AAA";
			namesScores["playerName8"] = "AAA";
			namesScores["playerName9"] = "AAA";
			
			namesScores["score0"] = 1.0f;
			namesScores["score1"] = 1.0f;
			namesScores["score2"] = 1.0f;
			namesScores["score3"] = 1.0f;
			namesScores["score4"] = 1.0f;
			namesScores["score5"] = 1.0f;
			namesScores["score6"] = 1.0f;
			namesScores["score7"] = 1.0f;
			namesScores["score8"] = 1.0f;
			namesScores["score9"] = 1.0f;
			//print ("hello there");
			
			namesScores.SaveAsync();
		});
		return;
	}

	void writeNewHighScores(){
		namesScores.SaveAsync().ContinueWith(t =>
		                                   {
			namesScores["playerName0"] = names[0];
			namesScores["playerName1"] = names[1];
			namesScores["playerName2"] = names[2];
			namesScores["playerName3"] = names[3];
			namesScores["playerName4"] = names[4];
			namesScores["playerName5"] = names[5];
			namesScores["playerName6"] = names[6];
			namesScores["playerName7"] = names[7];
			namesScores["playerName8"] = names[8];
			namesScores["playerName9"] = names[9];

			namesScores["score0"] = scores[0];
			namesScores["score1"] = scores[1];
			namesScores["score2"] = scores[2];
			namesScores["score3"] = scores[3];
			namesScores["score4"] = scores[4];
			namesScores["score5"] = scores[5];
			namesScores["score6"] = scores[6];
			namesScores["score7"] = scores[7];
			namesScores["score8"] = scores[8];
			namesScores["score9"] = scores[9];
			//print ("hello there");

			namesScores.SaveAsync();
		});
		return;
	}

	void setLocalLeaderboard(){
		float curScore = score;
		if(curScore > PlayerPrefs.GetFloat ("HighScore")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore");
			PlayerPrefs.SetFloat ("HighScore", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore2")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore2");
			PlayerPrefs.SetFloat ("HighScore2", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore3")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore3");
			PlayerPrefs.SetFloat ("HighScore3", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore4")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore4");
			PlayerPrefs.SetFloat ("HighScore4", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore5")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore5");
			PlayerPrefs.SetFloat ("HighScore5", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore6")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore6");
			PlayerPrefs.SetFloat ("HighScore6", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore7")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore7");
			PlayerPrefs.SetFloat ("HighScore7", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore8")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore8");
			PlayerPrefs.SetFloat ("HighScore8", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore9")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore9");
			PlayerPrefs.SetFloat ("HighScore9", curScore);
			curScore = tempFloat;
		}
		if(curScore > PlayerPrefs.GetFloat ("HighScore10")){
			float tempFloat = PlayerPrefs.GetFloat ("HighScore10");
			PlayerPrefs.SetFloat ("HighScore10", curScore);
			curScore = tempFloat;
		}


		changedLocal = true;
		return;
	}

	void OnGUI(){
		GUI.skin = customSkin;
		if(phase == 1){		//New High Score
			GUIStyle customBox = new GUIStyle("box");
			customBox.fontSize = 30;
			
			GUI.color = Color.cyan;
			string theText = "Enter name for worldwide leaderboard!!!!!";
			GUI.Box (new Rect (0.0f, 10.0f, Screen.width, 50), theText, customBox);
			GUI.color = Color.green;

			thePlayerName = GUI.TextArea(new Rect (0.0f, 190.0f, Screen.width, 50), thePlayerName, 20, customBox);

			if(GUI.Button (new Rect(0.0f, Screen.height /2, Screen.width, 50), "Touch here when done", customBox)){
				createNewLeaderboard (thePlayerName);
			}
			GUI.color = Color.cyan;
			//print (thePlayerName);
		}else if(phase == 2){	//No New High Score

			GUIStyle customButton = new GUIStyle("button");
			customButton.fontSize = 30;
			Color superTransColor = Color.white;
			superTransColor.a = 0.0f;
			GUI.color = superTransColor;
			string newestText = "";
			
			GUIStyle customBox = new GUIStyle("box");
			customBox.fontSize = 30;
			
			//Restart button
			GUI.color = Color.green;
			string firstText = "Tap Here to Play Again!";
			if(GUI.Button (new Rect (0.0f, 10.0f, Screen.width, 200), firstText, customButton)){
				Application.LoadLevel (0);
			}
			GUI.color = Color.cyan;

			if(score > PlayerPrefs.GetFloat("HighScore10") && !changedLocal){	//Local Leaderboard Stuff
				GUI.color = Color.cyan;
				setLocalLeaderboard();
				GUI.color = Color.cyan;
			}else{	//Back to Normal leaderboard stuff
				GUI.color = Color.green;
				if(isGlobal == true){
					newestText = "Tap here to see your high scores!";
					if(GUI.Button (new Rect (0.0f, 280.0f, Screen.width, 50), newestText, customButton)){
						isGlobal = false;
					}

					GUI.color = Color.cyan;
					newestText = "Global Leaderboard";
					GUI.Box (new Rect (0.0f, 340.0f, Screen.width, 50), newestText, customBox);

					//newestText = lastPlace.ToString();
					//GUI.Box (new Rect (0.0f, 250.0f, Screen.width, 50), newestText, customBox);

					//Draws the high scores to the screen
					float startingHeight = 400;
					for(int i = 0; i < 10; i++){
						if(names[i] == ""){
							newestText = "Loading...";
						}else{
							newestText = (i + 1) + ". " + names[i];
						}
						GUI.Box (new Rect (0.0f, 400.0f + (60.0f * i), Screen.width / 2, 50.0f), newestText, customBox);

						if(scores[i] == 0.0f){
							newestText = "Loading...";
						}else{
							newestText = scores[i].ToString();
						}
						GUI.Box (new Rect(Screen.width / 2, 400.0f + (60.0f * i), Screen.width / 2, 50.0f), newestText, customBox);
					}
				}else{
					newestText = "Tap here to see GLOBAL leaderboard!";
					if(GUI.Button (new Rect (0.0f, 280.0f, Screen.width, 50), newestText, customButton)){
						isGlobal = true;
					}
					
					GUI.color = Color.cyan;
					newestText = "Your High Scores";
					GUI.Box (new Rect (0.0f, 340.0f, Screen.width, 50), newestText, customBox);


					float theIncrement = 400.0f;

					//Actually print local high scores--------------------------------------------------------------------------------------------------------------------
					newestText = "1st: " + PlayerPrefs.GetFloat("HighScore").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "2nd: " + PlayerPrefs.GetFloat("HighScore2").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "3rd: " + PlayerPrefs.GetFloat("HighScore3").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "4th: " + PlayerPrefs.GetFloat("HighScore4").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "5th: " + PlayerPrefs.GetFloat("HighScore5").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "6th: " + PlayerPrefs.GetFloat("HighScore6").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "7th: " + PlayerPrefs.GetFloat("HighScore7").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "8th: " + PlayerPrefs.GetFloat("HighScore8").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "9th: " + PlayerPrefs.GetFloat("HighScore9").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;

					newestText = "10th: " + PlayerPrefs.GetFloat("HighScore10").ToString ("F2");
					GUI.Box (new Rect(0.0f, theIncrement, Screen.width, 50.0f), newestText, customBox);
					theIncrement += 60.0f;
					//End Actually printing local high scores--------------------------------------------------------------------------------------------------------------------
				}
			}
		}
	}
}
