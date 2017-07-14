using UnityEngine;
using System.Collections;

public class Newt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(GlobalStats.currentStats.jugador.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(PrefabManager.currentPrefabs.mochilaLlena.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(PrefabManager.currentPrefabs.mochilaVacia.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(PrefabManager.currentPrefabs.newt.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

}
