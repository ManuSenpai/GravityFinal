using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

    private Vector3 targetPos;
    SpriteRenderer spriteRender;

	// Use this for initialization

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.color = new Color(1f, 1f, 1f, 1f);
    }

	void FixedUpdate () {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        transform.position = targetPos;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            spriteRender = GetComponent<SpriteRenderer>();
            spriteRender.color = new Color(0.75f, 0.5f, 0.75f, 1f);
        }else
        {
            spriteRender = GetComponent<SpriteRenderer>();
            spriteRender.color = new Color(1f, 1f, 1f, 1f);
        }
        //transform.position = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}
}
