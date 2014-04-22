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
		thePlayerName = "";
		names = new string[] {"", "", "", "", "", "", "", "", "", ""};
		scores = new float[]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};

		/*ParseObject newGameScore = new ParseObject("PlayerScores");
		newGameScore["score0"] = 1.0f;
		newGameScore["score1"] = 1.0f;
		newGameScore["score2"] = 1.0f;
		newGameScore["score3"] = 1.0f;
		newGameScore["score4"] = 1.0f;
		newGameScore["score5"] = 1.0f;
		newGameScore["score6"] = 1.0f;
		newGameScore["score7"] = 1.0f;
		newGameScore["score8"] = 1.0f;
		newGameScore["score9"] = 1.0f;

		newGameScore["playerName0"] = "AAA";
		newGameScore["playerName1"] = "AAA";
		newGameScore["playerName2"] = "AAA";
		newGameScore["playerName3"] = "AAA";
		newGameScore["playerName4"] = "AAA";
		newGameScore["playerName5"] = "AAA";
		newGameScore["playerName6"] = "AAA";
		newGameScore["playerName7"] = "AAA";
		newGameScore["playerName8"] = "AAA";
		newGameScore["playerName9"] = "AAA";
		newGameScore.SaveAsync();*/
		//resetLeaderboards ();

		objectId = "lYFGz0XjTt";

		//namesScores = null;
		ParseQuery<ParseObject> query = ParseObject.GetQuery("PlayerScores");
		query.GetAsync("lYFGz0XjTt").ContinueWith(t =>
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
		query.GetAsync("lYFGz0XjTt").ContinueWith(t =>
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

		Vector2 resolution = new Vector2(Screen.width, Screen.height);
		float resx = resolution.x/265.0f; // 1280 is the x value of the working resolution
		float resy = resolution.y/398.0f; // 800 is the y value of the working resolution
		//Font size for long thin box
		customSkin.customStyles[1].fontSize = (int)(7.0f*resx);
		//Font size for "Global Leaderboard" and "Local Leaderboard" text
		customSkin.customStyles[2].fontSize = (int)(10.0f*resx);
		customSkin.customStyles[2].padding.top = (int)(10.0f*resx);
		//Font size and padding for Leaderboard Values
		customSkin.customStyles[4].fontSize = (int)(13.0f*resx);
		customSkin.customStyles[4].padding.top = (int)(25.0f*resx);
		customSkin.customStyles[4].padding.left = (int)(25.0*resx);
		customSkin.customStyles[4].padding.right = (int)(25.0*resx);
		//Padding for Enter Name: box
		customSkin.customStyles[6].fontSize = (int)(10.0f*resx);
		customSkin.customStyles[6].padding.top = (int)(9.0f*resx);
		customSkin.customStyles[6].padding.left = (int)(8.0f*resx);

		customSkin.customStyles[7].fontSize = (int)(10.0f*resx);
		customSkin.customStyles[7].padding.top = (int)(8.5f*resx);
		customSkin.customStyles[7].padding.left = (int)(7.0f*resx);

		//Congrats Font Size
		customSkin.customStyles[8].fontSize = (int)(17.0f*resx);



		if(phase == 1){		//New High Score

			GUIStyle customBox = new GUIStyle("box");
			customBox.fontSize = 30;

			string congrats = "CONGRATULATIONS!";
			GUI.Box (new Rect ((Screen.width-(861.0f/4.0f*resx))/2.0f, 90.0f*resy, 861.0f/4.0f*resx, 111.0f/4.0f*resy), congrats, customSkin.customStyles[8]);
			string theText = "Enter name: ";

			GUI.Box (new Rect ((Screen.width-(861.0f/4.0f*resx))/2.0f, 135.0f*resy, 861.0f/4.0f*resx, 111.0f/4.0f*resy), theText, customSkin.customStyles[6]);

			thePlayerName = GUI.TextArea(new Rect ((Screen.width-(861.0f/4.0f*resx))/2.0f + customSkin.customStyles[6].fontSize*9.5f, 135.0f*resy, 861.0f/4.0f*resx, 111.0f/4.0f*resy), thePlayerName, 17, customSkin.customStyles[7]);

			if(GUI.Button (new Rect((Screen.width-(861.0f/4.0f*resx))/2.0f, 175.0f*resy, 861.0f/4.0f*resx, 111.0f/4.0f*resy), "", customSkin.customStyles[5])
			   && !thePlayerName.ToLower().Contains ("fuck") && !thePlayerName.ToLower().Contains ("shit")
			   && !thePlayerName.ToLower().Contains ("Bitch") && !thePlayerName.ToLower().Contains ("cunt")
			   && !thePlayerName.ToLower().Contains ("nigga") && !thePlayerName.ToLower().Contains ("nigger")){
				createNewLeaderboard (thePlayerName);
			}
			//print (thePlayerName);
		}else if(phase == 2){	//No New High Score

			GUIStyle customButton = new GUIStyle("button");
			customButton.fontSize = 30;

			Color superTransColor = Color.white;
			//superTransColor.a = 0.0f;
			//GUI.color = superTransColor;
			string newestText = "";
			
			GUIStyle customBox = new GUIStyle("box");
			customBox.fontSize = 30;
			
			//Restart button (PLAY AGAIN)
			//GUI.color = Color.green;
			string firstText = "";

			if(GUI.Button (new Rect (((Screen.width-(861.0f/8.0f*resx))/2.0f) - 65*resx, 50.0f*resy, 861.0f/8.0f*resx, 345.0f/8.0f*resy), firstText, customSkin.customStyles[0])){
				Camera.main.GetComponent<GameController>().ResetGame();

			}
			if(GUI.Button (new Rect (((Screen.width-(861.0f/8.0f*resx))/2.0f) + 65*resx, 50.0f*resy, 861.0f/8.0f*resx, 345.0f/8.0f*resy), firstText, customSkin.customStyles[9])){
				Camera.main.GetComponent<GameController>().PickNewBall();
			}

			//Screenshot button

			if(GUI.Button (new Rect ((Screen.width-(861.0f/5.0f*resx))/2.0f, 85.0f*resy + 345.0f/8.0f*resy + 582.0f/3.5f*resy, 861.0f/5.0f*resx, 204.0f/5.0f*resy), firstText, customSkin.customStyles[3])){
				print ("Taking screenshot.. JK");
				Application.CaptureScreenshot("ss.png");
			}

			if(score > PlayerPrefs.GetFloat("HighScore10") && !changedLocal){	//Local Leaderboard Stuff
				setLocalLeaderboard();
			}else{	//Back to Normal leaderboard stuff
				if(isGlobal == true){
					newestText = "Tap here to see your high scores!";

					if(GUI.Button (new Rect ((Screen.width-(861.0f/4.0f*resx))/2.0f, 95.0f*resy, 861.0f/4.0f*resx, 111.0f/4.0f*resy), newestText, customSkin.customStyles[1])){
						isGlobal = false;
					}

					newestText = "Global Leaderboard";
					//Draw Leaderboard Background and Text
					GUI.Box (new Rect((Screen.width-(861.0f/3.5f*resx))/2.0f, 125.0f*resy, 861.0f/3.5f*resx, 582.0f/3.5f*resy), newestText, customSkin.customStyles[2]);
					//GUI.Box (new Rect (0.0f, 340.0f, Screen.width, 50), newestText, customBox);

					//newestText = lastPlace.ToString();
					//GUI.Box (new Rect (0.0f, 250.0f, Screen.width, 50), newestText, customBox);

					//Draws the high scores to the screen
					float startingHeight = 400;
					for(int i = 0; i < 10; i++){
						if(names[i] == ""){
							//newestText = "Loading...";
							newestText = "";
							if(i == 3){
								newestText = "                    Loading...                    ";
							}
						}else{
							newestText = (i + 1) + ". " + names[i];
						}
						GUI.Box (new Rect ((Screen.width-(861.0f/3.5f*resx))/2.0f, 125.0f*resy+(customSkin.customStyles[4].fontSize*i), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);

						if(scores[i] == 0.0f){
							//newestText = "Loading...";
							newestText = "";
						}else{
							newestText = scores[i].ToString();
						}
						GUI.Box (new Rect(((861.0f/3.5f*resx)/1.8f), 125.0f*resy+(customSkin.customStyles[4].fontSize*i), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					}
				}else{
					newestText = "Tap to see GLOBAL leaderboard!";
					if(GUI.Button (new Rect ((Screen.width-(861.0f/4.0f*resx))/2.0f, 95.0f*resy, 861.0f/4.0f*resx, 111.0f/4.0f*resy), newestText, customSkin.customStyles[1])){
						isGlobal = true;
					}
					


					float theIncrement = 0.0f;
					//Background Box
					newestText = "";
					//Draw Leaderboard Background and Text
					GUI.Box (new Rect((Screen.width-(861.0f/3.5f*resx))/2.0f, 125.0f*resy, 861.0f/3.5f*resx, 582.0f/3.5f*resy), newestText, customSkin.customStyles[2]);

					newestText = "Your High Scores";
					GUI.Box (new Rect((Screen.width-(861.0f/3.5f*resx))/2.0f, 125.0f*resy, 861.0f/3.5f*resx, 582.0f/3.5f*resy), newestText, customSkin.customStyles[2]);

					//Actually print local high scores--------------------------------------------------------------------------------------------------------------------
					newestText = "1st:   " + PlayerPrefs.GetFloat("HighScore").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "2nd:   " + PlayerPrefs.GetFloat("HighScore2").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "3rd:   " + PlayerPrefs.GetFloat("HighScore3").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "4th:   " + PlayerPrefs.GetFloat("HighScore4").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "5th:   " + PlayerPrefs.GetFloat("HighScore5").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "6th:   " + PlayerPrefs.GetFloat("HighScore6").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "7th:   " + PlayerPrefs.GetFloat("HighScore7").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "8th:   " + PlayerPrefs.GetFloat("HighScore8").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "9th:   " + PlayerPrefs.GetFloat("HighScore9").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;

					newestText = "10th:  " + PlayerPrefs.GetFloat("HighScore10").ToString ("F2");
					GUI.Box (new Rect ((861.0f/3.5f*resx)/3.5f, 125.0f*resy+(customSkin.customStyles[4].fontSize*theIncrement), 861.0f/3.5f*resx / 2, 582.0f/3.5f*resy), newestText, customSkin.customStyles[4]);
					theIncrement += 1.0f;
					//End Actually printing local high scores--------------------------------------------------------------------------------------------------------------------
				}
			}
		}
	}
}
