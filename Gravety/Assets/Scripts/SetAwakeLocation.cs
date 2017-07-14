using UnityEngine;
using System.Collections;

public class SetAwakeLocation : MonoBehaviour {

	// Use this for initialization
	void OnLevelWasLoaded()
    {
		GlobalStats.currentStats.jugador.transform.position = GlobalStats.currentStats.awakeLocation;
    }
}
