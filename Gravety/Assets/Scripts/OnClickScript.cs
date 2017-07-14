using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickScript : MonoBehaviour {

    public string levelChange;

	public void OnClick()
    {
        SceneManager.LoadScene(levelChange);
    }
}
