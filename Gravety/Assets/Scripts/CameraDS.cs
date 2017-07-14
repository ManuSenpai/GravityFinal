using UnityEngine;
using System.Collections;

public class CameraDS : MonoBehaviour {

    public static CameraDS cam;

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "MenuPrincipal" || Application.loadedLevelName == "GameOver" || Application.loadedLevelName == "Credits")
            Destroy(this.gameObject);
		if (cam == null) {
			cam = this;
			if(Application.loadedLevelName != "GameOver" || Application.loadedLevelName != "Credits") DontDestroyOnLoad (transform.gameObject);
		} else {
			if (cam != this)
				Destroy (this.gameObject);
		}
    }
}
