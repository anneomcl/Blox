using UnityEngine;
using System.Collections;

public class CreateBlockButton : MonoBehaviour {

	public bool guiDisplay = false;
	Sprite printSprite = null;
	Sprite loopSprite = null;
	int printID = 0;
	int loopID = 0;

	// Use this for initialization
	void Start () {
		printSprite = (Sprite) Resources.Load ("Blocks/printBlock", typeof(Sprite));
		loopSprite = (Sprite)Resources.Load ("Blocks/loopBlock", typeof(Sprite));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DetachBlock()
	{

	}

	public void CreateBlock(){
		GameObject print = new GameObject ();
		print.AddComponent<SpriteRenderer> ().sprite = printSprite;
		print.AddComponent<BoxCollider> ().size = new Vector3((float)1,(float) 1, (float) 0.2);
		print.AddComponent<Rigidbody> ();
		print.AddComponent<printBlock> ();
		print.tag = "Block";
		print.name = "PrintBlock" + printID;
		printID++;
	}

	public void CreateLoopBlock(){
		GameObject loop = new GameObject ();
		loop.AddComponent<SpriteRenderer> ().sprite = loopSprite;
		loop.AddComponent<BoxCollider> ().size = new Vector3((float)1, (float)1, (float) 0.2);
		loop.AddComponent<Rigidbody> ();
		loop.AddComponent<loopBlock> ();
		loop.tag = "Block";
		loop.name = "LoopBlockNew" + loopID;
		loopID++;
	}

	public void DeleteBlock(){
		GameObject [] blocks = GameObject.FindGameObjectsWithTag ("Block");
		for(int i = 0; i < blocks.Length; i++){
			Block curr = blocks[i].GetComponent<Block>();
			if(curr.isSelectedBlock & curr.Type != "wrapper"){
				Destroy (curr);
				Destroy(blocks[i]);
			}

		}
	}

	public void ShowPuzzleScreen(){
		guiDisplay = true;
	}

	public void ClearPuzzleScreen(){
		guiDisplay = false;
	}
}
