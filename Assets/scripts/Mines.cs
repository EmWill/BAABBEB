using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : Room {

	// MINES LEGEND: (x) = cannot be walked on
	// 0 = wall (x)
	// 1 = ground 
	// 2 = track
	// 3 = broken track
	// 4 = grass
	// 5-8 = water (x)
	void Start () {
		this.xScale = 10;
		this.yScale = 10;
		this.stage = new TileData [xScale, yScale];
		//generate();
	}
	

	void Update () {
		
	}

	


	protected override void generateRoom(){
		bool hasPond = false;
			trackGen();	
for(int x = 0; x < xScale; x++){
			for(int y = 0; y < yScale; y++){

			if (hasPond || !pondCheck(x, y)){
			groundGen(x, y);
			}
			else hasPond = true;
			
		}
		}
		placeSpikes();
	 	bringInTheMoles();
	}

	void groundGen(int x, int y){
if ((x >= 1 && flag[x - 1, y] == 5)|| 
(y >= 1 && flag[x, y-1] == 5)){
int rng = Random.Range(0, 2);
if (rng == 0)
renderOne(x, y, 5);
else renderOne(x, y, 2);
return;
}
int brng = Random.Range(0, 9);
if (brng == 8)
renderOne(x, y, 5);
else renderOne(x, y, 2);
	}

/* 
	void render(){
		for(int i = 0; i < scale; i++){
			for(int z = 0; z < scale; z++){
				Instantiate(stage[i, z], new Vector3(i , z  , 0), 
				Quaternion.identity);
	}
	}
	}
*/
private void bringInTheMoles(){
 for (int x = 1; x < xScale -1; x++){
	 for (int y = 2; y < yScale -1; y++){
		 if (Random.Range(1, 20) == 1 && canEnter(x, y))
		 addMole(x, y);
	 }
 }
 
}

private void addMole(int x, int y){
GameObject mole = Instantiate(entityBank[1], new Vector3(x, y, 0), Quaternion.identity);
				Mole moleScript = mole.GetComponent(typeof (Mole)) as Mole;
				getScript(mole).createEntity(this, x, y);

	}
	
	
bool pondCheck(int x, int y){
	if (x < xScale - 1 && y < yScale - 1 && flag[x, y] == -1 && flag[x + 1, y] == -1
	&& flag[x, y + 1] == -1 && flag[x + 1, y + 1] == -1){
		int pondRNG = Random.Range(0, 40);
		if (pondRNG == 10){
			renderOne(x, y + 1, 6);
			renderOne(x + 1, y + 1, 7);
			renderOne(x + 1, y , 8);
			renderOne(x, y , 9);
		return true;
		}
	}
return false;
}



void trackGen(){
	for (int y = 1; y < yScale - 1; y++){
		int rng = Random.Range(0, 10);
				if (rng == 5)
				renderOne(xScale/2, y, 4);
				else renderOne(xScale/2, y, 3);
				int zrng = Random.Range(0, 10);
				if (zrng == 5)
				renderOne(xScale/2 - 1, y, 4);
				else renderOne(xScale/2 - 1, y, 3);
	}
}

void placeSpikes(){
	for(int y = 2; y < yScale - 1; y++){
int placement = Random.Range(1, xScale - 2);
if(flag[placement, y] <= 5){
GameObject spike = Instantiate(entityBank[0], new Vector3(0, 0, 0), Quaternion.identity);
Entity spikeEnt = spike.GetComponent(typeof (Entity)) as Entity;
spikeEnt.createEntity(this, placement, y);
}
	}
}



}
