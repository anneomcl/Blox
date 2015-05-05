using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class StageSelectButton : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadMenu(){
		Application.LoadLevel (1);
	}

	public void LoadStage(){
		string name = GetComponent<Button>().name;
		string levelNum = Regex.Replace (name, "[^0-9]", "");
		//string path = "Level" + levelNum.ToString () + "Intro";
		int num = 3 * int.Parse (levelNum) - 1;
		Application.LoadLevel (2);
	}

	public void GoBack(){
		Application.LoadLevel (0);
	}
}
