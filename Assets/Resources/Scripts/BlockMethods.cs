using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace BlockMethods {

	public static class Scale {

		public static void Resize(Block curr) {

			ResizeParents (curr);
		}
		
		public static void ResizeParents(Block curr){
			float margin = 0.5f;
			float newHeight = GetSumChildrenHeight(curr) + curr.children.Count*(margin) + 1; //Children Height + .25*2 spacing + .5*2 margin
			float newWidth = GetGreatestChildrenWidth (curr);

			if(curr.root == curr)
			{
				if(curr.children.Count > 0)
				{
					newHeight = GameObject.Find (curr.children[0]).GetComponent<Block>().spriteDim.y;
					newHeight += curr.children.Count*(margin) + 1;
				}
			}

			
			ApplyScale (curr, newWidth, newHeight);

			if(curr.children.Count == 0)
			{
				newHeight = 1.0f;
				newWidth = 1.0f;
				
				Vector3 newScale = new Vector3 (newHeight, newWidth, curr.transform.localScale.z);
				curr.transform.localScale = newScale;
				curr.spriteDim.x = curr.transform.localScale.x;
				curr.spriteDim.y = curr.transform.localScale.y;
			}

			if (curr.root != curr | curr.parent != null) {
				ResizeParents (curr.parent);
			}
		}
		
		public static void ApplyScale(Block curr, float newWidth, float newHeight) {
			float scaleHeight = newHeight;
			float scaleWidth = newWidth;
			if (newHeight - curr.transform.localScale.y > 0)
								scaleHeight = newHeight - curr.transform.localScale.y;
			/*if (newWidth - curr.transform.localScale.x > 0)
								scaleWidth = newWidth - curr.transform.localScale.x;*/

			Vector3 newScale = new Vector3 (scaleWidth, scaleHeight, 0);
			if(!curr.delete)
				curr.transform.localScale += newScale;
			else
			{
				if(curr.transform.localScale.x - newScale.x > 1 & curr.transform.localScale.y - newScale.y > 1)
					curr.transform.localScale -= newScale;
				if(curr.transform.localScale.x == 1 & curr.transform.localScale.y == 1)
					curr.transform.localScale += newScale;
			}
			curr.spriteDim.x = curr.transform.localScale.x;
			curr.spriteDim.y = curr.transform.localScale.y;
		}
		
		public static float GetGreatestChildrenWidth(Block curr){

			float greatestChildWidth = 0;
			for(int i = 0; i < curr.children.Count; i++)
			{
				float currWidth = GameObject.Find (curr.children[i]).GetComponent<Block>().transform.localScale.x;

				if(currWidth > greatestChildWidth)
				{
					greatestChildWidth = currWidth;
				}
			}

			return greatestChildWidth;
		}
		
		public static float GetSumChildrenHeight(Block curr){
			float sum = 0;
			
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

	}

	public static class Center {
		public static void UpdateChildrenCenters(Block curr){

			if (curr.children.Count == 1) {
				Block childBlock = GameObject.Find (curr.children[0]).GetComponent<Block> ();
				UpdateSingleChildCenter (curr, childBlock);
			}

			else if (curr.children.Count > 1) {
				UpdateMultipleChildCenters(curr);
			}

			else {
				return;
			}
		}

		public static void UpdateMultipleChildCenters(Block curr){

			List<float> offsets = new List<float>(curr.children.Count);
			CalculateOffsets (curr, offsets);
			RecenterChildren (curr, offsets);

		}

		public static void RecenterChildren(Block curr, List<float> offsetList){
			
			int i = 0;

			foreach (string child in curr.children) {
				Block childBlock = GameObject.Find (child).GetComponent<Block> ();
				Vector3 newCenter = new Vector3(curr.relativeCenter.x, 
				                                (float) curr.relativeCenter.y + offsetList[i], 
				                                curr.transform.position.z - 1);

				//MoveColliderUpOneLayer(curr, childBlock);
				childBlock.relativeCenter = newCenter;
				childBlock.transform.position = newCenter;
				
				i+=1;
			}
		}

		public static void UpdateSingleChildCenter(Block curr, Block childBlock){
			Vector3 newCenter = new Vector3 (curr.relativeCenter.x, 
			                                 curr.relativeCenter.y, 
			                                 curr.transform.position.z - 1);
			//MoveColliderUpOneLayer(curr, childBlock);
			childBlock.relativeCenter = newCenter;
			childBlock.transform.position = newCenter;
		}

		public static void MoveColliderUpOneLayer(Block parent, Block child)
		{
			BoxCollider bc_parent = parent.GetComponent<BoxCollider> ();
			float newColliderZ = bc_parent.center.z - 1; 

			BoxCollider bc_child = child.GetComponent<BoxCollider> ();
			bc_child.center = 
				new Vector3(bc_child.center.x, bc_child.center.y, newColliderZ);
		}

		public static void MoveColliderUpManyLayers(Block child, float newZ)
		{
			float newColliderZ = newZ;
			
			BoxCollider bc_child = child.GetComponent<BoxCollider> ();
			bc_child.center = 
				new Vector3(bc_child.center.x, bc_child.center.y, newColliderZ);
		}

		public static void CalculateOffsetsEvenChildren(float numChildren, float parentYDim, float parentYCenter, float margin, List<float> offsetList){

			if (numChildren == 2) 
			{
				margin = 0.15f; //this is the only case where the resizing is too small for recenter
			}

			for (int i = 0; i < (int) numChildren/2; i++)
			{
				float value = parentYDim - (i+1)*(parentYCenter/((float) numChildren/2 + 1)) - parentYCenter - margin;
				//if (i == 0 && numChildren > 2) value += margin;
				//if (i == numChildren/2 - 1 && numChildren > 2) value += (0.25f)*margin;
				offsetList.Insert(i, value);
			}
			
			for (int i = (int) numChildren/2; i < (int) numChildren; i++)
			{
				float value = (numChildren - i)*(parentYCenter/((float) numChildren/2 + 1)) - parentYCenter + margin;
				//if (i == numChildren - 1 && numChildren > 2) value -= margin;
				//if (i == numChildren/2 && numChildren > 2) value -= (0.25f)*margin;
				offsetList.Insert (i, value);
			}
		}

		public static void CalculateOffsetsOddChildren(float numChildren, float parentYDim, float parentYCenter, float margin, List<float> offsetList){

			for (int i = 0; i < (int) numChildren/2; i++)
			{
				float factor = ((numChildren - 1)/2) - i;
				float spacing = ((parentYCenter)/(((numChildren - 1)/2) + 1));
				float value = parentYDim - (factor*spacing) - parentYCenter;
				offsetList.Insert(i, value);
			}

			offsetList.Insert ((int) numChildren / 2, 0);
			
			for (int i = (int) ((numChildren - 1)/2) + 1; i < (int) numChildren; i++)
			{
				int index = (int) numChildren - i - 1;
				float value = -1*offsetList[index];
				offsetList.Insert (i, value);
			}
		}

		public static void CalculateOffsets(Block curr, List<float> offsetList){

			float numChildren = curr.children.Count;
			float parentYDim = curr.spriteDim.y;
			float parentYCenter = parentYDim / 2;
			float margin = .25f;

			if (numChildren % 2 == 0) {
				CalculateOffsetsEvenChildren(numChildren, parentYDim, parentYCenter, margin, offsetList);
			}
			
			else
			{
				CalculateOffsetsOddChildren(numChildren, parentYDim, parentYCenter, margin, offsetList);
			}
		}

		public static void UpdateRootCenter(Block curr){
			if (curr.root == curr) {
				curr.relativeCenter = curr.transform.position;
			}
		}
	}

	public static class BlockCollision
	{

		public static void setRoot(Block parent, Block child){
			
			if(parent.root == null) { 
				parent.root = parent;
				child.root = parent;
			}
			
			else
				child.root = parent.root;
		}

		public static void removeChild(Block currParent, Block currChild, string child){

			for(int i = 0; i < currParent.children.Count; i++)
			{
				Block curr = GameObject.Find (currParent.children[i]).GetComponent<Block>();
				if(curr.name == child)
				{
					currParent.children.Remove(child);
					currChild.parent = null;
				}
			}
		}

		public static void addChild(Block currParent, Block currChild, string child){

			int index = 0;
			bool isNotAtEnd = false;

			if (currParent.children.Count > 0) {
				for(int i = 0; i < currParent.children.Count; i++)
				{
					Block curr = GameObject.Find(currParent.children[i]).GetComponent<Block>();
					float currY = curr.transform.position.y;

					if(currChild.transform.position.y > currY)
					{
						if(i != 0)
							index = i - 1;
						isNotAtEnd = true;
						break;
					}
				}
			}

			if(!isNotAtEnd)
				currParent.children.Add(child);

			else
				currParent.children.Insert(index, child);

			currChild.parent = currParent;
		}

		public static void HandleParentChildCollision(Block childBlock, Block parentBlock, string childName)
		{
			IgnoreParentChildCollisions(childBlock, parentBlock);
			setRoot (parentBlock,childBlock);
			addChild(parentBlock,childBlock, childName);
			Scale.Resize(parentBlock);
			parentBlock.originalScale = parentBlock.transform.localScale;
			parentBlock.smallScale = (parentBlock.transform.localScale) / 2;
		}
		
		public static void IgnoreParentChildCollisions(Block childBlock, Block parentBlock)
		{
			Block curr = childBlock;
			Block currParent = parentBlock;
			
			while(currParent != null)
			{
				Physics.IgnoreCollision(curr.GetComponent<Collider>(), currParent.GetComponent<Collider>());
				currParent = currParent.parent;
			}

			foreach (string child in parentBlock.children) {
				Block chb = GameObject.Find (child).GetComponent<Block> ();
				if(chb == curr) continue;
				Physics.IgnoreCollision(curr.GetComponent<Collider>(), chb.GetComponent<Collider>());
			}
		}
	}
}