using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : Entity {
	public const float NORTH = 180f;
	public const float WEST = -90f;
	public const float SOUTH = 0;
	public const float EAST = 90f;



	private int xDistance = 2;
	private int yDistance = 0;


	public void setDirection (float dir){
transform.eulerAngles = new Vector3(0, 0, dir);
}
	
public void setDist(int x, int y){
	xDistance = x;
	yDistance = y;
}
	
	public override void playerCollision(){
		player.setBlindness(0f);
		player.move(xDistance, yDistance);
	}

	public override void onTick(){
	
		

	}
}
