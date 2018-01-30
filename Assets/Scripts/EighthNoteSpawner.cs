using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EighthNoteSpawner : MonoBehaviour {

    public GameObject eighthNotes;
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
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        /* notes = new float[222];
         int j = 0;
         for (float i = 1f; i < 100; i += 2f)
         {
             notes[j] = i;
             j++;
         }*/
        if (scene.name == "level-hard")
        {
            notes = new float[26, 4] { { 30f, 1, 1, 0 }, { 32.5f, 0, 0, 0 }, { 38f, 1, 1, 0 }, { 40.5f, 0, 0, 0 }, {46f, 1, 1, 0 }, { 48.5f, 0, 0, 0 }, { 54f, 1, 1, 0 }, { 56.5f, 0, 0, 0 }, 
            { 62, 1, 1, 0 }, { 64.5f, 1, 1, 0 }, { 70, 1, 1, 0 }, { 72.5f, 1, 1, 0 },
        {111,0,0,0 }, { 112.5f,1,1,0}, {118,1,1,0 }, { 120.5f,1,1,0}, 
        { 124f,0,0,0}, { 125f,0,0,0}, { 126f,0,0,0},{127,0,0,0 }, { 128.5f,1,1,0},{ 130f,1,0,0}, { 134f,1,1,0}, { 136.5f,1,1,0},{ 138f,1,0,0},{ 142f,1,1,0}};
        }
        else
        {
            notes = new float[26, 4] { { 30f, 0, 0, 0 }, { 32.5f, 0, 0, 0 }, { 38f, 0, 0, 0 }, { 40.5f, 0, 0, 0 }, {46f, 1, 1, 0 }, { 48.5f, 0, 0, 0 }, { 54f, 1, 1, 0 }, { 56.5f, 0, 0, 0 },
            { 62, 1, 1, 0 }, { 64.5f, 1, 1, 0 }, { 70, 1, 1, 0 }, { 72.5f, 1, 1, 0 },
        {111,0,0,0 }, { 112.5f,1,1,0}, {118,1,1,0 }, { 120.5f,1,1,0},
        { 124f,0,0,0}, { 125f,0,0,0}, { 126f,0,0,0},{127,0,0,0 }, { 128.5f,1,1,0},{ 130f,1,0,0}, { 134f,1,1,0}, { 136.5f,1,1,0},{ 138f,1,0,0},{ 142f,1,1,0}};
        }
        bpm = SongManager.GetComponent<SongManager>().getBpm();
        savedBeat = 0f;
        bps = bpm / 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextIndex < 26)
        {
            if (nextIndex < notes.Length && notes[nextIndex, 0] == SongManager.GetComponent<SongManager>().getBeat())
            {
                int health = (int)(notes[nextIndex, 1] + notes[nextIndex, 2] + notes[nextIndex, 3]) + 1; //ONLY WORKS FOR NON HAZARD SHIELDS
                SpawnEighthNote(health, nextIndex);

                //initialize the fields of the music note

                nextIndex++;
            }
        }

    }
    void SpawnEighthNote(int health, int nextIndex)
    {
        // create a new gameObject
        GameObject clone = Instantiate(eighthNotes, transform.position, transform.rotation) as GameObject;
        clone.gameObject.GetComponent<EighthNote>().SetSpeed(speed);
        clone.gameObject.GetComponent<EighthNote>().SetSong(SongManager);
        clone.gameObject.GetComponent<EighthNote>().SetWaypoints(myWaypoints);
        clone.gameObject.GetComponent<EighthNote>().SetShields((int)(notes[nextIndex, 1]), (int)(notes[nextIndex, 2]), (int)(notes[nextIndex, 3]));
        clone.gameObject.GetComponent<EighthNote>().SetHealth(health);
    }
}
