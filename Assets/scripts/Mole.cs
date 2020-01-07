using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Entity {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		public override void playerCollision(){
			player.threaten();
			if (player.getGuard()){
				selfDestruct();
			}
			else{
			player.damage(1);
			print("ouch!");
			}

	}

	private void selfDestruct(){
		room.removeEntity(gameObject);
		Destroy(gameObject);
	}

	public override void onTick(){
trackPlayer();
	}

private void trackPlayer(){
	int xDest = player.getX();
	int yDest = player.getY(); // save player coordinates as destination
	List<Tuple> visited = new List<Tuple>();
		Queue<LinkedList<Tuple>> frontier = new Queue<LinkedList<Tuple>>();
		LinkedList<Tuple> path = new LinkedList<Tuple>();
		path.AddLast(new Tuple(xPos, yPos));
		frontier.Enqueue(path);
while (frontier.Count > 0){

			LinkedList<Tuple> currPath = frontier.Dequeue();
			int fCurrY = currPath.Last.Value.yTup; // grab y coord of last tile on current path
			int fCurrX = currPath.Last.Value.xTup; // grab x coord of last tile on current path
			if (fCurrY == yDest && fCurrX == xDest){
				if(currPath.Count > 1){
			enterTile(currPath.First.Next.Value.xTup, currPath.First.Next.Value.yTup);
				}
			}
else{
	for (int x = -1; x <= 1; x++){
		for (int y = 1; y >= -1; y--){
			if (!(x == 0 && y == 0)){
				Tuple nextTile = new Tuple(fCurrX + x, fCurrY + y);
				if (!visited.Contains(nextTile) &&
				(fCurrX + x > 0 && fCurrX + x < room.getXScale() - 1) &&
				(fCurrY + y > 0 && fCurrY + y < room.getYScale() - 1) &&
				 room.getTile(fCurrX + x, fCurrY + y).canEnter() &&
				 (!room.getTile(fCurrX + x, fCurrY + y).containsAnyEntity()
				 || room.getTile(fCurrX + x, fCurrY + y).getHasPlayer())){
					visited.Add(nextTile);
					LinkedList<Tuple> newPath = new LinkedList<Tuple>();
					foreach(Tuple tile in currPath)
						newPath.AddLast(tile);
						newPath.AddLast(nextTile);
						frontier.Enqueue(newPath);
				}
			}
		}
	}
}
}
}

}
