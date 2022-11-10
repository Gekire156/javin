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
    private void OnTriggerEnter(Collider collision){
        Destroy(this.gameObject);
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemycomponent))
        {
            enemycomponent.TakeDamage(10);
            Destroy(this.gameObject);
        }
    }
}