using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.U)) {
			GlobalStats.currentStats.player_nails += 30;
		}
	}
}
