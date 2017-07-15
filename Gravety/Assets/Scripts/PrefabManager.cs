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
    public GameObject puntoDisparo;
    public GameObject puntoNewt;
	public GameObject brazoDisparo;
    public GameObject newtAgarrado;

    public Text HUD_texto_vida;
    public Text HUD_texto_nivel;
    public GameObject HUD_barra_vida;
    public GameObject HUD_barra_vida_punta;
    public GameObject HUD_barra_vida_fondo;
    public GameObject HUD_barra_exp;
    public GameObject HUD_barra_exp_fondo;
	public GameObject trail;
	public GameObject esferaExp;

    private GameObject jugador;
    private GameObject HUD;
    // Use this for initialization

	void Awake()
	{
        jugador = GameObject.FindGameObjectWithTag("Player");
        HUD = GameObject.FindGameObjectWithTag("HUD");
        newt = jugador.transform.FindChild("newt").gameObject;
        mochilaLlena = jugador.transform.FindChild("mochila").gameObject;
        mochilaVacia = jugador.transform.FindChild("mochila-vacia").gameObject;
        brazoDisparo = jugador.transform.FindChild("Hombro").gameObject.transform.FindChild("brazoPistola").gameObject;
        puntoDisparo = brazoDisparo.transform.FindChild("PuntoDisparo").gameObject;
        puntoNewt = brazoDisparo.transform.FindChild("PuntoNewt").gameObject;
        newtAgarrado = jugador.transform.FindChild("newtCogido").gameObject;
        HUD_texto_vida = HUD.transform.FindChild("textoVida").GetComponent<Text>();
        HUD_texto_nivel = HUD.transform.FindChild("Nivel").GetComponent<Text>();
        HUD_barra_vida = HUD.transform.FindChild("Vida").gameObject;
        HUD_barra_vida_punta = HUD.transform.FindChild("HUD_BARRA_DER").gameObject;
        HUD_barra_vida_fondo = HUD.transform.FindChild("Fondo").gameObject;
        HUD_barra_exp = HUD.transform.FindChild("Exp_Barra").gameObject;
        HUD_barra_exp_fondo = HUD.transform.FindChild("Exp_Fondo").gameObject;
        esferaExp = HUD.transform.FindChild("Circle").gameObject;

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
