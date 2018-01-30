using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EighthNote : MonoBehaviour {
    Rigidbody2D rb;
    Transform _transform;
    Animator _animator;
    Color original;
    SpriteRenderer sr;
    GameObject SongManager;

    public static AudioSource _audio;
    public Vector2 SpawnPos;
    public Vector2 RemovePos;
    public GameObject[] myWaypoints; // to define the movement waypoints
    private float _vx = 0f;
    private float _vy = 0f;
    private int _myWaypointIndex = 0;
    private int _prevWaypointIndex = 0;
    private float prevBeat;
    private int health;
    private bool _hit;
    private float _hitBeat;
    private bool _hitable;
    private int totalShields;

    private float speed;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        if (_animator == null) // if Animator is missing
            Debug.LogError("Animator component missing from this gameobject");

    }
    // Use this for initialization
    void Start()
    {

        //rb.velocity = new Vector2(0, -speed);
        _myWaypointIndex = 0;
        _prevWaypointIndex = -1;
        prevBeat = 0;
        _hit = false;
        _hitable = true;
        original = sr.color;


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SongManager.GetComponent<Metronome>().getBeat());
        _vx = myWaypoints[_myWaypointIndex].transform.position.x - _transform.position.x;
        _vy = myWaypoints[_myWaypointIndex].transform.position.y - _transform.position.y;
        if (Mathf.Abs(_vx) <= 0.05f && Mathf.Abs(_vy) <= 0.05f)
        {
            // At waypoint so stop moving
            rb.velocity = new Vector2(0, 0);
            //_hitable = false; //Can't hit while waiting at waypoint

            if (_myWaypointIndex < myWaypoints.Length - 1)
            {
                _myWaypointIndex++;
            }
        }

        if ((SongManager.GetComponent<SongManager>().getBeat() % 1 == 0.5f || SongManager.GetComponent<SongManager>().getBeat() % 1 == 0) && SongManager.GetComponent<SongManager>().getBeat() != prevBeat)
        {
            prevBeat = SongManager.GetComponent<SongManager>().getBeat();

            // Debug.Log(_vx);
            //Debug.Log(_vy);

            if (_myWaypointIndex != _prevWaypointIndex && !_hit)
            {
                _prevWaypointIndex = _myWaypointIndex;
                rb.velocity = new Vector2(_vx * speed, _vy * speed);
                //_hitable = true; //Can hit while moving
            }
        }
        if (SongManager.GetComponent<SongManager>().getBeat() != _hitBeat && (SongManager.GetComponent<SongManager>().getBeat() % 1 == 0.5f || SongManager.GetComponent<SongManager>().getBeat() % 1 == 0))
        {
            _hit = false;
            _hitable = true;
        }





    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


    public void SetSong(GameObject newSong)
    {
        SongManager = newSong;
    }

    public void SetWaypoints(GameObject[] newWaypoints)
    {
        myWaypoints = newWaypoints;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public void SetShields(int shield1, int shield2, int shield3)
    {
        if (shield1 == 1)
            _transform.GetChild(0).gameObject.SetActive(true);
        else if (shield1 == 2)
            _transform.GetChild(3).gameObject.SetActive(true);
        if (shield2 == 1)
            _transform.GetChild(1).gameObject.SetActive(true);
        else if (shield2 == 2)
            _transform.GetChild(4).gameObject.SetActive(true);
        if (shield3 == 1)
            _transform.GetChild(2).gameObject.SetActive(true);
        else if (shield3 == 2)
            _transform.GetChild(5).gameObject.SetActive(true);
        totalShields = shield1 + shield2 + shield3; //Only works withouthazard
    }

    public void hit()
    {
        if (_hitable)
        {
            if (totalShields > 0)
            {
                _transform.GetChild(totalShields - 1).gameObject.SetActive(false);
                totalShields--;
            }

            _hit = true;
            _hitable = false;
            _hitBeat = SongManager.GetComponent<SongManager>().getBeat();
            health--;
            StartCoroutine(TakeDamage());
            if (health <= 0)
            {
                //Start death
                StartCoroutine(DestroyNote());
            }
        }
        else // Go immediately to last waypoint
        {
            Debug.Log("Miss");
            _vx = myWaypoints[myWaypoints.Length - 1].transform.position.x - _transform.position.x;
            _vy = myWaypoints[myWaypoints.Length - 1].transform.position.y - _transform.position.y;
            rb.velocity = new Vector2(_vx * speed * 2, _vy * speed * 2);
            health--;
            StartCoroutine(TakeDamage());
            _hitable = false;

        }

    }


    IEnumerator DestroyNote()
    {
        _audio.Play();
        sr.color = original;
        _animator.SetTrigger("Dead");
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        yield return new WaitForSeconds(0.19f);
        DestroyObject(gameObject);
    }

    IEnumerator TakeDamage()
    {
        sr.color = new Color(1, .7f, .7f);
        yield return new WaitForSeconds(0.19f);
        sr.color = original;
    }
}
