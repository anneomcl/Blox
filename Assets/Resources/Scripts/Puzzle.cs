using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Puzzle : MonoBehaviour {

	public Texture puzzleImage = null;
	public CreateBlockButton trigger;
	public int puzzleNumber = 0;
	public string solution = "";

	string allsolns = "1,Puzzle001,Debug.Log('Hello world');@2,Puzzle002,Debug.Log('Good morning');Do('Eat breakfast');@3,Puzzle003,for(int i = 0; i < 60; i++){Do('Drive');}@4,Puzzle004,Debug.Log('You look great!');for(int i = 0; i < 300; i++){Do('Work');Do('Check e-mail');}@5,Puzzle005,Do('Sit at park bench');for(int i = 0; i < 2; i++){for(int i = 0; i < 10; i++){}Debug.Log('Eureka!');}";

	void Start(){
		
		trigger = GameObject.Find ("CheckPuzzleButton").GetComponent<CreateBlockButton> ();
		string levelNum;
		string puzzleNum = Application.loadedLevelName;
		if (puzzleNum == "StageSelect" | puzzleNum == "Menu")
			levelNum = "1";
		else
			levelNum = Regex.Replace (puzzleNum, "[^0-9]", "");
		puzzleNumber = int.Parse (levelNum);
		//Load ("Assets/Resources/puzzles.txt", puzzleNumber);
		LoadWeb (allsolns);
	}

	void Update(){
		if (Code.ParseBlocks.executionComplete) 
		{
			Application.LoadLevel("Level" + puzzleNumber.ToString() + "Outro");
		}
	}

	public void OnGUI(){
		if(trigger.guiDisplay)
			GUI.DrawTexture(new Rect(0, 0,850,600), this.puzzleImage);
	}

	public void LoadWeb(string s){
		string[] hey = s.Split ("@" [0]);
		int i = 1;
		foreach (string l in hey) {
			if( i == puzzleNumber && l != null)
			{
				string[] add = l.Split (',');
				puzzleImage = (Texture) Resources.Load ("Images/"+add[1]);
				solution = add[2];
				break;
			}
			i++;
		}
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