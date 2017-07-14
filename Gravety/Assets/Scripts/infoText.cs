using UnityEngine;
using System.Collections;

public class infoText : MonoBehaviour {

    /* Este código sirve para hacer aparecer o desaparecer el texto bajo ciertas condiciones **/
    public bool isRead;

    //Esto va a gestionar que no estemos llamando continuamente a los triggers.
    public bool itsIn;
    public int textMargin;

    void Update()
    {

        if((GlobalStats.currentStats.jugador.transform.position.x >= (this.gameObject.GetComponent<RectTransform>().position.x - textMargin - this.gameObject.GetComponent<RectTransform>().sizeDelta.x * this.gameObject.GetComponent<RectTransform>().localScale.x / 2)) &&
            (GlobalStats.currentStats.jugador.transform.position.x < (this.gameObject.GetComponent<RectTransform>().position.x + textMargin + this.gameObject.GetComponent<RectTransform>().sizeDelta.x * this.gameObject.GetComponent<RectTransform>().localScale.x / 2)))
        {
            if(!itsIn)
            {
                itsIn = true;
                //Debug.Log("It's in");
                this.gameObject.GetComponent<Animator>().SetTrigger("enterText");
            }
        }
        else
        {
            if(itsIn)
            {
                itsIn = false;
                //Debug.Log("It's out");
                this.gameObject.GetComponent<Animator>().SetTrigger("exitText");
            }
        }

        //Si salgo por la derecha o por la izquierda
        //if ((GlobalStats.currentStats.jugador.transform.position.x ==
        //    (this.gameObject.transform.position.x - this.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2) &&
        //    GlobalStats.currentStats.jugador.GetComponent<Rigidbody2D>().velocity.x < 0) ||
        //    (GlobalStats.currentStats.jugador.transform.position.x ==
        //    (this.gameObject.transform.position.x + this.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2) &&
        //    GlobalStats.currentStats.jugador.GetComponent<Rigidbody2D>().velocity.x > 0))
        //{
        //    Debug.Log("I'm getting outta here");
        //    this.gameObject.GetComponent<Animator>().SetTrigger("exitText");
        //}
    }
}
