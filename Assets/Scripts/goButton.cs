using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Code;

public class goButton : MonoBehaviour {

	void Start(){

	}

	void Update() {

	}

	void OnMouseDown(){
		Code.ParseBlocks.ParseCodeBlock ();
	}

}
