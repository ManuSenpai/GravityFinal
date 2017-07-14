using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public float speed;
	public float jumpSpeed;

    public bool isFacingRight;
    public bool isFacingDown;
    public SpriteRenderer mySpriteRenderer;
    private SpriteRenderer mochilaLlenaSR;
    private SpriteRenderer mochilaVaciaSR;
    public Vector2 jumDir = Vector2.zero;

	// Use this for initialization
	void Start () {
        isFacingRight = false;
        isFacingDown = false;
        mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        mochilaVaciaSR = PrefabManager.currentPrefabs.mochilaVacia.GetComponent<SpriteRenderer>();
        mochilaLlenaSR = PrefabManager.currentPrefabs.mochilaLlena.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		speed = 350f;
		jumpSpeed = 750f;

		GravChange gravitacional = GetComponent<GravChange> ();
		//Obtenemos el Rigid Body del Queco para que al saltar le podamos aplicar fuerzas.
		Rigidbody2D QuecoRig = this.gameObject.GetComponent<Rigidbody2D> ();
        //Vamos a obtener la velocidad del personaje en todo momento para controlar la velocidad del salto.
        float airVelocity = calcularVelocidadEnAire(gravitacional, QuecoRig);
        float groundVelocity = calcularVelocidadEnTierra(gravitacional, QuecoRig);
        //Asignamos dichas velocidades a los valores que controlan las animaciones
        GlobalStats.currentStats.jugador.GetComponent<Animator>().SetFloat("SpeedX", airVelocity);
        GlobalStats.currentStats.jugador.GetComponent<Animator>().SetFloat("SpeedY", groundVelocity);

        if (Input.GetKey(KeyCode.D))
        {
			if(gravitacional.abajo || gravitacional.arriba){
                QuecoRig.velocity = new Vector2(speed, QuecoRig.velocity.y);
                if (gravitacional.abajo) isFacingRight = false;
                else isFacingRight = true;
			}
            if (gravitacional.abajo)
                this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else if (gravitacional.arriba)
                this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);

        }

        if (Input.GetKey (KeyCode.A)) {
			if(gravitacional.abajo || gravitacional.arriba){
                QuecoRig.velocity = new Vector2(-speed, QuecoRig.velocity.y);
                if (gravitacional.abajo) isFacingRight = true;
                else isFacingRight = false;
			}
            if (gravitacional.abajo)
                this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else if (gravitacional.arriba)
                this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

		if(Input.GetKey (KeyCode.W)){
			if(gravitacional.izq || gravitacional.der)
			{
                QuecoRig.velocity = new Vector2(QuecoRig.velocity.x, speed);
                if (gravitacional.der) isFacingRight = false;
                else isFacingRight = true;
			}
            if (gravitacional.izq)
                this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else if (gravitacional.der)
                this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

		if(Input.GetKey (KeyCode.S)){
			if(gravitacional.izq || gravitacional.der)
			{
                QuecoRig.velocity = new Vector2(QuecoRig.velocity.x, -speed);
                if (gravitacional.izq) isFacingRight = false;
                else isFacingRight = true;
			}
            if (gravitacional.der)
                this.gameObject.transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else if (gravitacional.izq)
                this.gameObject.transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (Input.GetKeyDown (KeyCode.Space)) {
            if(GlobalStats.currentStats.jugador.GetComponent<Animator>().GetBool("Ground") == true)
            {
                if (gravitacional.abajo)
                    QuecoRig.velocity = new Vector2(0, jumpSpeed);
                if (gravitacional.arriba)
                    QuecoRig.velocity = new Vector2(0, -jumpSpeed);
                if (gravitacional.izq)
                    QuecoRig.velocity = new Vector2(jumpSpeed, 0);
                if (gravitacional.der)
                    QuecoRig.velocity = new Vector2(-jumpSpeed, 0);
                GlobalStats.currentStats.jugador.GetComponent<Animator>().SetBool("Ground", false);
            }
		}

        if (QuecoRig.velocity == Vector2.zero)
        {
            QuecoRig.GetComponent<Animator>().SetBool("isIdle", true);
        }
    }

    /*
     * Esto lo sustituiremos después por un Vector2 y lo devolveremos, dando un SpeedX y SpeedY
     * */

    // Esta función la vamos a calcular para saber cuándo estamos al inicio del salto o al final
    float calcularVelocidadEnAire(GravChange gravitacional, Rigidbody2D QuecoRig)
    {
        if (gravitacional.abajo)
            return QuecoRig.velocity.y;
        else if (gravitacional.arriba)
            return -QuecoRig.velocity.y;
        else if (gravitacional.izq)
            return QuecoRig.velocity.x;
        else
            return -QuecoRig.velocity.x;
    }

    // Esta función indicará qué velocidad tenemos en tierra
    float calcularVelocidadEnTierra(GravChange gravitacional, Rigidbody2D QuecoRig)
    {
        if (gravitacional.abajo)
            return Mathf.Abs(QuecoRig.velocity.x);
        else if (gravitacional.arriba)
            return Mathf.Abs(QuecoRig.velocity.x);
        else if (gravitacional.izq)
            return Mathf.Abs(QuecoRig.velocity.y);
        else
            return Mathf.Abs(QuecoRig.velocity.y);
    }
}
