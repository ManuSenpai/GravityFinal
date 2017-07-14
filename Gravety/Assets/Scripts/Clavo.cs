using UnityEngine;
using System.Collections;

public class Clavo : MonoBehaviour {

    public float fireRate = 0; // Se va a disparar cada vez que le demos al ratón. No cadencia.
    private int damageMin, damageMax, damage;    // El daño que vamos a hacer cada vez que golpeemos con el clavo.
    private bool isCrit;
    public int bonus; // Bonus de daño por nivel.
	public GameObject trail;
	private TrailRenderer TR;

    Transform firePoint;

	void Start() {
		trail = Instantiate (PrefabManager.currentPrefabs.trail, transform.position, Quaternion.identity) as GameObject;
		TR = trail.GetComponent<TrailRenderer> ();
		trail.transform.parent = this.transform;
        damageMin = GlobalStats.currentStats.minDamage;
        damageMax = GlobalStats.currentStats.maxDamage;
		Physics2D.IgnoreCollision(GlobalStats.currentStats.jugador.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(PrefabManager.currentPrefabs.newt.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy2")
        {
            SoundManager.currentSounds.enemyHit.Play();
            damage = setRandomDamage();
            col.gameObject.GetComponent<Enemigo>().golpeado(damage, isCrit);
        }
		if (col.gameObject.tag == "Objeto") {
			col.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
        Destroy(this.gameObject);

    }

    public int setRandomDamage()
    {
        int dam = Random.Range(damageMin, damageMax);
        int randCrit = Random.Range(0, 100);
        if (randCrit > 80)
        {
            dam *= 3;
            // Debug.Log("DAÑO CRITICO");
            isCrit = true;

        }
        else isCrit = false;
        return dam;
    }

    public void bonusUp(int bonusValue)
    {
        GlobalStats.currentStats.minDamage += bonusValue;
        GlobalStats.currentStats.maxDamage += bonusValue;
    }

	public void OnDestroy()
	{
		TR.transform.parent = null;
		TR.autodestruct = true;
		TR = null;
	}

}
