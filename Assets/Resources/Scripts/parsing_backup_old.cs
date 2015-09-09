/*using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Mono.CSharp;
using System;


namespace Code {
	
	public static class ParseBlocks {

		public static bool complete;
		public static bool didNotCompile;

		public static void ParseCodeBlock() { 

			Block root = findRootBlock();

			string code = "";
			if(root != null)
				 code = ParseRootCodeBlock (root);

			else
				Debug.Log ("No block found!");

			Debug.Log ("Parsed code: " + code);

			string answer = GameObject.FindGameObjectWithTag ("Puzzle").GetComponent<Puzzle> ().solution;
			if (!didNotCompile && GameObject.FindGameObjectWithTag ("Puzzle").GetComponent<Puzzle> ().solution == code)
			{
				complete = true;
				Debug.Log ("Your answer is correct!");
			}
			else
			{
				if(!didNotCompile)
				{
					complete = false;
					Debug.Log (code);
					Debug.Log ("Incorrect answer. Should be: " + answer);
				}
			}

			if(didNotCompile)
			{
				Debug.Log ("Did not compile. Please try again.");
				didNotCompile = false;
				
			}
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

		//Only works in .NET 4.0+ frameworks :(
		/*public static string ExecuteParsedCode(string parsedCode){

			CodeDomProvider provider = CodeDomProvider.CreateProvider ("CSharp");
			bool compileOK = false;
			string exeName = "parsedCode.exe";

			CompilerParameters cp = new CompilerParameters ();
			cp.GenerateExecutable = true;
			cp.OutputAssembly = exeName;
			cp.GenerateInMemory = false;
			cp.TreatWarningsAsErrors = false;

			CompilerResults cr = provider.CompileAssemblyFromSource (cp, parsedCode);

			if(cr.Errors.Count > 0) compileOK = false;
			else compileOK = true;

			return "";
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

				Block currRoot = root;

				foreach (string child in currRoot.children)
				{
					Block currChild = GameObject.Find(child).GetComponent<Block>();
					if(currChild.Type == "print") code += ParsePrintBlock(currChild);
					if(currChild.Type == "loop") code += ParseLoopBlock(currChild);
					if(currChild.Type == "do") code += ParseDoBlock(currChild);
				}


				return code;
			}
		}

		public static string ParseDoBlock(Block child)
		{
			string expression = "Do('" + child.BlockInput + "');";
			if (child.parent.Type == "print" | child.parent.Type == "do")
			{
				didNotCompile = true;
				return "";
			}
			return expression;
		}

		public static void Do(string input){
			//do nothing
			
		}
	
		public static string ParsePrintBlock(Block child)
		{	
			string expression = "Debug.Log('" + child.BlockInput + "');";
			if (child.parent.Type == "print" | child.parent.Type == "do")
			{
				didNotCompile = true;
				return "";
			}
			return expression;
		}

		//make functionality for inner loops (replace i with j or k)
		public static string ParseLoopBlock(Block child)
		{
			string expression = "for(int i = 0; i < " + child.BlockInput + "; i++){";

			if (child.parent.Type == "print" | child.parent.Type == "print")
			{
				didNotCompile = true;
				return "";
			}

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
}*/