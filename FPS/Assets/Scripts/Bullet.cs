using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(player.GetComponent<Collider>(), this.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), this.GetComponent<Collider>());

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
        Destroy(this.gameObject);
        /*
        if (collision.gameObject.tag == "Enemy")
        {
            print("Hit the Enemy");
            player.TryGetComponent<Enemy>TakeDamage(10);
        }
        */
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemycomponent))
        {
            print("Hit the Enemy");
            enemycomponent.TakeDamage(10);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "player")
        {
            print("Hit player");
        }
        else if(collision.gameObject.tag == "bullet")
        {
            print("Hit bullet");
        }
        else if(collision.gameObject.tag == "ground")
        {
            print("Hit ground");
        }
        else if(collision.gameObject.tag == "gun")
        {
            print("Hit gun");
        }
        else
        {
            print("Hit something else");
        }
    }
}