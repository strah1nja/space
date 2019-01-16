using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] float spawnRate = 1;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject path;
    [SerializeField] int health;
    [SerializeField] int points;
    [SerializeField] float randomSpawn = 0.3f;
    [SerializeField] int numInWave = 5;
    [SerializeField] float movementSpeed = 2f;

    public GameObject GetEnemy()
    { return enemy; }

    public float GetSpawnRate()
    { return spawnRate; }

    public List<Transform> GetWayPoints()
    {
        var positions = new List<Transform>();
        foreach (Transform pos in path.transform)
        {
            positions.Add(pos);
        }
        return positions;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetPoints()
    {
        return points;
    }

    public float GetRandomSpawn()
    {
        return randomSpawn;
    }

    public int GetNumInWave()
    { return numInWave; }

    public float GetMovementSpeed()
    { return movementSpeed; }
}
