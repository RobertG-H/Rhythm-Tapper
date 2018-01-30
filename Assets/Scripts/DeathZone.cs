using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public GameObject Player;
    public static AudioSource _audio;

    // Use this for initialization
    void Start () {
        _audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(health);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Note")
        {
            Player.GetComponent<Player>().takeDamage();
            GameManager.gm.takeDamage(1);
            Destroy(collision.gameObject);
            _audio.Play();
        }

    }
}
