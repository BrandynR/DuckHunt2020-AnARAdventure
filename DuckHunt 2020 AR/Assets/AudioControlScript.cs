using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioControlScript : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(introMusic());
    }

    private void playSound(int sound)
    {
        audio.clip = clips[sound];
        audio.Play();
    }

    private IEnumerator introMusic()
    {
        yield return new WaitForSeconds(3f);
        playSound(0);
        StartCoroutine(duckQuack());
    }

    private IEnumerator duckQuack()
    {
        yield return new WaitForSeconds(1.8f);
        playSound(1);
        StartCoroutine(dogBark());
    }

    private IEnumerator dogBark()
    {
        yield return new WaitForSeconds(.5f);
        playSound(2);
        StartCoroutine(gunShot());
    }

    private IEnumerator gunShot()
    {
        yield return new WaitForSeconds(.4f);
        playSound(3);
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void quitApp()
    {
        Application.Quit();
    }
}
