using UnityEngine;
using System.Collections;
public class MusicPlayer : MonoBehaviour
{
    // Use this for initialization
    static MusicPlayer instance = null;

    public AudioClip startClip, gameClip, endClip;

    private AudioSource music;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }
    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Music player loaded level" + level);
        music.Stop();
        if (level == 0)
        {
            music.clip = startClip;
        }
        if (level == 1)
        {
            music.clip = gameClip;
        }
        if (level == 2)
        {
            music.clip = endClip;
        }
        music.loop = true;
        music.Play();
    }
}