using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour {
    protected int xScale;
    protected int yScale;
 protected TileData[,] stage;
protected Player player;

public GameObject[] tiles;

public GameObject[] entityBank;
protected int[,] flag;

protected List<GameObject> entities = new List<GameObject>();

protected void renderOne(int x, int y, int tile){
		if (flag[x, y] == -1){
		stage[x, y] = Instantiate(tiles[tile], new Vector3(x, y, 0), 
		Quaternion.identity).GetComponent(typeof(TileData)) as TileData;
		stage[x, y].setLocation(x, y);
		flag[x, y] = tile;
		}
	}

    public void generate(){
		player.nextStage();
clearRoom();
entities = new List<GameObject>();
flag = new int [xScale, yScale];
		for(int x = 0; x < xScale; x++){
			for(int y = 0; y < yScale; y++){
				flag[x, y] = -1;
			}
			}
            wallGen();
            generateRoom();
}
protected abstract void generateRoom();



  protected void wallGen(){
	for (int y = 0; y < yScale; y++){
	renderOne(0, y, 0);
	renderOne(xScale - 1, y, 0);
	}
	for (int x = 0; x < xScale; x++){
	renderOne(x, 0, 0);
	renderOne(x, yScale - 1, 1);
	}
}


public bool withinScale(int x, int y){
    return x >= 0 && x < xScale && y >= 0 && y < yScale;

}


public void clearRoom(){
			for(int x = 0; x < xScale; x++){
			for(int y = 0; y < yScale; y++){
				if (stage[x, y] != null)
		Destroy(stage[x, y].gameObject);
		}
		}
		foreach(GameObject entity in entities){
			Destroy(entity);
		}
		
	}

public bool canEnter(int x, int y){
    return stage[x,y].canEnter();
}

public TileData getTile(int x, int y){
    return stage[x, y];
}

public int getXScale(){
    return xScale;
}

public int getYScale(){
    return yScale;
}

public TileData getStage(int x, int y){
	return stage[x,y];

}

public void setPlayer(Player newPlayer){
	this.player = newPlayer;
}  

public Player getPlayer(){
	return this.player;
}

public void advanceTime(){
	foreach(GameObject entity in entities){
		getScript(entity).onTick();
	}
}

public void addEntity(GameObject entity){
entities.Add(entity);
}

public void removeEntity(GameObject entity){
	entities.Remove(entity);
}

protected Entity getScript(GameObject entity){
Entity entityScript = entity.GetComponent(typeof (Entity)) as Entity;
return entityScript;
}

}
