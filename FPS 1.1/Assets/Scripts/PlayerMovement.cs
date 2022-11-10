using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public float aimSmoothing = 10;

    private float horizontal;
    private float vertical;

    Vector2 currentRotation;
    private Rigidbody playerRB;

    public int jumpForce = 5;

    public int speed = 5;

    public GameObject bullet;

    public int ammoCount = 30;
    public float firerate = 5;

    Rigidbody firedBulletRB;

    GameObject firedBullet;

    Vector3 playerPosition;

    Vector3 gunPosition;

    Vector3 gunRotation;

    public GameObject ground;

    public Vector3 gunBarrelPosition;

    bool isOnGround;

    bool canFire;

    private bool reload;

    private bool canReload;

    //public Text ammoDisplay;
    public TMP_Text ammoDisplay;
    public TMP_Text ammoLabel;
    public TMP_Text reloadText;
    public TMP_Text reloadingText;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        bullet.SetActive(false);

        ammoCount = 30;
        canFire = true;
        reload = false;
        canReload = true;

        isOnGround = true;

        ammoDisplay.gameObject.SetActive(true);
        ammoLabel.gameObject.SetActive(true);
        reloadText.gameObject.SetActive(false);
        reloadingText.gameObject.SetActive(false);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        ammoDisplay.text = ammoCount.ToString();


        DetermineRotation();
        playerPosition = transform.position;

        gunPosition = transform.GetChild(1).position;

        gunBarrelPosition = GameObject.Find("muzzle").transform.position;


        gunRotation = transform.GetChild(0).eulerAngles;

        StartCoroutine(Shoot());
        Movement();
        
    }

    IEnumerator Shoot()
    {

        if(ammoCount > 0 && Input.GetKey(KeyCode.Mouse0) && canFire == true && reload == false)
        {
            ammoDisplay.gameObject.SetActive(true);
            ammoLabel.gameObject.SetActive(true);
            reloadText.gameObject.SetActive(false);
            reloadingText.gameObject.SetActive(false);

            canFire = false;
            ammoCount --;
            firedBullet = Instantiate(bullet.gameObject);

            //Physics.IgnoreCollision(this.GetComponent<Collider>(), firedBullet.GetComponent<Collider>());
            //Physics.IgnoreCollision(firedBullet.GetComponent<Collider>(), firedBullet.GetComponent<Collider>());

            firedBullet.gameObject.SetActive(true);
            firedBulletRB = firedBullet.GetComponent<Rigidbody>();

            firedBullet.transform.eulerAngles = gunRotation;
            firedBullet.transform.position = gunBarrelPosition;

            firedBulletRB.AddForce(firedBulletRB.transform.forward * 35, ForceMode.Impulse);
            Destroy(firedBullet, 1);
            yield return new WaitForSeconds(firerate);

        //Cant Shoot
            if(ammoCount <= 0){
                ammoDisplay.gameObject.SetActive(false);
                ammoLabel.gameObject.SetActive(false);
                reloadText.gameObject.SetActive(true);
                reloadingText.gameObject.SetActive(false);
                canFire = false;
            }
            
        //Can Shoot
            else{
                canFire = true;
            }
        }
        //Reload
        else if(Input.GetKeyDown("r") && canReload == true && ammoCount < 30){
                anim.SetTrigger("Reload");
                canFire = false;
                reload = true;
                canReload = false;
                ammoDisplay.gameObject.SetActive(false);
                ammoLabel.gameObject.SetActive(false);
                reloadText.gameObject.SetActive(false);
                reloadingText.gameObject.SetActive(true);
                yield return new WaitForSeconds(1.75f);
                reload = false;
                ammoDisplay.gameObject.SetActive(true);
                ammoLabel.gameObject.SetActive(true);
                reloadText.gameObject.SetActive(false);
                reloadingText.gameObject.SetActive(false);
                ammoCount = 30;
                canFire = true;
                canReload = true;

            }
    }

    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= aimSmoothing;
        currentRotation += mouseAxis;

        currentRotation.y = Math.Clamp(currentRotation.y, -90, 90);

        //Up and Down
        transform.GetChild(0).localRotation = Quaternion.AngleAxis(-currentRotation.y, Vector3.right);

        //Left and Right
        transform.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);

        //Up and Down of gun
        transform.GetChild(1).localRotation = Quaternion.AngleAxis(-currentRotation.y, Vector3.right);

    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * vertical * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontal * speed);

        
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "rock")
        {
            isOnGround = true;
        }
    }
}