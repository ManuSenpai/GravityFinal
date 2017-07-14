using UnityEngine;
using System.Collections;

public class Activacion : MonoBehaviour {

    public bool isActivated;
    public GameObject trigger; 
	private ConstantForce2D miCF;
	public int deactivationCode;

	// Use this for initialization
	void Start () {
		miCF = this.gameObject.GetComponent<ConstantForce2D> ();
		activar (deactivationCode);
        isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void activar(int activationCode)
	{
		//El switch de opciones de activación.
		switch (activationCode) {
		case 1:
			//Mover objeto hacia arriba.
			miCF.force = new Vector2 (0, 10000f);
			break;
		case 2:
			//Mover objeto hacia abajo.
			miCF.force = new Vector2 (0, -10000f);
			break;
		case 3:
			//Mover objeto hacia la izquierda;
			miCF.force = new Vector2 (-10000f, 0);
			break;
		case 4:
			//Mover objeto hacia la derecha:
			miCF.force = new Vector2 (10000f, 0);
			break;

		}
	}

	public void desactivar()
	{
		activar (deactivationCode);
	}
}
