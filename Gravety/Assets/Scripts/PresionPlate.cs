using UnityEngine;
using System.Collections;

public class PresionPlate : MonoBehaviour {

    public GameObject triggeredObject;
    private ArrayList objetosPresionando = new ArrayList();
    private int tallaObjetosPresionando;
    private bool isPressed;
	public int ActivationCode;
    public Sprite[] sprites;


    void Start()
    {
		isPressed = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public bool getIsPressed()
    {
        return isPressed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Proyectil")
        {
            objetosPresionando.Add(col.gameObject);
            tallaObjetosPresionando++;
            Debug.Log("Se ha añadido el objeto: " + col.gameObject.name + "; Talla de objetos: " + tallaObjetosPresionando);
            isPressed = true;
            triggeredObject.GetComponent<Activacion>().activar(ActivationCode);
            //this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        objetosPresionando.Remove(col.gameObject);
        tallaObjetosPresionando--;
        Debug.Log("Ha salido un objeto. Talla de objetos: " + tallaObjetosPresionando);
        if (tallaObjetosPresionando == 0)
        {
            isPressed = false;
            triggeredObject.GetComponent<Activacion>().desactivar();
            //this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }
}
