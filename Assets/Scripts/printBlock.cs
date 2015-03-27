using UnityEngine;
using System.Collections;

public class printBlock : Block {
	
	public string default_input = "Print statement?";
	public string block_input = "";
	bool guiIsVisible = false;
	
	public override string Type{
		get {
			return "print";
		}
	}

	public override string BlockInput{
		
		get {
			return block_input;
		}
		
		set {
			block_input = value;
		}
	}
	
	// Use this for initialization
	void Start () {
		SetRigidBodyConstraints ();
		Physics.IgnoreLayerCollision (DEFAULT_LAYER, VOID_LAYER);
	}

	void SetRigidBodyConstraints()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX 
			| RigidbodyConstraints.FreezeRotationY 
				| RigidbodyConstraints.FreezeRotationZ 
				| RigidbodyConstraints.FreezePositionX
				| RigidbodyConstraints.FreezePositionY 
				| RigidbodyConstraints.FreezePositionZ;
	}
	
	// Update is called once per frame
	void Update () {
		
		UpdateBlockCenters (this);
		
		if((Input.GetKeyDown("return") & block_input != default_input) | Input.GetKeyDown("escape")){
			Debug.Log (block_input);
			guiIsVisible = false;
			GUI.enabled = false;
		}
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
		
		Destroy (transform.rigidbody);
		transform.gameObject.layer = VOID_LAYER;

		BlockMethods.Center.MoveColliderUpManyLayers (this, -5);
		
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
		guiIsVisible = true;
	}
	
	void OnMouseDrag(){
		isFreeToMove = true;
		
		Vector3 curPos = 
			new Vector3(Input.mousePosition.x - posX, 
			            Input.mousePosition.y - posY, dist.z);  
		
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
		
		if (worldPos.z < -5)
			worldPos.z = -5;
		
		transform.position = worldPos;
		
		transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
		
	}
	
	void OnMouseUp(){
		isFreeToMove = false;

		Rigidbody newRigidBody = transform.gameObject.AddComponent<Rigidbody> ();
		newRigidBody.mass = 1;
		SetRigidBodyConstraints ();
		BlockMethods.Center.MoveColliderUpManyLayers (this, 0);

		transform.gameObject.layer = DEFAULT_LAYER;
		
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

	}
	
	void OnGUI() {
		if(guiIsVisible){
			Rect textfield_position = new Rect (new Rect(10, 10, 200, 20));
			block_input = GUI.TextField (new Rect (10, 10, 200, 20), block_input);
			
			if(UnityEngine.Event.current.type == EventType.Repaint)
			{
				
				if(textfield_position.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
				{
					if(block_input == default_input)
					{
						
					}
				}
				
				else
				{
					if(block_input == "") block_input = default_input;
				}
			}
		}
	}
}
