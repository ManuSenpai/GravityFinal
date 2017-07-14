using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager currentSounds;

    public AudioSource nailShoot;
    public AudioSource enemyDeath;
    public AudioSource enemyHit;
    public AudioSource EloiseHit;
    public AudioSource Stage1;
    public AudioSource Battle;
    public AudioSource SpiderShoot;

    // Use this for initialization
    void Awake()
    {
        Cursor.visible = false;
        if (currentSounds == null)
        {
            currentSounds = this;
            //Aunque cambie de mapa, esto no se va a eliminar
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (currentSounds != this)
                Destroy(this.gameObject);
        }
    }

    void OnLevelWasLoaded()
    {
        switch (Application.loadedLevelName)
        {
            case "Level0":
                if (Battle.isPlaying)
                    Battle.Stop();
                Stage1.Play();
                break;
            case "Level1":
                if (Stage1.isPlaying)
                    Stage1.Stop();
                Battle.Play();
                break;
            case "MenuPrincipal":
                if (Battle.isPlaying)
                    Battle.Stop();
                if (Stage1.isPlaying)
                    Stage1.Stop();
                break;
            case "GameOver":
                if (Battle.isPlaying)
                    Battle.Stop();
                if (Stage1.isPlaying)
                    Stage1.Stop();
                break;
            case "Credits":
                if (Battle.isPlaying)
                    Battle.Stop();
                if (Stage1.isPlaying)
                    Stage1.Stop();
                break;
        }
    }
}
