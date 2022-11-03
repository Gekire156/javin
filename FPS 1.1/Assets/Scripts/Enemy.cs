using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;

    public int speed = 5;

    public Transform playerTransform;

    public Transform enemyTransform;

    public float enemyPositionY;

    public GameObject enemy;

    public int rotationSpeed = 3;

    private int enemySpawns = 0;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = false;
 
        GetComponent<Rigidbody>().useGravity = true;

        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        enemyPositionY = this.transform.position.y;


        transform.LookAt(playerTransform, Vector3.down);
        transform.position += transform.forward * speed * Time.deltaTime;
        //enemyPositionY -= enemyGravity * Time.deltaTime;
        
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
            newEnemy.transform.position = new Vector3(Random.Range(-64, 22), 1, Random.Range(-54, 31));

        }
    }
}