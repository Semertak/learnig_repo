using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e = Engine; 

public class ConditionBlock : MonoBehaviour
{
    public e.Color color;
    public Sprite[] sprites;
	public Dictionary<e.Color, Sprite> spritesDict = null;
	public float time = 0;
	public int step = 0;
	public bool active = false;
	void Awake(){
		spritesDict = new Dictionary<e.Color, Sprite> {
			{e.Color.Green, sprites[0]},
			{e.Color.Purple, sprites[1]},
			{e.Color.Blue, sprites[2]},
			{e.Color.Yellow, sprites[3]},
			{e.Color.Red, sprites[4]}
		};
	}

}
