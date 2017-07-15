using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour {

	public static Jugador player;

    public int nivel, exp, expNextLevel, current_health, max_health;
    public Text contadorClavos;
    public Text contadorNivel;
    public GameObject ptPrefab; //Texto de info que aparece en el jugador.
    public GameObject proyectil; //Accedemos al clavo para que podamos cambiarle el daño.

    // GameObjects de las barras de vida;
    public GameObject puntosVida;

	void Start()
	{
		if (player == null) {
			player = this;
			if (Application.loadedLevelName != "GameOver" || Application.loadedLevelName != "Credits")
				DontDestroyOnLoad (transform.gameObject);
		} else {
			if (player != this) {
				Destroy (this.gameObject);
			}
		}
	}

    void OnLevelWasLoaded()
    {
        if ((Application.loadedLevelName == "MenuPrincipal" && player != null) || Application.loadedLevelName == "GameOver" || Application.loadedLevelName == "Credits")
            Destroy(this.gameObject);
        if (player == null)
        {
            player = this;
            if (Application.loadedLevelName != "GameOver" || Application.loadedLevelName != "Credits")
                DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            if (player != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        PrefabManager.currentPrefabs.HUD_barra_vida.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalStats.currentStats.player_current_health * 2, PrefabManager.currentPrefabs.HUD_barra_vida.GetComponent<RectTransform>().sizeDelta.y);
        PrefabManager.currentPrefabs.HUD_texto_nivel.text = GlobalStats.currentStats.player_level.ToString();
        PrefabManager.currentPrefabs.HUD_texto_vida.text = GlobalStats.currentStats.player_current_health.ToString() + " / " + GlobalStats.currentStats.player_max_health;
    }

    public void subirExp(int expSube)
    {
        GlobalStats.currentStats.player_current_exp += expSube;
        if (GlobalStats.currentStats.player_current_exp >= GlobalStats.currentStats.player_exp_next_level)
        {
            subirNivel();

        }
        else
        {
            float newWidth = (PrefabManager.currentPrefabs.HUD_barra_exp_fondo.GetComponent<RectTransform>().sizeDelta.x / GlobalStats.currentStats.player_exp_next_level) * GlobalStats.currentStats.player_current_exp;
            PrefabManager.currentPrefabs.HUD_barra_exp.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, PrefabManager.currentPrefabs.HUD_barra_exp_fondo.GetComponent<RectTransform>().sizeDelta.y);
            string mensaje = "+ " + expSube.ToString() + " exp";
            InitPT(mensaje);
        }
    }

    void subirNivel()
    {
		PrefabManager.currentPrefabs.esferaExp.GetComponent<Animator> ().SetTrigger ("levUp");
        GlobalStats.currentStats.player_level += 1;
        GlobalStats.currentStats.player_max_health += 15;
        GlobalStats.currentStats.player_current_health = GlobalStats.currentStats.player_max_health;
       

        string mensaje = "LEVEL UP";
        InitPT(mensaje);

        int resto = GlobalStats.currentStats.player_current_exp - GlobalStats.currentStats.player_exp_next_level;
        GlobalStats.currentStats.player_current_exp = resto;
        GlobalStats.currentStats.player_exp_next_level = (int)Mathf.Floor(GlobalStats.currentStats.player_exp_next_level * 1.8f);
        float newWidth = (PrefabManager.currentPrefabs.HUD_barra_exp_fondo.GetComponent<RectTransform>().sizeDelta.x / GlobalStats.currentStats.player_exp_next_level) * GlobalStats.currentStats.player_current_exp;
        PrefabManager.currentPrefabs.HUD_barra_exp.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, PrefabManager.currentPrefabs.HUD_barra_exp_fondo.GetComponent<RectTransform>().sizeDelta.y);


        // Aumentamos el tamaño de las cajas.
        PrefabManager.currentPrefabs.HUD_barra_vida_fondo.GetComponent<RectTransform>().sizeDelta = new Vector2(PrefabManager.currentPrefabs.HUD_barra_vida_fondo.GetComponent<RectTransform>().sizeDelta.x + 30, PrefabManager.currentPrefabs.HUD_barra_vida_fondo.GetComponent<RectTransform>().sizeDelta.y);
        PrefabManager.currentPrefabs.HUD_barra_vida.GetComponent<RectTransform>().sizeDelta = new Vector2(PrefabManager.currentPrefabs.HUD_barra_vida_fondo.GetComponent<RectTransform>().sizeDelta.x, PrefabManager.currentPrefabs.HUD_barra_vida_fondo.GetComponent<RectTransform>().sizeDelta.y);
        Vector2 newPosition = new Vector2(PrefabManager.currentPrefabs.HUD_barra_vida_punta.transform.position.x + 30, PrefabManager.currentPrefabs.HUD_barra_vida_punta.transform.position.y);
        PrefabManager.currentPrefabs.HUD_barra_vida_punta.transform.position = newPosition;
        

        // Aumentamos el daño efectuado con el clavo;
        proyectil.GetComponent<Clavo>().bonusUp(4);
    }

    void InitPT(string text)
    {
        GameObject temp = Instantiate(ptPrefab) as GameObject;
        temp.GetComponent<Text>().text = text;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.FindChild("PlayerCanvas"));
        //temp.transform.localScale = new Vector2(0, 0);
        tempRect.transform.localPosition = ptPrefab.transform.localPosition;
        tempRect.transform.localScale = ptPrefab.transform.localPosition;
        tempRect.transform.localRotation = ptPrefab.transform.localRotation;
        //temp.GetComponent<Animator>().SetTrigger("ExpUp");
        Destroy(temp.gameObject, 2);
    }

    public void getDamage(int damage)
    {
        GlobalStats.currentStats.player_current_health -= damage;
        PrefabManager.currentPrefabs.HUD_barra_vida.GetComponent<RectTransform>().sizeDelta = new Vector2(PrefabManager.currentPrefabs.HUD_barra_vida.GetComponent<RectTransform>().sizeDelta.x - (damage*2), PrefabManager.currentPrefabs.HUD_barra_vida.GetComponent<RectTransform>().sizeDelta.y);
        if (GlobalStats.currentStats.player_current_health <= 0)
        {
			Camera.main.orthographicSize = 240.4f;
            SceneManager.LoadScene("GameOver");
        }
    }
}
