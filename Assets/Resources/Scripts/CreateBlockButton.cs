using UnityEngine;
using System.Collections;

public class CreateBlockButton : MonoBehaviour {

	public bool guiDisplay = false;
	Sprite printSprite = null;
	Sprite loopSprite = null;
	Sprite whileSprite = null;
	Sprite doSprite = null;
	int printID = 0;
	int loopID = 0;
	int whileID = 0;
	int doID = 0;

	// Use this for initialization
	void Start () {
		printSprite = (Sprite) Resources.Load ("Blocks/printBlock", typeof(Sprite));
		doSprite = (Sprite) Resources.Load ("Blocks/doBlock", typeof(Sprite));
		loopSprite = (Sprite)Resources.Load ("Blocks/loopBlock", typeof(Sprite));
		whileSprite = (Sprite)Resources.Load ("Blocks/loopBlock", typeof(Sprite));
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public void CreateWhileBlock(){
		GameObject whileLoop = new GameObject ();
		whileLoop.AddComponent<SpriteRenderer> ().sprite = whileSprite;
		whileLoop.AddComponent<BoxCollider> ().size = new Vector3((float)1, (float)1, (float) 0.2);
		whileLoop.AddComponent<Rigidbody> ();
		whileLoop.AddComponent<whileBlock> ();
		whileLoop.tag = "Block";
		whileLoop.name = "WhileBlockNew" + whileID;
		whileID++;
	}

	public void CreateDoBlock(){
		GameObject doObj = new GameObject ();
		doObj.AddComponent<SpriteRenderer> ().sprite = doSprite;
		doObj.AddComponent<BoxCollider> ().size = new Vector3((float)1, (float)1, (float) 0.2);
		doObj.AddComponent<Rigidbody> ();
		doObj.AddComponent<doBlock> ();
		doObj.tag = "Block";
		doObj.name = "DoBlockNew" + doID;
		doID++;
	}

	public void ResetPuzzle()
	{
		DeleteAllBlocks ();
		SetWrapperBlock ();
	}

	private void SetWrapperBlock(){
		Block wrapper = GameObject.Find ("WrapperBlock").GetComponent<Block> ();
		wrapper.transform.position = new Vector3 (0, 2, 0);
		wrapper.transform.localScale = new Vector3 (1, 1, 1);
	}

	private void DeleteAllBlocks(){
		GameObject [] blocks = GameObject.FindGameObjectsWithTag ("Block");
		for(int i = 0; i < blocks.Length; i++){
			Block curr = null;

			if(blocks[i] != null & blocks[i].name == "WrapperBlock")
			{
				curr  = blocks[i].GetComponent<Block>();
				curr.children.Clear();
			}

			else if(blocks[i] != null)
			{
				curr  = blocks[i].GetComponent<Block>();
				if(curr != null){
					DestroyImmediate (curr);
					DestroyImmediate (blocks[i]);
				}
			}

			else
			{
				continue;
			}
		}


	}
	public void DeleteBlock(){
		GameObject [] blocks = GameObject.FindGameObjectsWithTag ("Block");
		for(int i = 0; i < blocks.Length; i++){
			Block curr = null;
			if(blocks[i] != null)
				curr  = blocks[i].GetComponent<Block>();
			else
				continue;

			if(curr.isSelectedBlock & curr.Type != "wrapper" && curr != null){
				DeleteBlockHelper(curr, blocks[i]);
			}
		}
	}

	public void DeleteBlockHelper(Block currBlock, GameObject currObj){

		Block currParent = currBlock.parent;
		currParent.delete = true;
		if (currParent.parent != null)
			currParent.parent.delete = true;
		BlockMethods.BlockCollision.removeChild (currParent, currBlock, currBlock.name);

		if(currBlock.children.Count > 0)
		{
			foreach(string child in currBlock.children)
			{
				Block currChild = GameObject.Find (child).GetComponent<Block>();
				//BlockMethods.BlockCollision.removeChild(currBlock, currChild, child);
				GameObject childObj = GameObject.Find (child);
				DestroyImmediate (currChild);
				DestroyImmediate (childObj);
			}
		}

		Destroy (currBlock);
		Destroy(currObj);

		Vector3 newScale = new Vector3 (1.0f, 1.0f, currParent.transform.localScale.z);
		currParent.originalScale = newScale;
		currParent.smallScale = newScale / 2;
		currParent.spriteDim = currParent.originalScale;
		currParent.transform.localScale = newScale;
		BlockMethods.Scale.ResizeParents(currParent);

		currParent.delete = false;
		if (currParent.parent != null)
			currParent.parent.delete = false;
	}

	public void ShowPuzzleScreen(){
		guiDisplay = true;
	}

	public void ClearPuzzleScreen(){
		guiDisplay = false;
	}
}
