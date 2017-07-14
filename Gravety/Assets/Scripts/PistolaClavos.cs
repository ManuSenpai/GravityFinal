using UnityEngine;
using System.Collections;

public class PistolaClavos : MonoBehaviour {
    public float cadencia = 0; // Cadencia de disparo. 0 = dispararemos cada vez que hagamos click.
    public float danyoInfligido = 10;
    public LayerMask areaImpacto; // Esta es la capa en donde el raycast va a golpear.

	
	// Update is called once per frame
	void Update () {
        if (cadencia == 0)
        {
            if (!Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                disparar();
            }
        }
	
	}

    void disparar()
    {
        Debug.Log("BANG!");
    }
}
