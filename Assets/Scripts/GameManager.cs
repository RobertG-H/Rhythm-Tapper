using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public GameObject Base;
    public GameObject Song;

    public string menuLevel;
    public string currentLevel;

    public GameObject[] UIExtraLives;
    public GameObject UILose;
    public GameObject UIWin; 
    public GameObject MMButton;
    public GameObject ResetButton;

    public int health;

    private void Awake()
    {
        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();

        Time.timeScale = 1f;
        // setup all the variables, the UI, and provide errors if things not setup properly.
        //setupDefaults();
    }
    // Use this for initialization
    void Start()
    {
        refreshGUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            UILose.SetActive(true);
            MMButton.SetActive(true);
            ResetButton.SetActive(true);
            Time.timeScale = 0f;
           // Song.gameObject.GetComponent<Song>().lose();
            //END GAME
        }
       if (Song.gameObject.GetComponent<SongManager>().isDone())
        {
           UIWin.SetActive(true);
           MMButton.SetActive(true);
            ResetButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }



    // public function for level complete
    public void LevelCompete()
    {
        SceneManager.LoadScene(menuLevel);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    // public function to add points and update the gui and highscore player prefs accordingly
    public void takeDamage(int amount)
    {
        // increase score
        health -= amount;

        // update UI
        refreshGUI();


    }
    void refreshGUI()
    {
        //UIScore.text = "Powerups";
        //UIHighScore.text = "Highscore: "+highscore.ToString ();
        //UILevel.text = _scene.name;
        // turn on the appropriate number of life indicators in the UI based on the number of lives left
        for (int i = 0; i < UIExtraLives.Length; i++)
        {
            if (i < health)
            { // show one less than the number of lives since you only typically show lifes after the current life in UI
                UIExtraLives[i].SetActive(true);
            }
            else
            {
                UIExtraLives[i].SetActive(false);
            }
        }
    }

}
