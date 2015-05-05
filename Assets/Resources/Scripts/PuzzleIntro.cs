using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using System.Text.RegularExpressions;

public class PuzzleIntro : MonoBehaviour {
	
	public Texture puzzleImage = null;
	public int puzzleNumber = 0;
	public string solution = "";
	
	void Start(){
		string levelNum;
		string puzzleNum = EditorApplication.currentScene;
		if (puzzleNum == "Assets/StageSelect.unity")
			levelNum = "1";
		else
			levelNum = Regex.Replace (puzzleNum, "[^0-9]", "");
		puzzleNumber = int.Parse (levelNum);
		Load ("Assets/Resources/puzzles.txt", puzzleNumber);
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel("Level" + puzzleNumber.ToString());
		}
	}
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(10,10,600,600), this.puzzleImage);
	}

	public void Load(string fileName, int levelNum)
	{
		levelNum--;
		List<string> entries = new List<string>();
		string line;
		StreamReader reader = new StreamReader (fileName,Encoding.Default);
		using (reader)
		{
			do
			{
				line = reader.ReadLine ();
				if(line != null)
				{
					string[] add = line.Split (',');
					entries.Add(add[0]);
					entries.Add (add[1]);
					entries.Add (add[2]);
				}
			}
			
			while(line!= null);
			reader.Close ();
		}

		puzzleImage = (Texture) Resources.Load ("Images/"+entries[1 + 3*(puzzleNumber - 1)]);
		solution = entries[2 + 3*(puzzleNumber - 1)];
	}

}
