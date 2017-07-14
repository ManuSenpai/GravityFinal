using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GetBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("GRAVETY_TEST");
	}
}
