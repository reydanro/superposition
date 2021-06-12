using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private AudioSource music;
    public static MusicController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            music = GetComponent<AudioSource>();
            music.Play();
            Instance = this;
        }
        else if (Instance != this)
        {
            AudioSource newMusic = GetComponent<AudioSource>();

            if (Instance.music.clip.ToString() != newMusic.clip.ToString())
            {

                Instance.music.Stop();
                Destroy(Instance);
                DontDestroyOnLoad(transform.gameObject);
                newMusic.Play();
                Instance = this;
            }
            else
            {

                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
