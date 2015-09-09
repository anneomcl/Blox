/* TO-DO list:

- code parsing overhaul:
	tree structure
	nested loops parse to i, j, k; any more and there is an error
	print cannot nest anything

- Unsnapping:
if a block has a root, it is in a compound
if the block is a leaf: 
	allow it to move freely
	resize its parents
if the block has any children:
	delete its children
	allow it to move freely
	resize its parents

- Reject uncompilable combinations
	Right now, is only two print blocks
	Recursive tree traversal
	

*/