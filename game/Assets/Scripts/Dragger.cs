using System;
using System.Collections;
using UnityEngine;
using cb = ControlBlock;
using e = Engine;
 
class Dragger : MonoBehaviour
{
    public bool dragging = false;
    public Vector3 delta = new Vector3(0F, 0F, 0F);
    public Cell rightCell;

    void OnMouseDown()
    {
        dragging = true;
        delta = this.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (rightCell)
        {
            rightCell.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
 
    void OnMouseUp()
    {
        dragging = false;
        if ( !GameObject.Find("Grid").GetComponent<GridInit>().DropBlock(this.gameObject) ){
            GameObject.Find("Inventory").GetComponent<Inventory>().FailDropBlock(this.gameObject);
        }
        else{
            GameObject.Find("Inventory").GetComponent<Inventory>().SuccedDropBlock(this.gameObject);
        };
    }
 
    void Update()
    {
        if (dragging)
        {
			this.transform.MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition) + delta, 0);
        }
    }

    public void SetRightCell(Cell cell)
    {
        rightCell = cell;
        GameObject obj = rightCell.gameObject;

		if (rightCell.isRightCell) {
			this.transform.Translate( obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y, 0F);
			rightCell.isRightCell = false;
			obj.GetComponent<CircleCollider2D>().enabled = false;

            // При отпускании блока, нужно добалять соответсвующий блок в game
            var cellPos = rightCell.PosInGrid;
            var go = this.gameObject;
            e.Game game = GameObject.Find("GameField").GetComponent<Level>().game;
            switch (go.GetComponent<ControlBlock>().GetBlockType()){
                
                case cb.BlockType.cbAction:
                    Debug.Log("cdAction");
                    var new_act = new e.ColorSwitch(0, this.GetComponent<ColorSwitch>().color, rightCell.posInGridX, rightCell.posInGridY);
                    game.AddObject(new_act);
                    break;
                case cb.BlockType.cbCondition:
                    Debug.Log("cbCondition");
                    var new_cond = new e.ColorCondition(go.GetComponent<ConditionBlock>().color, new e.Point(-1, 0), rightCell.posInGridX, rightCell.posInGridY);
                    game.AddObject(new_cond);
                    break;
                case cb.BlockType.cbCicle:
                    break;
		    }
		}
    }
}