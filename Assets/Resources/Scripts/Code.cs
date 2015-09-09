using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Mono.CSharp;
using System;


namespace Code {
	
	public static class ParseBlocks {

		public static bool executionComplete;
		public static bool didNotCompile;

		public static bool ParseCode()
		{
			Block root = FindWrapperBlock ();
			string code = ParseWrapperBlock (root);
			Debug.Log("Parsed code: " + code);
			string answer = GameObject.FindGameObjectWithTag ("Puzzle").GetComponent<Puzzle> ().solution;
			if(!didNotCompile && answer == code)
			{
				executionComplete = true;
				return true;
			}

			else if(!didNotCompile)
			{
				executionComplete = false;
				Debug.Log ("Incorrect answer. Should be: " + answer);
				Debug.Log ("Actually got: " + code);
				return false;
			}

			else
			{
				didNotCompile = false;
				Debug.Log ("Did not compile. Please try again.");
				return false;
			}
		}

		public static Block FindWrapperBlock(){
			GameObject [] blockObjects = GameObject.FindGameObjectsWithTag ("Block");
			
			foreach (GameObject block in blockObjects) {
				if(block.GetComponent<Block>().Type == "wrapper")
					return block.GetComponent<Block>();
			}
			
			return null;
		}

		public static string ParseWrapperBlock(Block root)
		{
			string code = "";

			//there will be at most 3 layers of the tree, including the root
			//there will be at most 2 children per block

			code += ParseChildrenBlocks (root);

			return code;
		}

		public static string ParseLoopBlock(Block loop)
		{
			CheckForErrors (loop);
			string code = "for(int i = 0; i < " + loop.BlockInput + "; i++){";
			code += ParseChildrenBlocks (loop);
			code += "}";
			return code;
		}

		public static string ParseDoBlock(Block doBlock)
		{
			CheckForErrors(doBlock);
			string code = "Do('" + doBlock.BlockInput + "');";
			code += ParseChildrenBlocks (doBlock);
			return code;
		}

		public static void Do(string input)
		{
			//do nothing
		}

		public static string ParsePrintBlock(Block print)
		{
			CheckForErrors (print);
			string code = "Debug.Log('" + print.BlockInput + "');";
			code += ParseChildrenBlocks (print);
			return code;
		}

		public static void CheckForErrors(Block curr)
		{
			if(curr.parent.Type == "print" | curr.parent.Type == "Do")
			{
				didNotCompile = true;
			}
			int n;
			if(curr.Type == "loop" && !int.TryParse(curr.BlockInput, out n))
			{
				didNotCompile = true;
			}
		}

		public static string ParseChildrenBlocks(Block root)
		{
			string code = "";
			foreach(string child in root.children)
			{
				Block currChild = GameObject.Find(child).GetComponent<Block>();
				if(currChild.Type == "loop") code += ParseLoopBlock(currChild);
				if(currChild.Type == "print") code += ParsePrintBlock(currChild);
				if(currChild.Type == "do") code += ParseDoBlock(currChild);
			}
			return code;
		}

		public static string ExecuteParsedCode(string parsedCode){
			Mono.CSharp.Evaluator.Init (new string[] {});
			foreach(System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				Mono.CSharp.Evaluator.ReferenceAssembly(assembly);
			}
			Evaluator.Run ("using UnityEngine;\n" + "using System;");
			if(Evaluator.Run(parsedCode) && !didNotCompile) Debug.Log ("Code compiled successfully.");
			else
			{
				Debug.Log ("ERROR");
				didNotCompile = true;
			}
			parsedCode = "";
			return "";
		}
	}
}