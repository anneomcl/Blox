using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Code {

	public static class ParseBlocks {

		public static void ParseCodeBlock() { 

			Block root = findRootBlock();

			string code = "";
			if(root != null)
				 code = ParseRootCodeBlock (root);

			else
				Debug.Log ("No block found!");

			Debug.Log ("Parsed code: " + code);
		}

		public static Block findRootBlock(){
			GameObject [] blockObjects = GameObject.FindGameObjectsWithTag ("Block");
			
			foreach (GameObject block in blockObjects) {
				if(block.GetComponent<Block>().Type == "wrapper")
					return block.GetComponent<Block>();
			}

			return null;
		}

		public static string ParseRootCodeBlock(Block root){

			if(root.Type == null)
				return "";

			else
			{
				Debug.Log ("Found root");
				Debug.Log (root.Type + ": " + root.BlockInput);
				string code = "";

				//FIX: currently NOT a tree traversal
				foreach (string child in root.children)
				{
					Block currChild = GameObject.Find(child).GetComponent<Block>();
					if(currChild.Type == "print") code += ParsePrintBlock(currChild);
					if(currChild.Type == "loop") code += ParseLoopBlock(currChild);
				}

				return code;
			}
		}

		public static string ParsePrintBlock(Block child)
		{
			string expression = "Debug.Log(\"" + child.BlockInput + "\");\n";
			return expression;
		}

		public static string ParseLoopBlock(Block child)
		{
			string expression = "for(i = 0; i < " + child.BlockInput + "; i++){";

			if(child.children.Count > 0){
				foreach (string currChild in child.children)
				{
					Block loopChild = GameObject.Find(currChild).GetComponent<Block>();
					if(loopChild.Type == "print") expression += ParsePrintBlock(loopChild);
					if(loopChild.Type == "loop") expression += ParseLoopBlock(loopChild);
				}
			}

			expression += "}";
			return expression;
		}
	}
}