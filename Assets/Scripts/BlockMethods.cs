using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace BlockMethods {

	public static class Scale {

		public static void Resize(Block curr) {
			ResizeParents (curr);
		}
		
		public static void ResizeParents(Block curr){
			float margin = 0.25f;
			float newHeight = GetSumChildrenHeight(curr) + curr.children.Count*(margin) + 1; //Children Height + .25*2 spacing + .5*2 margin
			float newWidth = GetSumChildrenWidth(curr);
			
			ApplyScale (curr, newWidth, newHeight);
			
			if (curr.root != curr | curr.parent != null) {
				ResizeParents (curr.parent);
			}
		}
		
		public static void ApplyScale(Block curr, float newWidth, float newHeight) {
			Vector3 newScale = new Vector3 (newWidth, newHeight - curr.transform.localScale.y, 0);
			curr.transform.localScale += newScale;
			
			curr.spriteDim.y = curr.renderer.bounds.size.y;
			curr.spriteDim.x = curr.renderer.bounds.size.x;
		}
		
		public static float GetSumChildrenWidth(Block curr){
			
			float sum = GameObject.Find (curr.children[0]).GetComponent<Block>().spriteDim.x; 
			
			if (curr.spriteDim.x > (sum + sum)) 
			{
				sum = 0;
			}
			
			return sum;
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
				UpdateSingleChildCenter (curr);
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

				childBlock.relativeCenter = newCenter;
				childBlock.transform.position = newCenter;
				
				i+=1;
			}
		}

		public static void CalculateOffsetsEvenChildren(float numChildren, float parentYDim, float parentYCenter, float margin, List<float> offsetList){

			if (numChildren == 2) 
			{
				margin = 0.5f; //this is the only case where the resizing is too small for recenter
			}

			for (int i = 0; i < (int) numChildren/2; i++)
			{
				float value = parentYDim - (i+1)*(parentYCenter/((float) numChildren/2 + 1)) - parentYCenter - margin;
				if (i == 0 && numChildren > 2) value += margin;
				if (i == numChildren/2 - 1 && numChildren > 2) value += (0.25f)*margin;
				offsetList.Insert(i, value);
			}
			
			for (int i = (int) numChildren/2; i < (int) numChildren; i++)
			{
				float value = (numChildren - i)*(parentYCenter/((float) numChildren/2 + 1)) - parentYCenter + margin;
				if (i == numChildren - 1 && numChildren > 2) value -= margin;
				if (i == numChildren/2 && numChildren > 2) value -= (0.25f)*margin;
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
			float margin = 1;

			if (numChildren % 2 == 0) {
				CalculateOffsetsEvenChildren(numChildren, parentYDim, parentYCenter, margin, offsetList);
			}
			
			else
			{
				CalculateOffsetsOddChildren(numChildren, parentYDim, parentYCenter, margin, offsetList);
			}
		}

		public static void UpdateSingleChildCenter(Block curr){
			Block childBlock = GameObject.Find (curr.children[0]).GetComponent<Block> ();
			Vector3 newCenter = new Vector3 (curr.relativeCenter.x, 
			                                 curr.relativeCenter.y, 
			                                 curr.transform.position.z - 1);
			childBlock.relativeCenter = newCenter;
			childBlock.transform.position = newCenter;
		}

		public static void UpdateRootCenter(Block curr){
			if (curr.root == curr) {
				curr.relativeCenter = curr.transform.GetComponentInChildren<Renderer> ().bounds.center;
			}
		}
	}
}