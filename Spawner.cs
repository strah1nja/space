using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    private int waveStarting;
    // Use this for initialization
    IEnumerator Start () {
        do
        {
            yield return StartCoroutine(SpawnWaves());
        } while (true);
     }

    
        
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnWaves() {
        for (int i = 0; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnEnemies(currentWave));
        }

    }

    IEnumerator SpawnEnemies(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumInWave(); i++)
        {
            Instantiate(waveConfig.GetEnemy(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetSpawnRate());
        }
        waveStarting++;
    }
}
