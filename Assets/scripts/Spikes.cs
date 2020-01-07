using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Entity {


public override void onTick(){

}

public override void playerCollision(){
player.damage(1);
}
	// Use this for initialization
}
