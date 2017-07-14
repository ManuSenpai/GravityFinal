using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour {

    public int puntosVida;
    public int expAtDeath;
    public bool isHit;
    public GameObject DTprefab; // Es el texto que saldrá para recibir daño
    public float moveSpeed; // Velocidad de movimiento del enemigo.
    private float minDist = 1200f; // Mínima distacia con el jugador
    private float maxDist; // Máxima distancia con el jugador

    // @param: minDamage, maxDamage: Valores que referencian el daño máximo y mínimo que ejecutarán. Cada enemigo tendrá uno.
    public int minDamage, maxDamage;
    Rigidbody2D myRB;

	// Use this for initialization
	void Start () {
        // Debug.Log("Tengo " + puntosVida + " puntos de vida.");
        myRB = GetComponent<Rigidbody2D>();
        isHit = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Vector2.Distance(transform.position, GlobalStats.currentStats.jugador.transform.position));
        //Debug.Log(moveSpeed);
        //Debug.Log(player.transform.position);
        GravChange gravitacional = GetComponent<GravChange>();

        if (puntosVida <= 0)
        {
            PrefabManager.currentPrefabs.newtAgarrado.SetActive(false);
            PrefabManager.currentPrefabs.newt.GetComponent<Fire>().loaded = true;
            GlobalStats.currentStats.objetoActivo = GlobalStats.currentStats.jugador;
            GlobalStats.currentStats.jugador.GetComponent<GravChange>().gravitational = true;
            SoundManager.currentSounds.enemyDeath.Play();
            transform.localScale = new Vector2(0,0);
            GlobalStats.currentStats.jugador.GetComponent<Jugador>().subirExp(expAtDeath);
            Destroy(this);
        }

        if ((Mathf.Abs(Vector2.Distance(transform.position, GlobalStats.currentStats.jugador.transform.position)) <= minDist) || isHit)
        {
            try
            {
                this.GetComponent<Animator>().SetBool("Attack", true);
            }
            catch
            {
                Debug.Log("Esto es un pincho xD");
            }
            if (gravitacional.abajo || gravitacional.arriba)
            {
                if (transform.position.x - GlobalStats.currentStats.jugador.transform.position.x > 0)
                {
                    if (gravitacional.abajo)
                        this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    else if (gravitacional.arriba)
                        this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    myRB.velocity = new Vector2(-1.5f * moveSpeed, myRB.velocity.y);
                }
                else if (transform.position.x - GlobalStats.currentStats.jugador.transform.position.x != 0)
                {
                    if (gravitacional.abajo)
                        this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    else if (gravitacional.arriba)
                        this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    myRB.velocity = new Vector2(1.5f * moveSpeed, myRB.velocity.y);
                }
            }
            if (gravitacional.izq || gravitacional.der)
            {
                if (transform.position.y - GlobalStats.currentStats.jugador.transform.position.y > 0)
                {
                    if (gravitacional.izq)
                        this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    else if (gravitacional.der)
                        this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    myRB.velocity = new Vector2(myRB.velocity.x, -1.5f * moveSpeed);
                }
                else if (transform.position.y - GlobalStats.currentStats.jugador.transform.position.y != 0)
                {
                    if (gravitacional.izq)
                        this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    else if (gravitacional.der)
                        this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    myRB.velocity = new Vector2(myRB.velocity.x, 1.5f * moveSpeed);
                }
            }
        }

    }

    public void golpeado(int valorGolpe, bool isCrit)
    {
        isHit = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f, 0);
        puntosVida -= valorGolpe;
        if (!isCrit)
        {
            try
            {
                InitDT(valorGolpe.ToString()).GetComponent<Animator>().SetTrigger("Hit");
            }
            catch
            {
                Debug.Log("No pasa na de na");
            }
        }
        else
            try
            {
                InitDT(valorGolpe.ToString()).GetComponent<Animator>().SetTrigger("Crit");
            }
            catch
            {
                Debug.Log("No pasa na de na");
            }
        // Debug.Log("Tengo " + puntosVida + " puntos de vida.");
    }

    GameObject InitDT(string text)
    {
        GameObject temp = Instantiate(DTprefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.localPosition = DTprefab.transform.localPosition;
        tempRect.transform.localScale = DTprefab.transform.localScale;
        tempRect.transform.localRotation = DTprefab.transform.localRotation;

        temp.GetComponent<Text>().text = text;
        Destroy(temp.gameObject, 2);

        return temp;
    }

}
