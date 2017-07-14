using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

    public Transform bala;
    public float velocidadBala;
    public float cadencia;
    public float distRango;
    public float velocidadArma;
    public bool isAttacking;
    public bool isAlreadyAttacking;

    public GameObject objetivo;

	// Use this for initialization
	void Start () {
        isAlreadyAttacking = false;
        isAttacking = false;
        velocidadBala = 800f;
        cadencia = 1f;
        velocidadArma = 2f;
        distRango = 1500f;
        if (objetivo == null)
            objetivo = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //Entra en el rango de ataque
        checkAttackStatus();
        if (Mathf.Abs(Vector2.Distance(transform.position, GlobalStats.currentStats.jugador.transform.position)) <= distRango){
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

    }

    void ShootAtTarget()
    {
        SoundManager.currentSounds.SpiderShoot.Play();
        Transform disparo = (Transform) Instantiate(bala, transform.position, transform.rotation);
        disparo.GetComponent<Rigidbody2D>().velocity = (objetivo.transform.position - transform.position).normalized * velocidadBala;
    }

    void checkAttackStatus()
    {
        if (isAttacking == true && isAlreadyAttacking == false)
        {
            InvokeRepeating("ShootAtTarget", velocidadArma, cadencia);
            isAlreadyAttacking = true;
        }
        else if(!isAttacking)
        {
            isAlreadyAttacking = false;
            CancelInvoke("ShootAtTarget");
        }
    }
}
