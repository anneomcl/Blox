using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Code;

public class goButton : MonoBehaviour {

	bool done = true;
	bool guiIsVisible = false;

	void Start(){

	}

	void Update() {
		if(Input.anyKeyDown){
			done = true;
		}
	}

	public void go(){
		done = Code.ParseBlocks.ParseCode ();
	}

	void OnGUI(){
		
		if(!done){
			if(UnityEngine.Event.current.type == EventType.Repaint)
			{
				GUIStyle guiStyle = GameObject.Find ("GUIStyle").GetComponent<GUIStyleCustom>().guiStyle;
				GUIStyle guiStyle2 = new GUIStyle();
				guiStyle2.font = guiStyle.font;
				guiStyle2.fontSize = guiStyle.fontSize*2;
				guiStyle2.fontStyle = guiStyle.fontStyle;
				guiStyle2.normal.textColor = Color.red;
				GUI.TextField(new Rect(200, 200, 500, 100), "Try again!", guiStyle2);
			}
		}
	}

}
