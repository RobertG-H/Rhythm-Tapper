using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteSpawner : MonoBehaviour {

    public GameObject quarterNotes;
    public GameObject SongManager;
    public GameObject[] myWaypoints;

    //beats per minute of a song
    private double bpm;
    private double bps;
    public float speed;

    //keep all the position-in-beats of notes in the song
    float[,] notes;

    //the index of the next note to be spawned
    int nextIndex = 0;

    private float savedBeat;

    // Use this for initialization
    void Start () {
        Scene scene = SceneManager.GetActiveScene();
        /*  notes = new float[222];
          int j = 0;
         for (float i = 1f; i < 100; i+=4f)
         {
             notes[j] = i;
            j++;
        }*/


        if (scene.name == "level-hard")
        {
            notes = new float[18, 4] { { 13, 1,1,1 }, { 17, 1, 1, 1 }, {21, 1, 1, 1 }, { 25, 1, 0, 0 }
        , {77,0,0,0 }, {79,0,0,0 }, {81,0,0,0 } , {83,0,0,0 } , {85,0,0,0 } , {89,0,0,0 } , {91,0,0,0 } , {93,0,0,0 } , {95,0,0,0 } , {97,0,0,0 }, {99,0,0,0 }, {101,0,0,0 },{105,0,0,0 },{107,0,0,0 }};
        }
        else
        {
            notes = new float[18, 4] { { 13, 1,0,0 }, { 17, 1, 0, 0 }, {21, 1, 0, 0 }, { 25, 1, 0, 0 }
        , {77,0,0,0 }, {79,0,0,0 }, {81,0,0,0 } , {83,0,0,0 } , {85,0,0,0 } , {89,0,0,0 } , {91,0,0,0 } , {93,0,0,0 } , {95,0,0,0 } , {97,0,0,0 }, {99,0,0,0 }, {101,0,0,0 },{105,0,0,0 },{107,0,0,0 }};
        }
        bpm = SongManager.GetComponent<SongManager>().getBpm();
        savedBeat = 0f;
        bps = bpm / 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextIndex < 18)
        {
            if (nextIndex < notes.Length && notes[nextIndex, 0] == SongManager.GetComponent<SongManager>().getBeat())
            {
                int health = (int)(notes[nextIndex, 1] + notes[nextIndex, 2] + notes[nextIndex, 3]) + 1; //ONLY WORKS FOR NON HAZARD SHIELDS
                SpawnQuarterNote(health, nextIndex);

                //initialize the fields of the music note

                nextIndex++;
            }
        }

        
    }


    void SpawnQuarterNote(int health, int nextIndex)
    {
        // create a new gameObject
        GameObject clone = Instantiate(quarterNotes, transform.position, transform.rotation) as GameObject;
        clone.gameObject.GetComponent<Note>().SetSpeed(speed);
        clone.gameObject.GetComponent<Note>().SetSong(SongManager);
        clone.gameObject.GetComponent<Note>().SetWaypoints(myWaypoints);

        //Shield generation

        clone.gameObject.GetComponent<Note>().SetShields((int)(notes[nextIndex, 1]), (int)(notes[nextIndex, 2]), (int)(notes[nextIndex, 3]));
        clone.gameObject.GetComponent<Note>().SetHealth(health);
    }
}
