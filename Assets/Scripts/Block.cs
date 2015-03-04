using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using BlockMethods;

public class Block : MonoBehaviour {
	#region Mouse Variables
	Vector3 dist;
	float posX;
	float posY;
	bool isMoving;
	bool canCollide;
	int DEFAULT_LAYER = 0;
	int VOID_LAYER = 8;
	#endregion

	#region Block Variables
	public Vector2 relativeCenter = new Vector3(0,0,0);
	public Vector2 spriteDim = new Vector2(1,1);
	public Block root = null;
	public Block parent = null;	
	public List<string> children = new List<string>();
	#endregion

	void Start(){

		Physics2D.IgnoreLayerCollision (DEFAULT_LAYER, VOID_LAYER);

	}

	void Update(){

		UpdateBlockCenters (this);
	}
	
	void UpdateBlockCenters(Block curr)
	{
		BlockMethods.Center.UpdateRootCenter (curr);

		BlockMethods.Center.UpdateChildrenCenters (curr);
	}

	void setRoot(Block parent, Block child){
		
		if(parent.root == null) { 
			parent.root = parent;
			child.root = parent;
		}
		
		else
			child.root = parent.root;
	}
	
	void addChild(Block currParent, Block currChild, string child){
		
		currParent.children.Add (child);
		currChild.parent = currParent;
	}

	void OnMouseDown(){
		isMoving = true;

		Destroy (transform.rigidbody2D);
		transform.gameObject.layer = VOID_LAYER;

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
		canCollide = true;

		Rigidbody2D newRigidBody = transform.gameObject.AddComponent<Rigidbody2D> ();
		newRigidBody.mass = 1;

		transform.gameObject.layer = DEFAULT_LAYER;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Block parentBlock = collision.gameObject.GetComponent<Block>();
		Block childBlock = this;
		string childName = this.name;

		if (childBlock.parent == parentBlock)
			return;

		if (collision.gameObject.tag == "Block" & !parentBlock.isMoving & childBlock.canCollide)
		{
			setRoot (parentBlock,childBlock);
			addChild(parentBlock,childBlock, childName);
			//Scale.Resize(parentBlock);
			Physics2D.IgnoreCollision(parentBlock.collider2D, childBlock.collider2D);
			
			Debug.Log (childBlock.name + ": " + childBlock.parent);
			Debug.Log (parentBlock.name + ": " + parentBlock.children.Count);
		}
	}
}