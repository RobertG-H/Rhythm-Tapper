using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    public KeyCode key;
    //public AudioClip correctSound;
    //public AudioClip missedSound;
    // public AudioSource audioSource;


    SpriteRenderer sr;
    Animator _animator;

    bool active = false;
    GameObject note;
    Color old;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        //audioSource = gameObject.GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        if (_animator == null) // if Animator is missing
            Debug.LogError("Animator component missing from this gameobject");
    }

    // Use this for initialization
    void Start()
    {
        old = sr.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            StartCoroutine(Pressed());
        }
        
        if (CrossPlatformInputManager.GetButtonDown("Jump") && active)
        {

            try
            {
                note.GetComponent<Note>().hit();
            }
            catch
            {

            }
            try
            {
                note.GetComponent<EighthNote>().hit();
            }
            catch
            {

            }
            
            
            //  audioSource.clip = correctSound;
            //  audioSource.Play();
        }

        else if (Input.GetKeyDown(key) && !active)
        {
            // audioSource.clip = missedSound;
            //audioSource.Play();
        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        active = true;
        if (collision.gameObject.tag == "Note")
        {
            //Debug.Log("HIT");
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }

    IEnumerator Pressed()
    {
        _animator.SetBool("Attack", true);
        //sr.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(.1f);//.24 at 60 samples
        //sr.color = old;
        _animator.SetBool("Attack", false);
    }

    public void takeDamage()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        _animator.SetBool("Hit", true);
        _animator.SetBool("Attack", false);
        yield return new WaitForSeconds(.1f);
        _animator.SetBool("Hit", false);
    }
}
