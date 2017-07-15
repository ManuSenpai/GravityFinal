using UnityEngine;
using System.Collections;

/* Esta clase se empleará para gestionar las colisiones del jugador con los diferentes elementos del entorno.
 * También gestionará los coleccionables y elementos con los que el personaje se encontrará. **/

public class PlayerCollisions : MonoBehaviour {

    public AudioSource sonido;
    public float pullx, pully;

	void Start () {
        pullx = 500f;
        pully = 250f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {

		if (col.gameObject.tag == "DamageTrap")
		{
			SoundManager.currentSounds.EloiseHit.Play();
			int direction = comprobarDireccionColision(this.gameObject, col.gameObject);
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(pullx * direction, pully);
			int damageHit = col.gameObject.GetComponent<BoobyTraps>().damage;
			//int damageHit = col.gameObject.GetComponent<Enemigo>().minDamage;
			this.GetComponent<Jugador>().getDamage(damageHit);
			GlobalStats.currentStats.jugador.GetComponent<Animator>().SetBool("isHit", true);
		}

        // Nos chocamos contra un enemigo
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy2")
        {
            SoundManager.currentSounds.EloiseHit.Play();
            int direction = comprobarDireccionColision(this.gameObject, col.gameObject);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(pullx * direction, pully);
            int damageHit = Random.Range(col.gameObject.GetComponent<Enemigo>().minDamage, col.gameObject.GetComponent<Enemigo>().maxDamage);
            //int damageHit = col.gameObject.GetComponent<Enemigo>().minDamage;
            this.GetComponent<Jugador>().getDamage(damageHit);
            GlobalStats.currentStats.jugador.GetComponent<Animator>().SetBool("isHit", true);
        }

        if (col.gameObject.tag == "enemyBullet")
        {
            SoundManager.currentSounds.EloiseHit.Play();
            int direction = comprobarDireccionColision(this.gameObject, col.gameObject);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(pullx * direction, pully);
            int damageHit = Random.Range(col.gameObject.GetComponent<enemyBulletDestroy>().minDamage, col.gameObject.GetComponent<enemyBulletDestroy>().maxDamage);
            this.GetComponent<Jugador>().getDamage(damageHit);
            GlobalStats.currentStats.jugador.GetComponent<Animator>().SetBool("isHit", true);
        }

        // Cogemos un objeto de curación
        if (col.gameObject.tag == "Lifebox")
        {
            int lifeAmount = col.gameObject.GetComponent<LifeBox>().lifeAmount;
            if (GlobalStats.currentStats.player_max_health - lifeAmount < GlobalStats.currentStats.player_current_health)
                GlobalStats.currentStats.player_current_health = GlobalStats.currentStats.player_max_health;
            else
                GlobalStats.currentStats.player_current_health += lifeAmount;
            col.gameObject.transform.localScale = new Vector2(0, 0);
        }

		if (col.gameObject.tag == "Scenario" || col.gameObject.tag == "Objeto" || col.gameObject.tag == "Puerta" || col.gameObject.tag == "Placa")
        {
            if(!GlobalStats.currentStats.jugador.GetComponent<Animator>().GetBool("Ground"))
            {
                GlobalStats.currentStats.jugador.GetComponent<Animator>().SetBool("Ground", true);
            }
            if(GlobalStats.currentStats.jugador.GetComponent<Animator>().GetBool("isHit"))
            {
                GlobalStats.currentStats.jugador.GetComponent<Animator>().SetBool("isHit", false);
            }
        }
    }

    int comprobarDireccionColision(GameObject jugador, GameObject enemigo)
    {
        if ((jugador.transform.position.x - enemigo.transform.position.x) < 0)
            return -1;
        else
            return 1;
    }
}
