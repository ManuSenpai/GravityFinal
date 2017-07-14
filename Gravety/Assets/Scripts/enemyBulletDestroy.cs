using UnityEngine;
using System.Collections;

public class enemyBulletDestroy : MonoBehaviour {

    public int minDamage;
    public int maxDamage;

	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D col)
    {
		if(col.gameObject.tag != "Player")
        {
			if (col.gameObject.tag != "Enemy2")
				Destroy (this.gameObject);
			else
				Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D> (), this.gameObject.GetComponent<Collider2D> (), true);
        }
		else
            Destroy(this.gameObject, 0.1f);
    }
}
