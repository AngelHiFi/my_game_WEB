using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	
	[SerializeField] private Rigidbody targetRb;
	[SerializeField] protected GameObject[] enemies;  // префабы врагов
    [SerializeField] protected GameObject powerup;    // префаб бонуса ( пуль, здоровье, броня)
	
    [SerializeField] protected private float zEnemySpawn = 12.0f;
    [SerializeField] protected private float xSpawnRange = 16.0f;
    [SerializeField] protected private float zPowerupRange = 5.0f;
    [SerializeField] protected private float ySpawn = 1.95f;
    [SerializeField] protected private float powerupSpawnTime = 5.0f;   
    [SerializeField] protected private float enemySpawnTime = 3.0f;     // раз в секунду            default 1.0f
    [SerializeField] protected private float startDelay = 1.0f;         // задержка между спаунами  default 1.0f

	
    void Start()
    {
		StartSpawn();  // abstraction
    }
	
	 protected private void StartSpawn()
	 {
		targetRb = GetComponent<Rigidbody>();
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
     }

     protected private void SpawnEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndex = Random.Range(0, enemies.Length);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);
        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
    }

    protected private void SpawnPowerup()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zPowerupRange, zPowerupRange);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);
        Instantiate(powerup, spawnPos, powerup.gameObject.transform.rotation);
	}
}

