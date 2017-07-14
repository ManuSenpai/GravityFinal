using UnityEngine;
using System.Collections;

public class FollowCharacter : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    //Esto establece que la cámara tiene boundaries.
    public bool bounds;
    public Vector3 maxCameraPosition;
    public Vector3 minCameraPosition;

	// Use this for initialization
	void Start () {
		player = GlobalStats.currentStats.jugador;
		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
        offset = transform.position - player.transform.position;
        Camera.main.orthographicSize = GlobalStats.currentStats.getCameraSize(Application.loadedLevelName);
        maxCameraPosition = GlobalStats.currentStats.obtenerMaxCameraPosition(Application.loadedLevelName);
        minCameraPosition = GlobalStats.currentStats.obtenerMinCameraPosition(Application.loadedLevelName);
    }

    void OnLevelWasLoaded()
    {
        Camera.main.orthographicSize = GlobalStats.currentStats.getCameraSize(Application.loadedLevelName);
        maxCameraPosition = GlobalStats.currentStats.obtenerMaxCameraPosition(Application.loadedLevelName);
        minCameraPosition = GlobalStats.currentStats.obtenerMinCameraPosition(Application.loadedLevelName);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		player = GlobalStats.currentStats.jugador;
		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}
        transform.position = player.transform.position + offset;

        if(bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
        }
	}
}
