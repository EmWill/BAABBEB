using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PotentialTile {
public int xTile, yTile, entity;

	public static bool operator == (PotentialTile tile1, PotentialTile tile2){
		return tile1.Equals(tile2);
	}

	public static bool operator != (PotentialTile tile1, PotentialTile tile2){
		return !tile1.Equals(tile2);
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

	public PotentialTile(int x, int y, int ent){
		xTile = x; 
		yTile = y;
		entity = ent;
	}
}
