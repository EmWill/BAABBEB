using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour {
private Map_Boss boss;
private Room currRoom;
private int x;
//might make these public
private int y;
private TileData location;

private int hp;
private bool guard;
private bool threatened;
private float blindness;
public float blindSpeed;
public AudioSource hitSoundPrefab;
private AudioSource hitSound;
public GameObject blindFoldObj;
private SpriteRenderer blindFoldSprite;
void Start (){
	hitSound = Instantiate(hitSoundPrefab);
	blindFoldObj = Instantiate(blindFoldObj, new Vector3(0, 0, 0), Quaternion.identity);
	blindFoldSprite = blindFoldObj.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
	this.blindness = 0f;
	blindFoldSprite.color = new Color(1f, 1f, 1f, blindness);
}

	public void setValues (Map_Boss bossInput){
		this.boss = bossInput;
		this.x = 0;
		this.y = 0;
		this.hp = 3;
		this.guard = false;
		this.threatened = false;
		//blindFold.color = new Color(1f, 1f,1f, 0f); 
	}

public void nextStage(){
	blindness = 0f;
	blindFoldSprite.color = new Color(1f, 1f, 1f, blindness);
}
	public void onTick(){
		this.threatened = false;
		this.guard = Input.GetKey("down"); 
		if (blindness <= 1f)
		blindness += blindSpeed;
blindFoldSprite.color = new Color(1f, 1f, 1f, blindness);

	}

	public void setCoords(int x, int y){
this.x = x; 
this.y = y;
	}

public void setBlindness(float alpha){
	blindness = alpha;
	blindFoldSprite.color = new Color(1f, 1f, 1f, blindness);
}

public int getX(){
	return this.x;
}

public int getY(){
	return this.y;
}

public bool missedGuard(){
		print(this.guard);
	return this.guard && !this.threatened;
}
	
bool checkSpace(int x, int y){
	
if (currRoom.withinScale(x, y)){
return currRoom.canEnter(x, y);
}
else return false;
}

public bool move(int x, int y){
	int xSum = x + this.x;
	int ySum = y + this.y;
	if (checkSpace(xSum, ySum)){
		this.location.setHasPlayer(false);
		this.x = xSum;
		this.y = ySum;
		this.location = currRoom.getTile(xSum, ySum);
		this.location.setHasPlayer(true);
		return true;
	}
	this.x = xSum;
	this.y = ySum;
	return false;
}

public void setRoom(int x, int y, Room room){
	currRoom = room;
	this.x = x;
	this.y = y;
	this.location = room.getTile(x, y);
	this.location.setHasPlayer(true);
}

public void threaten(){
	this.threatened = true;
}

public bool getGuard(){
	return this.guard;
}


public void damage(int dmg){
	hitSound.Play();
	hp -= dmg;
	if (hp <= 0){
		boss.goalReached();
		// placeholder. I should create a more reliable way of "killing" the player.
		hp = 3;
	}
}

public TileData getTile(){
return location;
}

}
