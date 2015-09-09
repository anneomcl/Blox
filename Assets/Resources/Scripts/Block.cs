using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using BlockMethods;

public class Block : MonoBehaviour {
	#region Mouse Variables
	public Vector3 dist;
	public float posX;
	public float posY;
	public float posZ = 0;
	public bool isFreeToMove = false;
	public bool isSelectedBlock = false;
	public int DEFAULT_LAYER = 0;
	public int VOID_LAYER = 8;
	#endregion

	#region Block Variables
	public Vector2 relativeCenter = new Vector3(0,0,0);
	public Vector2 spriteDim = new Vector2(1,1);
	public Block root = null;
	public Block parent = null;	
	public List<string> children = new List<string>();
	public virtual string Type { get; set;}
	public virtual string BlockInput { get; set; }
	public Vector3 originalScale = new Vector3(1f, 1f, 1f);
	public Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);
	public bool delete = false;
           	#endregion

	void Start(){
	}

	void Update(){
	}

	void OnMouseDown(){
	}
	
	void OnMouseDrag(){
	}

	void OnMouseUp (){
	}
	

	//allow multi-child blocks to enter into a single parent
	void OnCollisionEnter(Collision collision) {
		Block parentBlock = collision.gameObject.GetComponent<Block>();
		Block childBlock = this;
		string childName = this.name;

		if (collision.gameObject.tag == "Block" & 
		    !parentBlock.isFreeToMove & childBlock.isSelectedBlock)
		{
			if(parentBlock.parent != null){
				if(parentBlock.parent.parent != null || parentBlock.parent == childBlock.parent) return;
			}

			if(parentBlock.children.Count > 1) return;

			BlockMethods.BlockCollision.HandleParentChildCollision(childBlock, parentBlock, childName);
		}

	}
}