using UnityEngine;
using System.Collections;

public class MenuPausa : MonoBehaviour {

    public GameObject canvasPausa;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //Pausamos el juego
            canvasPausa.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
	
	}

    public void OnClick()
    {
        Time.timeScale = 1f;
    }
}
