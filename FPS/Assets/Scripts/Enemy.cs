using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        print("Took Damage");
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemey")
        {
            EnemyHealth eHealth = other.gameObject.GetComponent<EnemyHealth>;
            eHealth -= 10;
        }
        Destroy(this.gameObject);
    }
    */
}
