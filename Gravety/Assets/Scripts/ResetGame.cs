using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {

	// Use this for initialization
    void OnGameStart()
    {
        SceneManager.LoadScene("GRAVETY_TEST");
    }

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("GRAVETY_TEST");
	}
}
