using UnityEngine;
using System.Collections;

public class BlockSolver: MonoBehaviour
{

	private bool isBlocked, timerOn;
	private GameObject blocker;
	private float thisInstant;

	void Start()
	{
		isBlocked = false;
		timerOn = false;
		blocker = null;
	}

	void Update()
	{
		if (isBlocked && !timerOn) {
			thisInstant = Time.time;
			timerOn = true;
			isBlocked = false;
		}

		if (timerOn && Time.time < thisInstant + 5) {
			Physics2D.IgnoreCollision (blocker.gameObject.GetComponent<Collider2D> (), this.gameObject.GetComponent<Collider2D> (), true);
		} else if(blocker != null){
			Physics2D.IgnoreCollision (blocker.gameObject.GetComponent<Collider2D> (), this.gameObject.GetComponent<Collider2D> (), false);
			timerOn = false;
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Puerta" && Mathf.Abs(col.gameObject.GetComponent<Rigidbody2D> ().velocity.y) < 350f) {
			Debug.Log ("( " + Mathf.Abs (col.gameObject.GetComponent<Rigidbody2D> ().velocity.x) + " , " + Mathf.Abs (col.gameObject.GetComponent<Rigidbody2D> ().velocity.y) + " )"); 
			isBlocked = true;
			blocker = col.gameObject;
		}
	}
		
}


