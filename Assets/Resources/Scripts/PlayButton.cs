﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoadStage(){
		Application.LoadLevel (2);
	}
}
