using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileData : MonoBehaviour{
public bool walkable; // must stay public
private int x;
private int y;
private bool hasPlayer = false;


private List<Entity> entities = new List<Entity>();
void Start (){
	//hasPlayer = false;
}
public bool canEnter(){
	return walkable;
	// I might update this with more conditions later.
}

public void setHasPlayer(bool val){
	this.hasPlayer = val;
}

public bool getHasPlayer(){
	return this.hasPlayer;
}

public void setLocation(int x, int y){
	this.x = x;
	this.y = y;
}

public void setWalkable(bool walkable){
	this.walkable = walkable;
}

public void removeEntity(Entity entity){
	entities.Remove(entity);
}

public void addEntity(Entity entity){
	entities.Add(entity);
	if (entity.getTile() != this){
		entity.enterTile(this.x, this.y);
	}
}

public void collide(){
	foreach(Entity entity in entities){
		entity.playerCollision();
	}
}

public bool containsEntity(Entity entity){
return entities.Contains(entity);
}

public bool containsAnyEntity(){
	return entities.Count > 0;
}

}