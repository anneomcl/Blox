using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	Vector3 dist;
	float posX;
	float posY;
	Vector2 relativeCenter = new Vector3(0,0,0);
	Vector2 spriteDim = new Vector2(1,1);
	Block root = null;
	Block parent = null;	
	List<string> children = new List<string>();
	bool isSnapped = false;
	
	bool isMoving;

	void Start(){


	}

	void Update(){

		UpdateBlockCenters (this);

	}

	void UpdateBlockCenters(Block curr)
	{

		UpdateRootCenter (curr);

		UpdateChildrenCenters (curr);
	
	}

	void UpdateChildrenCenters(Block curr){

		if (this.children.Count == 1) {
			UpdateSingleChildCenter (curr);
		}

		else if (this.children.Count > 1) {
			UpdateMultipleChildCenters(curr);
		}

		else {
			return;
		}
	}

	void UpdateMultipleChildCenters(Block curr){

		List<float> offsets = new List<float>();
		CalculateOffsets (curr, offsets);

	}

	void CalculateOffsets(Block curr, List<float> offsetList){
		/*int numChildren = curr.children.Count;
		
		float parentYDim = curr.spriteDim.y;
		
		float parentYCenter = parentYDim / 2;
		
		if (numChildren % 2 == 0) {
			for (int i = 0; i < offsetList.Count/2 - 1; i++)
			{
				float value = parentYDim - (i+1)*(parentYCenter/(numChildren + 1)) - parentYCenter;
				offsetList.Insert(i, value);
			}
			
			for (int i = offsetList.Count/2; i < offsetList.Count - 1; i++)
			{
				float value = (offsetList.Count - i)*(parentYCenter/(numChildren + 1)) - parentYCenter;
				offsetList.Insert (i, value);
				Debug.Log (i + ", " + value);
			}
		}
		
		else
		{
			
		}*/
	}

	void UpdateSingleChildCenter(Block curr){
		Block childBlock = GameObject.Find (this.children[0]).GetComponent<Block> ();
		Vector3 newCenter = new Vector3 (this.relativeCenter.x, 
		                                 this.relativeCenter.y, 
		                                 this.transform.position.z - 1);
		childBlock.relativeCenter = newCenter;
		childBlock.transform.position = newCenter;
	}

	void UpdateRootCenter(Block curr){
		if (curr.root == curr) {
			relativeCenter = curr.transform.GetComponentInChildren<Renderer> ().bounds.center;
		}
	}


	void OnMouseDown(){
		isMoving = true;
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
		
	}
	
	void OnMouseDrag(){
		isMoving = true;
		Vector3 curPos = 
			new Vector3(Input.mousePosition.x - posX, 
			            Input.mousePosition.y - posY, dist.z);  
		
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
		transform.position = worldPos;
	}

	void OnMouseUp (){
		isMoving = false;
	}

	//To-do: make sure Go button doesn't cause collisions
	void OnCollisionEnter2D(Collision2D collision) {
		Block parentBlock = collision.gameObject.GetComponent<Block>();
		Block childBlock = this;
		string childName = this.name;

		if (collision.gameObject.tag == "Block" & !parentBlock.isMoving)
		{
			setRoot (parentBlock,childBlock);

			addChild(parentBlock,childBlock, childName);

			Resize(parentBlock);

			childBlock.isSnapped = true;

			Physics2D.IgnoreCollision (parentBlock.transform.collider2D, childBlock.transform.collider2D);
		}
	}

	void Resize(Block curr) {
		ResizeParents (curr);
		// resize "this", and all of its parents
	}

	void ResizeParents(Block curr){
		float newHeight = GetSumChildrenHeight(curr); //Gets sum of heights from level below
		float newWidth = GetSumChildrenWidth(curr);

		Vector3 newScale = new Vector3 (newWidth, newHeight, 0);
		curr.transform.localScale += newScale;

		curr.spriteDim.y = newHeight - 2;
		curr.spriteDim.x = newWidth - 2; //apply new dimensions to curr
		
		if (curr.root != curr | curr.parent != null) {
			ResizeParents (curr.parent);
		}
	}

	float GetSumChildrenWidth(Block curr){

		//only need width of one child in each layer
		float sum = GameObject.Find (curr.children[0]).GetComponent<Block>().spriteDim.x; 

		
		return sum;
	}

	float GetSumChildrenHeight(Block curr){
		float sum = 0;
		
		//Base: A leaf
		if (curr.children.Count == 0) {
			sum += curr.spriteDim.y;
		} 
		
		else {
			
			foreach (string child in curr.children) {
				sum += GetSumChildrenHeight (GameObject.Find (child).GetComponent<Block> ()); 
			}
		}
		
		return sum;
	}

	void setRoot(Block parent, Block child){
		
		if(parent.root == null) { 
			parent.root = parent; //set roots of both
			child.root = parent;
		}
		
		else
			child.root = parent.root;
	}

	//TO-DO: need to organize children of multiple layers into some sort of hash
	void addChild(Block currParent, Block currChild, string child){

		currParent.children.Add (child);
		currChild.parent = currParent; //set parent
		
		//currParent = currParent.parent;

		//adds child to all parents
		/*while (currParent != null) {
			currParent.children.Add (child);
			currParent = currParent.parent;
		}*/

	}



}