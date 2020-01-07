using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : Room {
	private int currX;
	private int currY;

	private const int maxTeleporterGap = 5;

	private bool[,] accessible;

	private struct Step {
public int x, y, entity;

	public static bool operator == (Step step1, Step step2){
		return step1.Equals(step2);
	}

	public static bool operator != (Step step1, Step step2){
		return !step1.Equals(step2);
	}

	public override bool Equals(System.Object obj){
		return base.Equals(obj);
	}
	// !!! do NOT know if did this properly
	// use a print statement maybe to check if it's working

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public Step(int x, int y, int entity){
		this.x = x; 
		this.y = y;
		this.entity = entity;
	}
}

	private struct Branch
	{
		public LinkedList<Step> path;
		public bool[,] accessible;

		public int teleCount, currX, currY;

	  	public Branch(LinkedList<Step> pathInput, bool[,] accessibility, int currX, int currY){
			path = pathInput;
			accessible = accessibility;
			teleCount = maxTeleporterGap;
			this.currX = currX;
			this.currY = currY;
		}
		
	}


	// Use this for initialization
	void Start () {
		this.xScale = 15;
		this.yScale = 18;
		this.stage = new TileData [xScale, yScale];
		this.currX = xScale / 2;
		this.currY = 1;
		accessible = new bool [xScale, yScale];
		
			

	}

	void resetFields(){
		this.currX = xScale / 2;
		this.currY = 1;
for(int x = 0; x < xScale; x++){
			for(int y = 0; y < yScale; y++){
				accessible[x, y] = false;
			}
			}
	}

	void continuePath(int x, int y, int tile){
renderOne(x, y, tile);
accessible[x, y] = true;
accessible[x - 1, y + 1] = true;
accessible[x , y + 1] = true;
accessible[x + 1, y + 1] = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected override void generateRoom(){
DFSWalkTest();
resetFields();
	}

private void DFSWalkTest(){
	int entrance = Random.Range(2, xScale - 2);
	for (int x = 1; x < xScale; x++){
		if (!(x >= entrance -1 && x <= entrance + 1)){
			renderOne(x, yScale / 2, 0);
			accessible[x, yScale /2] = true;
		}
	}
	buildPath(walkDistanceNoTouch(xScale /2, yScale -2));
	fillEmpty();
}
	private void generateRoomTestOne(){
		//continuePath(currX, currY, 2);
		int startDir = Random.Range(0,2);
		int xEdge;
		if (startDir == 0){
			xEdge = 1;
		}
		else {
			xEdge = xScale -2;
		}
		buildPath(walkDistanceNoTouch(xEdge, yScale / 2 + 1));
		addTelepoter(currX, currY, teleporter.SOUTH, 0, -yScale / 2);
		currX = xEdge;
		currY = 1;
		continuePath(currX, currY, 2);
		walkDistance(xEdge, 6);
		if (startDir == 0){
			teleportTo(currX, currY, teleporter.EAST, xScale -3, 0);
		}
		else{
			teleportTo(currX, currY, teleporter.WEST, -(xScale -3), 0);
		}


				
				
fillEmpty();
resetFields();
	}

	private void generateRoomTeleporters(){
		Stack<Branch> frontier = new Stack<Branch>();
		Branch start = new Branch(new LinkedList<Step>(), new bool [xScale, yScale], xScale /2 , 1 );
		frontier.Push(start);
		while (frontier.Count > 0){
			Branch currBranch = frontier.Pop();
			int activeX = currBranch.currX;
			int activeY = currBranch.currY;
			if (false){ //condition to wrap it up
				// wrap up function
			}
			int teleRNG = Random.Range(0, currBranch.teleCount);
			bool teleportFirst = false;
			if (teleRNG == 0){
				teleportFirst = true;
				// goto statement
			}
			List<int> walkRand = new List<int>{-1, 0, 1};
			while (walkRand.Count > 0){
				
			}

		}
	}

	LinkedList<Step> walkDistanceNoTouch(int xDest, int yDest){
		// memory efficiency? 
		if (!withinScale(xDest, yDest) || Mathf.Abs(xDest - currX) > (yDest - currY))
		return null;
		List<Step> visited = new List<Step>();
		Stack<LinkedList<Step>> frontier = new Stack<LinkedList<Step>>();
		LinkedList<Step> path = new LinkedList<Step>();
		path.AddLast(new Step(currX, currY, -1));
		frontier.Push(path);
		while (frontier.Count > 0){
			LinkedList<Step> currPath = frontier.Pop();
			List<int> rando = new List<int>{-1, 0, 1};
			int fCurrY = currPath.Last.Value.y + 1;
			int fCurrX = currPath.Last.Value.x; // grab x coord of last tile on current path
			if (fCurrY == yDest){
				currPath.AddLast(new Step(xDest, yDest, -1));
				//buildPath(currPath);
				return currPath;
			}
			else{
			while (rando.Count > 0){ 
				int nextPick = Random.Range(0, rando.Count);
				int dir = rando[nextPick];
				rando.RemoveAt(nextPick);
				Step nextTile = new Step(fCurrX + dir, fCurrY, -1);
				if (!visited.Contains(nextTile) && (fCurrX + dir > 0 && fCurrX + dir < xScale - 1)
				&& !accessible[fCurrX + dir, fCurrY] &&
				Mathf.Abs(xDest - (fCurrX + dir)) <= (yDest - fCurrY)){
					visited.Add(nextTile);
					LinkedList<Step> newPath = new LinkedList<Step>();
					foreach(Step tile in currPath)
						newPath.AddLast(tile);
						newPath.AddLast(nextTile);
						frontier.Push(newPath);
				}
			

			}
			}
		}
		return null;
	}

	void buildPath(LinkedList<Step> path){
		foreach(Step tile in path)
		continuePath(tile.x, tile.y, 2);
		currX = path.Last.Value.x;
		currY = path.Last.Value.y;
	}

	void walkDistance(int xDest, int yDest){
		int yDist = yDest - currY;
		int dir = 0;
		while (currY < yDest){
			int xDist = xDest - currX;
			if (xDist > 0){
				dir = 1;
			}
			else dir = -1;
			int absDist = Mathf.Abs(xDist);
			if (absDist + 1 > yDist - 1){
				if (absDist >= yDist){
					moveXDir(dir);
				}
				else if(yDist > 1) {
					int rng = Random.Range(0,2);
					if (rng == 0){
						moveXDir(dir);
					}

				}
			}
			else{
			int randDir = Random.Range(-1, 2);
			moveXDir(randDir);
			}
			currY++;
			continuePath(currX, currY, 2);
			yDist--;
				}
				
		}
	
	void moveXDir(int dir){
		if (currX + dir > 0 && currX + dir < xScale - 1)
		currX += dir;
	}


	void fillEmpty(){
		for(int x = 1; x < xScale; x++){
			for (int y = 1; y < yScale; y++){
				 if(accessible[x,y])
				renderOne(x, y, 3);
				else renderOne(x, y, 2);
			}
		}

	}

	private void addTelepoter(int x, int y, float dir, int xDist, int yDist){
GameObject teleporter = Instantiate(entityBank[0], new Vector3(x, y, 0), Quaternion.identity);
				teleporter teleScript = teleporter.GetComponent(typeof (teleporter)) as teleporter;
				teleScript.setDirection(dir);
				teleScript.setDist(xDist, yDist);
				getScript(teleporter).createEntity(this, x, y);

	}

	private void teleportTo(int x, int y, float dir, int xDist, int yDist){
addTelepoter(x, y, dir, xDist, yDist);
currX += xDist;
currY += yDist;
continuePath(currX, currY, 2);

	}


}
