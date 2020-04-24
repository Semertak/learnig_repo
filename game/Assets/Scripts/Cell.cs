using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
	public bool isCollision = false;
	public bool isRightCell = false;
	public int posInGridX = 0;
	public int posInGridY = 0;

	void OnTriggerEnter2D(Collider2D collider) {
		isCollision = true;
	}

	void OnTriggerExit2D(Collider2D collider) {
		isCollision = false;
	}

	public void SetPosInGrid(int y, int x){
		posInGridX = x;
		posInGridY = y;
	}

    public int[] PosInGrid => new int[2] { posInGridX, posInGridY };
}
