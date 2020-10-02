using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> buttons = new List<AudioClip>();

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip finish;

    [SerializeField]
    AudioClip hit;

    bool soundEffectsOn = false;

    [SerializeField]
    GameObject soundEffectsButton;

    bool musicOn = false;

    [SerializeField]
    GameObject musicButton;

    [SerializeField]
    List<AudioClip> horns = new List<AudioClip>();

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        setMusicOn();
        setSoundOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickedButton()
    {
        if (soundEffectsOn)
        {
            source.clip = buttons[Random.Range(0, buttons.Count)];
            source.volume = 1;
            source.Play();
        }
    }

    public void carHit()
    {
        if (soundEffectsOn && player.getPlayerAlive())
        {
            source.clip = hit;
            source.volume = 0.5f;
            source.Play();
        }
    }

    public void reachedEnd()
    {
        if (soundEffectsOn)
        {
            source.clip = finish;
            source.volume = 1;
            source.Play();
        }
    }

    public void setSoundOn()
    {
        if(soundEffectsOn)
        {
            soundEffectsOn = false;
            soundEffectsButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            soundEffectsOn = true;
            soundEffectsButton.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void setMusicOn()
    {
        if (musicOn)
        {
            musicOn = false;
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicSource.Pause();
        }
        else
        {
            musicOn = true;
            musicButton.transform.GetChild(0).gameObject.SetActive(false);
            musicSource.Play();
        }
    }

    public void playCarNoise(AudioSource s)
    {
        if (soundEffectsOn && !s.isPlaying)
        {
            s.clip = horns[Random.Range(0, horns.Count)];
            s.volume = 0.1f;
            s.Play();
        }
    }
}
