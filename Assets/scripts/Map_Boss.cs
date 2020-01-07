using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Boss : MonoBehaviour {
	public Camera camera;
	public AudioSource beat;
	public GameObject mineFab;
	public GameObject oceanFab;
public GameObject playerFab;
	private GameObject mine;
	private GameObject ocean;
	private Room currRoom;
	private GameObject playerObj;
	public float timerFull;
    private float timer;	
	private Player player;
	private int dir; 

	private bool awake = false; // remove
// 4.5 - 4.5 cam pos
	// Use this for initialization
	void Start () {
		timer = timerFull;
		dir = 0;
		mine = Instantiate(mineFab);
		ocean = Instantiate(oceanFab);
		currRoom = mine.GetComponent(typeof(Mines)) as Mines;
		playerObj = Instantiate(playerFab);
		player = playerObj.GetComponent(typeof(Player)) as Player;
		player.setValues(this);
		player.setCoords(1, 1);
	}
	
	void LateUpdate(){
		playerPos(); //please don't keep this in update for the entire game
	}
	// Update is called once per frame
	void Update () {
		controls();
		if (awake) timer -= Time.deltaTime;
		if (timer <= 0)
		timerOperations();

		if (Input.GetButtonDown("Submit")){
			int rng = Random.Range(0, 3);
			print(rng);
		}

		if (Input.GetKeyDown("m")){
			currRoom.clearRoom();
			currRoom = mine.GetComponent(typeof(Mines)) as Mines;
			reset();
		}

		if (Input.GetKeyDown("o")){
			currRoom.clearRoom();
			currRoom = ocean.GetComponent(typeof(Ocean)) as Ocean;
			reset();
		}
	}

	void playerPos(){
		playerObj.transform.position = new Vector3(player.getX(), player.getY(), 0);
	}
	
	public void reset(){
		camera.orthographicSize = currRoom.getYScale() / 2;
		camera.transform.position = new Vector3(currRoom.getXScale() /2 -0.5f, currRoom.getYScale() / 2 -0.5f, -10f );
		currRoom.setPlayer(this.player);
currRoom.generate();
			player.setRoom(currRoom.getXScale() /2, 1, currRoom);
			awake = true; // remove
			timer = timerFull;
	}

	void timerOperations(){
		
		player.onTick();
timer = timerFull;
if(awake){
if (!player.move(dir, 1)){
	goalReached();
	return;
}
if (player.getY() == (currRoom.getYScale() -1)){
goalReached();
return;
}

timer = timerFull; // delete
currRoom.advanceTime();
player.getTile().collide();
if (player.missedGuard()){
player.damage(1);
}
beat.Play();
}
	}

public void goalReached(){
	player.setBlindness(0f);
	awake = false;
}

	void controls(){
		if (Input.GetButton("left") && !Input.GetButton("right")){
			dir = -1;
			return;
		}
		if (Input.GetButton("right") && !Input.GetButton("left")){
			dir = 1;
			return;
		}
		dir = 0;
	}

}
