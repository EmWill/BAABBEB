using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour, Room {
TileData[,] stage;
private int[,] flag;
public int scale;
public GameObject[] tiles;
	// Use this for initialization
	void Start () {
		stage = new TileData [scale, scale];
		//generate();
	}
	
	// Update is called once per frame
	void Update () {/* 
		if (Input.GetButtonDown("Submit")){
			for(int x = 0; x < scale; x++){
			for(int y = 0; y < scale; y++){
		Destroy(stage[x, y].sprite);
		}
		}
		generate();
	}
	*/
	}

	public void clearRoom(){
			for(int x = 0; x < scale; x++){
			for(int y = 0; y < scale; y++){
				if (stage[x, y] != null)
		Destroy(stage[x, y].sprite);
		}
		}
		
	}


	public void generate(){
		clearRoom();
		bool hasPond = false;
       flag = new int [scale, scale];
		for(int x = 0; x < scale; x++){
			for(int y = 0; y < scale; y++){
				flag[x, y] = -1;
			}
			}

			wallGen();
			trackGen();
		
for(int x = 0; x < scale; x++){
			for(int y = 0; y < scale; y++){

			if (hasPond || !pondCheck(x, y)){
			groundGen(x, y);
			}
			else hasPond = true;
			
		}
		}
	}

	void groundGen(int x, int y){
if ((x >= 1 && flag[x - 1, y] == 4)|| 
(y >= 1 && flag[x, y-1] == 4)){
int rng = Random.Range(0, 2);
if (rng == 0)
renderOne(x, y, 4, false);
else renderOne(x, y, 1, false);
return;
}
int brng = Random.Range(0, 9);
if (brng == 8)
renderOne(x, y, 4, false);
else renderOne(x, y, 1, false);
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

	void renderOne(int x, int y, int tile, bool locked){
		if (flag[x, y] == -1){
		stage[x, y] = new TileData(Instantiate(tiles[tile], new Vector3(x, y, 0), Quaternion.identity), x, y);
		flag[x, y] = tile;
		}
	}
	
bool pondCheck(int x, int y){
	if (x < scale - 1 && y < scale - 1 && flag[x, y] == -1 && flag[x + 1, y] == -1
	&& flag[x, y + 1] == -1 && flag[x + 1, y + 1] == -1){
		int pondRNG = Random.Range(0, 40);
		if (pondRNG == 10){
			renderOne(x, y + 1, 5, true);
			renderOne(x + 1, y + 1, 6, true);
			renderOne(x + 1, y , 7, true);
			renderOne(x, y , 8, true);
		return true;
		}
	}
return false;
}

void wallGen(){
	for (int x = 0; x < scale; x++){
	renderOne(x, 0, 0, true);
	renderOne(x, scale - 1, 0, true);
	}
	for (int y = 0; y < scale; y++){
	renderOne(0, y, 0, true);
	renderOne(scale - 1, y, 0, true);
	}
}

void trackGen(){
	for (int y = 1; y < scale - 1; y++){
		int rng = Random.Range(0, 10);
				if (rng == 5)
				renderOne(scale/2, y, 3, true);
				else renderOne(scale/2, y, 2, true);
				int zrng = Random.Range(0, 10);
				if (zrng == 5)
				renderOne(scale/2 - 1, y, 3, true);
				else renderOne(scale/2 - 1, y, 2, true);
	}
}

public TileData getStage(int x, int y){
	return stage[x,y];

}

}
