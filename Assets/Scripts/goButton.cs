using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class goButton : MonoBehaviour {

	List<Transform> blocks = new List<Transform>();
	Transform parent;

	void Start(){
		parent = GameObject.Find ("Block").transform;
	}

	void Update() {

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
		
		if(hit.collider != null && Input.GetMouseButtonDown(0))
		{
			foreach (Transform child in parent)
				blocks.Add(child);
		}
	}

}
