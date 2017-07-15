//@author: Manuel Gavilan Ortiz
//@version: 1.0 
using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    private GameObject Queco;
    public bool loaded;
    public GameObject objetoActivo;
    public int offsetRotacion;
	public Material[] materials;
	public Sprite[] sprites;

    private float velBola;
    private float velClavo;
    private GameObject newtGrabbed;
    private static float mediumCharge = 1.5f;
    private static float fullCharge = 4f;

	// Use this for initialization
	void Start () {
        Queco = GlobalStats.currentStats.jugador;
        PrefabManager.currentPrefabs.newt.transform.localScale = Vector2.zero;
        PrefabManager.currentPrefabs.mochilaLlena.transform.localScale = new Vector2(0.7680524f, 0.7680524f);
        PrefabManager.currentPrefabs.mochilaVacia.transform.localScale = Vector2.zero;
        loaded = true;
        GlobalStats.currentStats.objetoActivo = GlobalStats.currentStats.jugador;
        velBola = 3000f;
        velClavo = 4000f;
        newtGrabbed = PrefabManager.currentPrefabs.newtAgarrado;
        newtGrabbed.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        // Disparamos a Newt. Primero apuntamos y después disparamos.
        if(newtGrabbed.activeSelf)
        {
            newtGrabbed.transform.position = GlobalStats.currentStats.objetoActivo.transform.position;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PrefabManager.currentPrefabs.mochilaLlena.transform.localScale = new Vector2(0f, 0f);
            PrefabManager.currentPrefabs.mochilaVacia.transform.localScale = new Vector2(0.7680524f, 0.7680524f);
            PrefabManager.currentPrefabs.newt.transform.localScale = new Vector2(0.5f, 0.5f);
			PrefabManager.currentPrefabs.brazoDisparo.GetComponent<SpriteRenderer> ().sprite = sprites [1];
            PrefabManager.currentPrefabs.newt.transform.position = PrefabManager.currentPrefabs.puntoNewt.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                if (PrefabManager.currentPrefabs.newt.GetComponent<Fire>().loaded != false)
                {
                    GameObject newt = (GameObject) Instantiate(PrefabManager.currentPrefabs.newt, Queco.transform.localPosition, Quaternion.identity);
                    newt.transform.localScale = new Vector2(15f, 15f);
                    newt.AddComponent<Rigidbody2D>();
                    Rigidbody2D newtRB = newt.GetComponent<Rigidbody2D>();
                    newtRB.transform.position = new Vector2(PrefabManager.currentPrefabs.puntoNewt.transform.position.x, PrefabManager.currentPrefabs.puntoNewt.transform.position.y);
                    transform.rotation = Quaternion.identity;
                    disparar(newtRB, velBola);
                    loaded = false;
                }
            }
        }
        else
        {
            if (PrefabManager.currentPrefabs.newt.GetComponent<Fire>().loaded)
            {
                PrefabManager.currentPrefabs.mochilaLlena.transform.localScale = new Vector2(0.7680524f, 0.7680524f);
                PrefabManager.currentPrefabs.mochilaVacia.transform.localScale = Vector2.zero;
            }
			PrefabManager.currentPrefabs.brazoDisparo.GetComponent<SpriteRenderer> ().sprite = sprites [0];
            PrefabManager.currentPrefabs.newt.transform.localScale = Vector2.zero;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject clavo = (GameObject) Instantiate(PrefabManager.currentPrefabs.clavo, Queco.transform.localPosition, Quaternion.identity);
                clavo.transform.localScale = new Vector2(10f, 10f);
                Rigidbody2D clavoRB = clavo.GetComponent<Rigidbody2D>();
                //clavoRB.transform.position = new Vector2(Queco.transform.position.x, Queco.transform.position.y);
                clavoRB.transform.position = new Vector2(PrefabManager.currentPrefabs.puntoDisparo.transform.position.x, PrefabManager.currentPrefabs.puntoDisparo.transform.position.y);
                transform.rotation = Quaternion.identity;
                dispararClavo(clavoRB, velClavo);
            }
        }
        // Disparo principal con pistola de clavos.

       

        // Recargamos a Newt en su posición
        if (Input.GetKeyDown(KeyCode.R)){
            if (GlobalStats.currentStats.objetoActivo != GlobalStats.currentStats.jugador)
            {
                GlobalStats.currentStats.setNewtFired(false);
                newtGrabbed.SetActive(false);
                GlobalStats.currentStats.objetoActivo.GetComponent<GravChange>().gravitational = false;
                if(PrefabManager.currentPrefabs.newt == null)
                    PrefabManager.currentPrefabs.newt = (GameObject)Instantiate(PrefabManager.currentPrefabs.sideNewt, Queco.transform.localPosition, Quaternion.identity);
                PrefabManager.currentPrefabs.newt.GetComponent<Fire>().loaded = true;
				GlobalStats.currentStats.objetoActivo.GetComponent<Renderer> ().material = materials [1];
                GlobalStats.currentStats.objetoActivo = Queco;
                GlobalStats.currentStats.jugador.GetComponent<GravChange>().gravitational = true;
            }
        }
	}

    void disparar(Rigidbody2D proyectil, float velocidad)
    {
        GlobalStats.currentStats.setNewtFired(true);
        Vector3 posPantalla = Camera.main.WorldToScreenPoint(PrefabManager.currentPrefabs.puntoNewt.transform.position);
        Vector3 direccion = (Input.mousePosition - posPantalla).normalized;
        if (GlobalStats.currentStats.jugador.transform.localScale.x < 0)
            proyectil.velocity = transform.TransformDirection(new Vector2(-direccion.x, direccion.y) * velocidad);
        else
            proyectil.velocity = transform.TransformDirection(new Vector2(direccion.x, direccion.y) * velocidad);
    }

    void dispararClavo(Rigidbody2D proyectil, float velocidad)
    {
        SoundManager.currentSounds.nailShoot.Play();
        Vector3 posPantalla = Camera.main.WorldToScreenPoint(PrefabManager.currentPrefabs.puntoDisparo.transform.position);
        Vector3 direccion;
        if (!GlobalStats.currentStats.GetMaxAngleReached())
        {
            direccion = (Input.mousePosition - posPantalla).normalized;
        }
        else
        {
            float rotacion = GlobalStats.currentStats.GetCurrentArmRotation();
            if (rotacion > 0.95 && rotacion < 1.05) direccion = Vector3.up;
            else if (rotacion > 0.65 && rotacion < 0.75) direccion = Vector3.right;
            else if (rotacion < -0.65 && rotacion > -0.75) direccion = Vector3.left;
            else direccion = Vector3.down;
        }

        proyectil.GetComponent<Clavo>().setRandomDamage();
        //proyectil.velocity = transform.TransformDirection(new Vector2(direccion.x, direccion.y) * velocidad);
        if (GlobalStats.currentStats.jugador.transform.localScale.x < 0)
            proyectil.velocity = transform.TransformDirection(new Vector2(-direccion.x, direccion.y) * velocidad);
        else
            proyectil.velocity = transform.TransformDirection(new Vector2(direccion.x, direccion.y) * velocidad);
        //proyectil.AddForce(direccion * velocidad, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Scenario" || col.gameObject.tag == "Nails" || col.gameObject.tag == "Lifebox")
        {
            if(GlobalStats.currentStats.getNewtIsFired())
            {
                GlobalStats.currentStats.setNewtFired(false);
                Debug.Log(col.gameObject.tag);
                PrefabManager.currentPrefabs.newt.GetComponent<Fire>().loaded = true;
                Destroy(this.gameObject);
            }
        }
		else if (col.gameObject.tag == "Objeto" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy2" || col.gameObject.tag == "Puerta")
        {
            if (GlobalStats.currentStats.getNewtIsFired())
            {
                GlobalStats.currentStats.objetoActivo.GetComponent<Renderer>().material = materials[1];
                GlobalStats.currentStats.objetoActivo = col.gameObject;
                GlobalStats.currentStats.objetoActivo.GetComponent<Renderer>().material = materials[0];
                newtGrabbed.SetActive(true);
                newtGrabbed.transform.position = GlobalStats.currentStats.objetoActivo.transform.position;
                GlobalStats.currentStats.objetoActivo.GetComponent<GravChange>().gravitational = true;
                GlobalStats.currentStats.jugador.GetComponent<GravChange>().gravitational = false;
                PrefabManager.currentPrefabs.newt.GetComponent<Fire>().loaded = false;
                Destroy(this.gameObject);
            }
        }
    }

}
