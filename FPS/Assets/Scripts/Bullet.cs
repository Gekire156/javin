using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnCollisionEnter(Collider collision)
    {
        GameObject collisionGameobject = collision.gameObject;
        print("It ran 1");
        if (collisionGameobject.tag == "Enemy")
        {
            print("It ran 2");
            collisionGameobject.GetComponent<Enemy>().TakeDamage(10);
        }
        Destroy(this.gameObject);
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        print("In a collision");
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemycomponent))
        {
            print("Hit the Enemy");
            enemycomponent.TakeDamage(10);
            Destroy(this.gameObject);
        }
    }
}
