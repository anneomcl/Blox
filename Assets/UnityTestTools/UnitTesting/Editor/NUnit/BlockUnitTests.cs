using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using BlockMethods;

[TestFixture]
public class BlockUnitTests {
	/*		
	[Test]
	public void BlockInitializeTest(){

		Block child = Block.CreateBlock ("DEFAULT");

		Assert.AreEqual (1f, child.spriteDim.y);
		Assert.AreEqual (1f, child.spriteDim.x);
		Assert.IsNotNull (child.name);
	}

	[Test]
	public void BlockParentChildCollisionTest(){

		Block child = Block.CreateBlock ("DEFAULT1");
		Block parent = Block.CreateBlock ("DEFAULT2");

		BlockMethods.Collision.HandleParentChildCollision (child, parent, child.name);
		Assert.AreEqual (1, parent.children.Count);
	}

	[Test]
	public void BlockResizeOneChildTest(){

		Block child = Block.CreateBlock ("DEFAULT3");
		Block parent = Block.CreateBlock ("DEFAULT4");

		float margin = 0.25f; //default margin

		BlockMethods.Collision.HandleParentChildCollision (child, parent, child.name);

		Assert.AreEqual (2f, parent.spriteDim.x);
		Assert.AreEqual (2f + margin, parent.spriteDim.y);
	}



 * 
	private static GameObject blockObject;
	public static GameObject thisBlockObject{
		get{
			if(blockObject == null)
			{
				blockObject = new GameObject("DEFAULT");
			}
			return blockObject;
		}
	}

	public static Block CreateBlock(string newName)
	{
		thisBlockObject.AddComponent ("BoxCollider2D");
		//thisBlockObject.AddComponent ("RigidBody");
		thisBlockObject.AddComponent ("SpriteRenderer");
		var thisObject = thisBlockObject.AddComponent<Block> ();
		thisObject.name = newName;
		return thisObject;
	}
 * 
 * [Test]
	public void BlockOffset3ChildrenTest(){
		List<float> actualOffsetList = new List<float> ();
		BlockMethods.Center.CalculateOffsetsOddChildren (3, 4.75f, 2.38f, 1, actualOffsetList);


		List<float> expectedOffsetList = new List<float> () { 3.5f, 0, -3.5f };
		Assert.AreEqual (expectedOffsetList, actualOffsetList);
	}

	[Test]
	public void BlockOffset3ChildrenTest2(){
		List<float> actualOffsetList = new List<float> ();
		BlockMethods.Center.CalculateOffsetsOddChildren (3, 10, 5, 1, actualOffsetList);
		
		
		List<float> expectedOffsetList = new List<float> () { 2.75f, 0, -2.75f };
		Assert.AreEqual (expectedOffsetList, actualOffsetList);
	}

	[Test]
	public void BlockOffset5ChildrenTest(){
		List<float> actualOffsetList = new List<float> ();
		BlockMethods.Center.CalculateOffsetsOddChildren (3, 12, 6, 1, actualOffsetList);
		
		
		List<float> expectedOffsetList = new List<float> () { 3.5f, 0, -3.5f };
		Assert.AreEqual (expectedOffsetList, actualOffsetList);
	}*/

}