using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBlock : MonoBehaviour {
	public enum BlockType
	{
		cbCondition = 0,
		cbAction = 1,
		cbCicle = 2
	}

	[Header("BlockType")]
	public BlockType blockType;

	public BlockType GetBlockType(){
		return blockType;
	}
}
