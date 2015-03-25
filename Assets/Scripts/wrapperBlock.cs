using UnityEngine;
using System.Collections;

public class wrapperBlock : Block {
	
	public override string Type{
		
		get {
			return "wrapper";
		}
	}
	
	// Use this for initialization
	void Start () {
		
		Physics2D.IgnoreLayerCollision (DEFAULT_LAYER, VOID_LAYER);
	}
	
	// Update is called once per frame
	void Update () {
		
		UpdateBlockCenters (this);

	}
	
	void UpdateBlockCenters(Block curr)
	{
		BlockMethods.Center.UpdateRootCenter (curr);
		
		BlockMethods.Center.UpdateChildrenCenters (curr);
	}
	
	void TranslateToCode(){
		
		
	}
	
	void setCurrentSelectedBlock()
	{
		GameObject [] blockList = GameObject.FindGameObjectsWithTag("Block");
		foreach(GameObject blockObj in blockList)
		{
			Block block = blockObj.GetComponent<Block>();
			if(block.isSelectedBlock)
			{
				block.isSelectedBlock = false;
			}
		}
		
		this.isSelectedBlock = true;
	}
	
	void OnMouseDown(){
		isFreeToMove = true;
		setCurrentSelectedBlock ();
		
		Destroy (transform.rigidbody2D);
		transform.gameObject.layer = VOID_LAYER;
		
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
	}
	
	void OnMouseDrag(){
		isFreeToMove = true;
		
		Vector3 curPos = 
			new Vector3(Input.mousePosition.x - posX, 
			            Input.mousePosition.y - posY, dist.z);  
		
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
		
		transform.position = worldPos;
		transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
		
	}
	
	void OnMouseUp(){
		isFreeToMove = false;
		
		Rigidbody2D newRigidBody = transform.gameObject.AddComponent<Rigidbody2D> ();
		newRigidBody.mass = 1;
		
		transform.gameObject.layer = DEFAULT_LAYER;
		
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		
	}
}

