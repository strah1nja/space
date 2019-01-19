using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject laser;
    [SerializeField] float firerate = 1;
    public float firespeed = 2;
    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;
    [SerializeField] float speed = 5f;
    Coroutine fireing;
    [SerializeField] int health;
    [SerializeField] AudioClip laserShot, playerExplosion;
    [SerializeField] [Range(0, 1)] float playerLaserVolume = 0.5f;


    // Use this for initialization
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        
    }
 
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Shoot();
        // M2();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer ddealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(ddealer);
    }

    private void ProcessHit(DamageDealer ddealer)
    {
        health -= ddealer.GetDamage();
        ddealer.Destroy();
        print("PlayerHealth: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(playerExplosion, Camera.main.transform.position);
        }
    }

    public void Shoot() {


        if (Input.GetMouseButtonDown(0))
        {
            fireing = StartCoroutine(Fire());
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(fireing);
        }

        

    }

    IEnumerator Fire() {
        while (true)
        {
            GameObject shot = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(laserShot, Camera.main.transform.position, playerLaserVolume);
            shot.GetComponent<Rigidbody2D>().velocity += new Vector2(0, firespeed);
            yield return new WaitForSeconds(firerate);
        }

    }


    public void PlayerMove()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float newPos = Mathf.Clamp(transform.position.x + deltaX,xMin+0.5f,xMax-0.5f); 
        transform.position = new Vector2(newPos, transform.position.y);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin+0.5f, yMax-0.5f);
        transform.position = new Vector2(transform.position.x, newPosY);
     
        


    }

    public void M2()
    {
        // Moves object according to finger movement on the screen

        if (Input.touchCount > 0 &&
          Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            // Get movement of the finger since last frame
            var touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
            transform.Translate(+touchDeltaPosition.x * speed * Time.deltaTime,
                        +touchDeltaPosition.y * speed * Time.deltaTime, 0);

            Mathf.Clamp(transform.position.x, xMin, xMax);
            Mathf.Clamp(transform.position.y, xMin, yMax);

        }

    }
}
