using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine;

public class Ball : MonoBehaviour {
	public e.Ball eBall = null;
	public Sprite[] sprites;
	private Dictionary<e.Color, Sprite> spritesDict = null;
	public float time = 0;
	public int step = 0;
	private Vector3 step_position = Vector3.zero;
	void Awake(){
		spritesDict = new Dictionary<e.Color, Sprite> {
			{e.Color.Green, sprites[0]},
			{e.Color.Purple, sprites[1]},
			{e.Color.Blue, sprites[2]},
			{e.Color.Yellow, sprites[3]},
			{e.Color.Red, sprites[4]}
		};
		step_position = this.transform.position;
	}
	
	public Sprite GetSpriteByColor(e.Color color){
		return spritesDict[color];
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		var actions = this.eBall.GetMoveActions();
		if (actions.Count > step) {
			var action = actions[step];
			var grid = GameObject.Find("Grid").GetComponent<GridInit>();
			Cell cell = null;
			var pos = action.Position;
			if (pos.X >= 0 && pos.Y >= 0) {
				cell = grid.cellsArray[pos.Y, pos.X];
				var cell_actions = this.eBall.GetActionsByCell(pos.X, pos.Y);
				foreach(var cell_action in cell_actions) {
					switch (cell_action.Type) {
						case e.ActionType.BallProduced:
							
						break;
						case e.ActionType.BallMove:
							if (cell) {
								this.transform.position = Vector3.Lerp(this.step_position, cell.transform.position, time);
								Vector3 difference = this.step_position - cell.transform.position; 
								difference.Normalize();
								// вычисляемый необходимый угол поворота
								float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
								// Применяем поворот вокруг оси Z
								this.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
							}
							else if(false) {

							}
						break;
						case e.ActionType.BallChangeColor:
							if(time >= 1){
								// TODO Механика получения нужного цвета.
								// При расстановке блоков надо сохранять цвет и от бэка получать цвет на который поменяли.
								// Для этого при выставлении блока надо сохранять его цвет в бэк
								// А для этого цвет надо хранить в экземпляре блока.
								e.Color color = cell_action.Color;
								this.GetComponent<SpriteRenderer>().sprite = spritesDict[color];
							}
						break;
					}
				}
				if(this.transform.position == cell.transform.position){
					this.step_position = this.transform.position;
					step++;	
					time = 0;		
				}
			}
			else {
				if(time >= 1){
					step++;	
					time = 0;	
				}
			}
			
		}
		
	}
}
