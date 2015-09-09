using UnityEngine;
using System.Collections;

public class loopBlock : Block {
	public string default_input = "How many minutes?";
	private string block_input = "";
	bool guiIsVisible = false;
	bool moveBack = false;

	public override string Type{

		get {
			return "loop";
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
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX 
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
			guiIsVisible = false;
			GUI.enabled = false;
		}

		if(moveBack){
			Vector3 newLocation = new Vector3 (transform.position.x, transform.position.y, 0);
			transform.position = Vector3.MoveTowards (transform.position, newLocation, .1f);
			if(transform.position.z == 0) moveBack = false;
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
		
		Destroy (transform.GetComponent<Rigidbody>());
		transform.gameObject.layer = VOID_LAYER;

		//BlockMethods.Center.MoveColliderUpManyLayers (this, -5);
		
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;

		if(parent == null && children.Count == 0 && root != this)
			transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

		guiIsVisible = true;
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

		Rigidbody newRigidBody = transform.gameObject.AddComponent<Rigidbody> ();
		newRigidBody.mass = 1;
		SetRigidBodyConstraints ();
		//BlockMethods.Center.MoveColliderUpManyLayers (this, 0);

		moveBack = true;

		transform.gameObject.layer = DEFAULT_LAYER;

		
		transform.localScale = originalScale;
		
		//transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

	}

	void OnGUI() {
		if(guiIsVisible){
			
			GUI.SetNextControlName ("text");
			Rect textfield_position = new Rect (new Rect(175, 400, 475, 150));
			GUIStyle guiStyle = GameObject.Find ("GUIStyle").GetComponent<GUIStyleCustom>().guiStyle;
			block_input = GUI.TextField (textfield_position, block_input, guiStyle);
			
			if(UnityEngine.Event.current.type == EventType.Repaint)
			{
				if(textfield_position.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
				{
					if (GUI.GetNameOfFocusedControl () == "text")
					{
						GUIStyle guiStyle2 = new GUIStyle();
						Font myFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
						guiStyle2.font = myFont;
						guiStyle2.fontSize = 24;
						GUI.Label(new Rect(250,100,400,100), "Press 'Enter' twice to submit!", guiStyle2);
						
						if (block_input == default_input) block_input = "";
					}
				}
				
				else
				{
					if(block_input == "") block_input = default_input;
				}
			}
		}
		
		if (Event.current.isKey && Event.current.keyCode == KeyCode.Return) {
			GUI.SetNextControlName ("");
			GUI.FocusControl ("");
		}
	}
}
