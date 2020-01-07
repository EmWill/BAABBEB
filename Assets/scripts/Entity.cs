using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour  {
	protected int xPos;
	protected int yPos;
	protected Room room;
	protected TileData tile;
protected Player player;
	public abstract void onTick();

public void createEntity(Room room, int x, int y){
	this.room = room;
	room.addEntity(this.gameObject);
	this.player = room.getPlayer();
	this.xPos = x;
	this.yPos = y;
	transform.position = new Vector3(x, y, 0);
	enterTile(x, y);
}

public void enterTile(int x, int y){
if (this.tile != null){
	this.tile.removeEntity(this);
}
this.tile = room.getTile(x, y);
if(!this.tile.containsEntity(this)){
this.tile.addEntity(this);
}
transform.position = new Vector3(x, y, 0);
this.xPos = x;
this.yPos = y;
}

public TileData getTile(){
	return this.tile;
}

public abstract void playerCollision(); 



}
