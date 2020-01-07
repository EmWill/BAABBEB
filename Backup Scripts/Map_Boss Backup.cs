using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Boss : MonoBehaviour {
	public GameObject mineFab;
public GameObject playerFab;
	private GameObject mine;
	private Mines mineScript;
	private Room currRoom;

	private GameObject playerObj;
    private float timer = 2f;	
	private Player player;

	// Use this for initialization
	void Start () {
		mine = Instantiate(mineFab);
		mineScript = mine.GetComponent(typeof(Mines)) as Mines;
		playerObj = Instantiate(playerFab);
		player = new Player(this); 
		player.setCoords(6, 7);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0)
		timerOperations();

		if (Input.GetButtonDown("Submit")){
			mineScript.generate();
		}
	}

	void timerOperations(){
timer = 2f;
	}
}
