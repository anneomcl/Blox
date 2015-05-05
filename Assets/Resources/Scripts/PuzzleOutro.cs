using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;

public class PuzzleOutro : MonoBehaviour {

	int nextPuzzleNumber;
	int levelToLoad = 0;
	bool loadLevel = false;

	// Use this for initialization
	void Start () {
		string currPuzzleNum = EditorApplication.currentScene;
		string levelNum = Regex.Replace (currPuzzleNum, "[^0-9]", "");
		nextPuzzleNumber = int.Parse (levelNum) + 1;
	}
	
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			loadLevel = true;
		}
	}

	public void LoadNextLevel()
	{
		if(loadLevel)
		{
			loadLevel = false;
			levelToLoad = 3*nextPuzzleNumber - 1;
			Application.LoadLevel(levelToLoad);
		}
	}
}
