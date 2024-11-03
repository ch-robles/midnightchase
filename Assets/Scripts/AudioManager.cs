using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio Clip ------")]
    public AudioClip menupause;
    public AudioClip ingame;
    public AudioClip horse;
    //public AudioClip footsteps;
    public AudioClip click;
    public AudioClip death;
    public AudioClip win;


    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        PlayMenuMusic ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void PlayMenuMusic ()
	{
		if (instance != null) {
			if (instance.musicSource != null) {
				instance.musicSource.Pause();
				instance.musicSource.clip = instance.menupause;
				instance.musicSource.Play();
			}
		} else {
			Debug.LogError("Unavailable MusicPlayer component");
		}
	}

    static public void PlayGameMusic ()
	{
		if (instance != null) {
			if (instance.musicSource != null) {
				instance.musicSource.Pause();
				instance.musicSource.clip = instance.ingame;
				instance.musicSource.Play();
			}
		} else {
			Debug.LogError("Unavailable MusicPlayer component");
		}
	}

    //---------------------------//

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    //--------------------------//

    public void ButtonClick()
    {
        PlaySFX(click);
    }

    public void Death()
    {
        PlaySFX(death);
    }

    public void Win()
    {
        PlaySFX(win);
    }

    public void HorseNeigh()
    {
        PlaySFX(horse);
    }

   
}