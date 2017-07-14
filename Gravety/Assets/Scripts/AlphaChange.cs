using UnityEngine;
using System.Collections;

public class AlphaChange : MonoBehaviour {

    public GameObject jugador;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0f, 0f);
        }
    }
		
}
