using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;

public class PrefabManager : MonoBehaviour
{

    public static PrefabManager currentPrefabs;

    public GameObject clavo;
    public GameObject newt;
    public GameObject sideNewt;
    public GameObject mochilaLlena;
    public GameObject mochilaVacia;
    public GameObject idleEloise;
    public GameObject puntoDisparo;
    public GameObject puntoNewt;
	public GameObject brazoDisparo;
    public GameObject newtAgarrado;

    public Text HUD_texto_vida;
    public Text HUD_texto_nivel;
    public Text HUD_texto_nails;
    public GameObject HUD_barra_vida;
    public GameObject HUD_barra_vida_punta;
    public GameObject HUD_barra_vida_fondo;
    public GameObject HUD_barra_exp;
    public GameObject HUD_barra_exp_fondo;
	public GameObject trail;
	public GameObject esferaExp;

    // Use this for initialization

	void Awake()
	{
		Cursor.visible = false;
		if (currentPrefabs == null)
		{
			currentPrefabs = this;
			//Aunque cambie de mapa, esto no se va a eliminar
			if ( Application.loadedLevelName != "GameOver" || Application.loadedLevelName != "Credits") DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			if (currentPrefabs != this)
				Destroy(this.gameObject);
		}
	}

    void OnLevelWasLoaded()
    {

        if (Application.loadedLevelName == "MenuPrincipal" || Application.loadedLevelName == "GameOver" || Application.loadedLevelName == "Credits")
            Destroy(this.gameObject);
        Cursor.visible = false;
        // Inicializamos al personaje como que está en el suelo.

        if (currentPrefabs == null)
        {
            currentPrefabs = this;
            //Aunque cambie de mapa, esto no se va a eliminar
			if ( Application.loadedLevelName != "GameOver" || Application.loadedLevelName != "Credits") DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (currentPrefabs != this)
                Destroy(this.gameObject);
        }
    }
		
}
