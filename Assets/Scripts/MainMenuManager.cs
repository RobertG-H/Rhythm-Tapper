using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager
public class MainMenuManager : MonoBehaviour {

    public AudioClip songToPlay;
    private AudioSource audioSource;
    // Use this for initialization
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameEasy()
    {

        SceneManager.LoadScene("level-easy");

    }
    public void StartGameHard()
    {
        Debug.Log("load hard level");
        SceneManager.LoadScene("level-hard");

    }

    public void DoQuit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void ShowControls()
    {
        //Hide buttons
        //show controls text
    }
}
