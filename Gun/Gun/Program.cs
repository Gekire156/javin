using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 20;
    public int reserveAmmoCapacity = 270;

    bool _canShoot;
    int _currentAmmoInClip;
    int _ammoInReserve;

    //public Image muzzleflashImage;
    //public Sprite[] flashes;

    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public float aimSmoothing = 10;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1;
    Vector2 _currentRotation;
    public float weaponSwayAmount = -2;

    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reserveAmmoCapacity;
        _canShoot = true;
    }

    private void Update()
    {
        DetermineAim();
        DetermineRotation();
        if(Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip --;
            StartCoroutine(ShootGun());
        }
        else if(Input.GetKeyDown(KeyCode.R && _currentAmmoInClip < clipSize && _ammoInReserve > 0))
        {
            int amountNeeded = clipSize - _currentAmmoInClip;
            if(amountNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= amountNeeded;
                
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= amountNeeded;
            }
        }
    }

    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        _currentRotation += mouseAxis;

        _currentRotation.Y = Math.Clamp(_currentRotation.Y, -90, 90);

        //transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;

        transform.root.localRotation = Quaternion.AngleAxis(_currentRotation.X, Vector3.up);
        transform.parent.localRotation = Quaternion.AngleAxis(-_currentRotation.Y, Vector3.right);
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if(Input.GetMouseButton(1))
        {
            target = aimingLocalPosition;
        }

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, TimeOnly.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    IEnumerator ShootGun()
    {
        //StartCoroutine(Muzzleflash());

        RayCastForEnemy();

        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    IEnumerator Muzzleflash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.length)];
        muzzleFlashImage.color = Color.White;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new ConsoleColor(0, 0, 0, 0);
    }

    void RayCastForEnemy()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("Enemy")))
        {
            try
            {
                Debug.log("Hit an enemy");
                RigidBody rb = hit.transform.GetComponent<RigidBody>();
                rb.constraints = RigidBodyConstraints.None;
                rb.AddForce(transform.parent.transform.forward * 250);
            }
            catch { }
        }
    }
}