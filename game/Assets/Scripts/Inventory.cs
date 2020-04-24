using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine;
using cb = ControlBlock;

public class Inventory : MonoBehaviour {
	Vector3 switcherItemPosition = new Vector3(0F, 0F, 0F);
	Vector3 counterItemPosition = new Vector3(0F, 0F, 0F);
	private GameObject [] switcherInInventoryArray = null;
	Vector3 conditionItemPosition = new Vector3(0F, 0F, 0F);
	private GameObject [] conditionInInventoryArray = null;
	private int redSwitcherCounter = 2;
	private int greenSwitcherCounter = 2;
	private int yellowSwitcherCounter = 2;
	private int purpleSwitcherCounter = 2;
	private int blueSwitcherCounter = 2;
	private int switcherCounter = 0;
	private int redConditionCounter = 1;
	private int greenConditionCounter = 1;
	private int yellowConditionCounter = 1;
	private int purpleConditionCounter = 1;
	private int blueConditionCounter = 1;
	private int conditionCounter = 0;
	GameObject draggedBlock = null;

	public void OnBlockUp(GameObject block){
		this.draggedBlock = block;
	}

	public void OnBlockDown(GameObject block){
		this.draggedBlock = null;
	}

	void Start(){
		switcherCounter = redSwitcherCounter + greenSwitcherCounter + yellowSwitcherCounter + purpleSwitcherCounter + blueSwitcherCounter;
		conditionCounter = redConditionCounter + greenConditionCounter + yellowConditionCounter + purpleConditionCounter + blueConditionCounter;
		this.switcherInInventoryArray = new GameObject[switcherCounter];
		this.conditionInInventoryArray = new GameObject[conditionCounter];

		var tmp_switncher = GameObject.Find("Switcher");
		tmp_switncher.GetComponent<SpriteRenderer>().enabled = false;
		tmp_switncher.GetComponent<Collider2D>().enabled = false;

		switcherItemPosition = tmp_switncher.GetComponent<SpriteRenderer>().transform.position;

		for (var i=0; i<switcherCounter; i++){
			e.Color color = GetSwitcherColor();
			tmp_switncher.GetComponent<ColorSwitch>().color = color;
			tmp_switncher.GetComponent<SpriteRenderer>().sprite = tmp_switncher.GetComponent<ColorSwitch>().spritesDict[color];
			var clone = Instantiate(tmp_switncher);
			clone.transform.position = switcherItemPosition;
			clone.transform.parent = this.transform;
			clone.GetComponent<SpriteRenderer>().enabled = false;
			clone.GetComponent<Collider2D>().enabled = false;
			this.switcherInInventoryArray[i] = clone;
		}

		var tmpCondition = GameObject.Find("Condition");
		tmpCondition.GetComponent<SpriteRenderer>().enabled = false;
		tmpCondition.GetComponent<Collider2D>().enabled = false;

		counterItemPosition = tmpCondition.GetComponent<SpriteRenderer>().transform.position;

		for (var i=0; i<conditionCounter; i++){
			e.Color color = GetConditionColor();
			tmpCondition.GetComponent<ConditionBlock>().color = color;
			tmpCondition.GetComponent<SpriteRenderer>().sprite = tmpCondition.GetComponent<ConditionBlock>().spritesDict[color];
			var clone = Instantiate(tmpCondition);
			clone.transform.position = counterItemPosition;
			clone.transform.parent = this.transform;
			clone.GetComponent<SpriteRenderer>().enabled = false;
			clone.GetComponent<Collider2D>().enabled = false;
			this.conditionInInventoryArray[i] = clone;
		}
		
		ShowNextChangeBlock();
		ShowNextConditionBlock();
	}
	public void FailDropBlock(GameObject block){
		switch (block.GetComponent<ControlBlock>().GetBlockType()){
			case cb.BlockType.cbAction:
				block.transform.position = switcherItemPosition;
				ReturnBlockToInventory(block);
				break;
			case cb.BlockType.cbCondition:
				block.transform.position = conditionItemPosition;
				//ReturnBlockToInventory(block);
				break;
			case cb.BlockType.cbCicle:
				Debug.Log("Cicle");
				break;
		}
	}
	public void SuccedDropBlock(GameObject block){
		switch (block.GetComponent<ControlBlock>().GetBlockType()){
			case cb.BlockType.cbAction:
				ShowNextChangeBlock();
				break;
			case cb.BlockType.cbCondition:
				ShowNextConditionBlock();
				break;
			case cb.BlockType.cbCicle:
				break;
		}
	}

	void ShowNextChangeBlock(){
		if (switcherCounter == 0)	{	return;	}
		
		GameObject next_item = this.switcherInInventoryArray[switcherCounter - 1];

		next_item.GetComponent<SpriteRenderer>().enabled = true;
		next_item.GetComponent<Collider2D>().enabled = true;

		switcherCounter--;
	}
	void ShowNextConditionBlock(){
		if (conditionCounter == 0)	{	return;	}
		
		GameObject next_item = this.conditionInInventoryArray[conditionCounter - 1];

		next_item.GetComponent<SpriteRenderer>().enabled = true;
		next_item.GetComponent<Collider2D>().enabled = true;

		conditionCounter--;
	}
	void ReturnBlockToInventory(GameObject block){
		if (switcherCounter > 0){
			block.GetComponent<SpriteRenderer>().enabled = false;
			block.GetComponent<Collider2D>().enabled = false;
		}		
		this.switcherInInventoryArray[switcherCounter] = block;
		switcherCounter++;
	}

	e.Color GetSwitcherColor(){
		if (redSwitcherCounter > 0){
			redSwitcherCounter--;
			return e.Color.Red;
		}
		if (greenSwitcherCounter > 0){
			greenSwitcherCounter--;
			return e.Color.Green;
		}
		if (yellowSwitcherCounter > 0){
			yellowSwitcherCounter--;
			return e.Color.Yellow;
		} 
		if (purpleSwitcherCounter > 0){
			purpleSwitcherCounter--;
			return e.Color.Purple;
		} 
		if (blueSwitcherCounter > 0){
			blueSwitcherCounter--;
			return e.Color.Blue;
		}
		return e.Color.Red;
	}

	e.Color GetConditionColor(){
		if (redConditionCounter > 0){
			redConditionCounter--;
			return e.Color.Red;
		}
		if (greenConditionCounter > 0){
			greenConditionCounter--;
			return e.Color.Green;
		}
		if (yellowConditionCounter > 0){
			yellowConditionCounter--;
			return e.Color.Yellow;
		} 
		if (purpleConditionCounter > 0){
			purpleConditionCounter--;
			return e.Color.Purple;
		} 
		if (blueConditionCounter > 0){
			blueConditionCounter--;
			return e.Color.Blue;
		}
		return e.Color.Red;
	}

}

