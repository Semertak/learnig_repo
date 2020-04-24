using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInit : MonoBehaviour {
	[Header("Ширина")] public int width;
	[Header("Высота")] public int height;
	[Header("Ширина рамки")] public float border;
	[Header("х между клетками")] public float HSpace;
	[Header("y между клетками")] public float VSpace;

	public Cell[,] cellsArray;

	void Start () {	
		cellsArray = new Cell[height + 1, width];

		for (int i = 0; i < height; i++){
			for (int j = 0; j < width; j++){
				Cell cell = this.gameObject.GetComponentInChildren<Cell>();

				float xBase = this.transform.position.x + cell.GetComponent<SpriteRenderer>().bounds.size.x / 2 + border;
				float yBase = this.transform.position.y - cell.GetComponent<SpriteRenderer>().bounds.size.y / 2 - border;
				float xPos = xBase + (cell.GetComponent<SpriteRenderer>().bounds.size.x + HSpace) * j;
				float yPos = yBase - (cell.GetComponent<SpriteRenderer>().bounds.size.y + VSpace) * i ;
				Vector3 cellPos = new Vector3(xPos, yPos, 0);

				Cell newCell = Instantiate(cell, cellPos, new Quaternion (0,0,0,0));
				newCell.GetComponent<SpriteRenderer>().enabled = true;
				newCell.GetComponent<CircleCollider2D>().enabled = true;
				newCell.transform.parent = this.transform;
				newCell.SetPosInGrid(i,j);
				cellsArray[i,j] = newCell;
			}
		}
		// Создаем дополнительный ряд, для Корзин
		for (int j = 0; j < width; j++){
			Cell cell = this.gameObject.GetComponentInChildren<Cell>();

			float xBase = this.transform.position.x + cell.GetComponent<SpriteRenderer>().bounds.size.x / 2 + border;
			float yBase = this.transform.position.y - cell.GetComponent<SpriteRenderer>().bounds.size.y / 2 - border;
			float xPos = xBase + (cell.GetComponent<SpriteRenderer>().bounds.size.x + HSpace) * j;
			float yPos = yBase - (cell.GetComponent<SpriteRenderer>().bounds.size.y + VSpace) * height ;
			Vector3 cellPos = new Vector3(xPos, yPos, 0);

			Cell newCell = Instantiate(cell, cellPos, new Quaternion (0,0,0,0));
			newCell.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/GameField/ball_exit");
			newCell.GetComponent<SpriteRenderer>().enabled = true;
			newCell.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
			newCell.GetComponent<SpriteRenderer>().sortingOrder = 0;
			newCell.GetComponent<CircleCollider2D>().enabled = false;
			newCell.transform.parent = this.transform;
			newCell.SetPosInGrid(height,j);
			cellsArray[height,j] = newCell;
		}
	}

	public bool DropBlock(GameObject block){
		Cell rightCell = null;
		float minDistance = 999;
		foreach(Cell cell in cellsArray){
			if(cell.isCollision){
				float newDistance = Vector3.Distance(cell.transform.position, block.transform.position);
				if(newDistance < minDistance){
					minDistance = newDistance;
					rightCell = cell;
				}
			}
		}
		if (rightCell){
			rightCell.isRightCell = true;
			block.GetComponent<Dragger>().SetRightCell(rightCell);
			return true;
		}
		return false;
	}
}
