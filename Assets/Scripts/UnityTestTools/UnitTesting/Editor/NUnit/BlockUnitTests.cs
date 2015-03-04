using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using BlockMethods;

[TestFixture]
public class BlockUnitTests {

	[Test]
	public void BlockResizeTest(){
		Assert.AreNotEqual (1, 2);
	}


	[Test]
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
	}

}