using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] float firerate = 0.5f;
    [SerializeField] GameObject shot;
    [SerializeField] float shotSpeed;
    [SerializeField] GameObject expl;
    [SerializeField] int points = 10;
    List<Transform> positions;
    [SerializeField] AudioClip a, laseShot;
    [SerializeField] [Range(0,1)] float enemyDeathVolume= 0.5f;
    [SerializeField] [Range(0, 1)] float enemyLaserVolume = 0.5f;
    GameSession gses;
    int pos = 0;
    float speed = 2f;
    int health;
    // Use this for initialization
    void Start () {
        positions = waveConfig.GetWayPoints();
        transform.position = positions[pos].transform.position;
        health = waveConfig.GetHealth();
        StartCoroutine(Fire());
        gses = FindObjectOfType<GameSession>();
        
    }

    // Update is called once per frame
    void Update () {

        if (pos <= positions.Count-1)
        {
            transform.position = Vector2.MoveTowards(transform.position, positions[pos].transform.position, speed * Time.deltaTime);
            if (transform.position == positions[pos].transform.position)
            {
                pos++;
            }
        }
        else 
	{
            Destroy(gameObject);
        }	
}


    IEnumerator Fire()
    {
        while (true)
        {
            GameObject laser = Instantiate(shot, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laseShot, Camera.main.transform.position, enemyLaserVolume);
            laser.GetComponent<Rigidbody2D>().velocity -= new Vector2(0, shotSpeed * Time.deltaTime);
            yield return new WaitForSeconds(firerate);

        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        ProcesHit(damageDealer);

    }

    private void ProcesHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Destroy();
        print(health);
        if (health <= 0)
        {
            Destroy(gameObject);
            gses.AddToScore(points);
            Instantiate(expl, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(a, Camera.main.transform.position, enemyDeathVolume);
        }
    }
}

