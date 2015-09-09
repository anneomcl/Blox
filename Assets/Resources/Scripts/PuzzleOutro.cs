using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class PuzzleOutro : MonoBehaviour {

	int nextPuzzleNumber;
	int levelToLoad = 0;
	bool loadLevel = false;
	public Texture puzzleImage = null;

	// Use this for initialization
	void Start () {
		string currPuzzleNum = Application.loadedLevelName;
		string levelNum = Regex.Replace (currPuzzleNum, "[^0-9]", "");
		nextPuzzleNumber = int.Parse (levelNum) + 1;
		puzzleImage = (Texture) Resources.Load ("Images/Level"+ levelNum + "Outro");

	}
	
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			loadLevel = true;
			LoadNextLevel();
		}
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(0, 0,850,600), this.puzzleImage);
	}

	public void LoadNextLevel()
	{
		if(loadLevel)
		{
			loadLevel = false;
			Code.ParseBlocks.executionComplete = false;
			levelToLoad = 3*nextPuzzleNumber - 1;
			if(levelToLoad < 25)
				Application.LoadLevel(levelToLoad);
			else
				return;
				//You win game!
		}
	}
}
