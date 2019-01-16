using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] WaveConfig waveConfig;
    List<Transform> positions;
    int pos = 0;
    float speed = 2f;
    int health;
    // Use this for initialization
    void Start () {
        positions = waveConfig.GetWayPoints();
        transform.position = positions[pos].transform.position;
        health = waveConfig.GetHealth();
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
        }
    }
}

