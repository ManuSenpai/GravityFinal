using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    // Ponemos un String para poder cargar un nivel según el nombre
    public string nivelACambiar;
    public Vector2 nextLocation;
    // Esto nos gestionará que lo hagamos por colisión
    public GameObject colisionador;

    void Start()
    {
        if (colisionador == null)
        {
            colisionador = GameObject.FindGameObjectWithTag("Player");
        }
    }

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player")
        {
            Application.LoadLevel(nivelACambiar);
            GlobalStats.currentStats.awakeLocation = nextLocation;
        }
	}
}
