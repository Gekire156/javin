using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;

    public int speed = 5;

    public Transform player;

    public GameObject enemy;

    private int enemySpawns = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            enemy.gameObject.SetActive(false);
            enemyHealth = 100;
            SpawnEnemy(2);
            Destroy(enemy);
        }
    }
    public void SpawnEnemy(int number)
    {
        for(int i = 0; i < number; i++){
            GameObject newEnemy = Instantiate(enemy);
            enemySpawns += 1;
            newEnemy.name = "enemy " + enemySpawns;
            newEnemy.gameObject.SetActive(true);
            newEnemy.transform.position = new Vector3(Random.Range(5, 45), 1, Random.Range(5, 45));

        }
    }
}
