using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tuple {
public int xTup, yTup;

	public static bool operator == (Tuple tup1, Tuple tup2){
		return tup1.Equals(tup2);
	}

	public static bool operator != (Tuple tup1, Tuple tup2){
		return !tup1.Equals(tup2);
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

	public Tuple(int x, int y){
		xTup = x; 
		yTup = y;
	}
}
