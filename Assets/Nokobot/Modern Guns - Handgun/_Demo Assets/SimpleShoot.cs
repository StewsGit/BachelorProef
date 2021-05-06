using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;


[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 800f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    [Header("Functionality")]
    [Tooltip("Object to show when magazine is getting reloaded")] [SerializeField] private GameObject attachedMagazine;
    // [Tooltip("Standalone Magazine")] [SerializeField] private GameObject LooseMagazine;

    public bool magazineInSlot;
    public bool gunInHand;
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip noAmmoSound;
    public Magazine ammo;
    public GameObject magazine;
    public GameObject slider;
    public XRBaseInteractor socketInteractor;
    private bool hasSlide = true;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (Input.GetAxis("Oculus_CrossPlatform_Button4") >0)
        {
            if (magazine != null)
            {
            DropMag();
            }

        }

    }

    public void PullTrigger()
    {
        if (ammo && ammo.Bullets >= 0 && hasSlide)
        {

            if (ammo.Bullets == 1)
            {
             //   GameObject MuzzleEffect = Instantiate(Muzzle, MuzzlePos.position, MuzzlePos.rotation);
              //  Destroy(MuzzleEffect, 2);
                gunAnimator.SetBool("EmptyMag", true);
                slider.GetComponent<Rigidbody>().mass = 0;
                CasingRelease();
                Shoot();
            source.PlayOneShot(noAmmoSound);
            }
            else if (ammo.Bullets == 0)
            {
                gunAnimator.SetBool("EmptyMag", true);
                slider.GetComponent<Rigidbody>().mass = 0;
            source.PlayOneShot(noAmmoSound);
            }
            else
            {
             //   GameObject MuzzleEffect = Instantiate(Muzzle, MuzzlePos.position, MuzzlePos.rotation);
               // Destroy(MuzzleEffect, 2);
                gunAnimator.SetTrigger("Fire");

            }
        }
        else
        {
            source.PlayOneShot(noAmmoSound);
        }

    }
    //This function creates the bullet behavior
    void Shoot()
    {
        ammo.Bullets--;
        source.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
    public void GetInteractable()
    {

    }
    public void ReloadMag(XRBaseInteractor mag)
    {
        Debug.Log("Mag reload" + mag.name);
        magazine = mag.selectTarget.gameObject;
        attachedMagazine.GetComponent<MeshRenderer>().enabled = true;

        // still make listt to remove bullets.
        magazine.GetComponent<MeshRenderer>().enabled = false;

        ammo = magazine.GetComponent<Magazine>();
        source.PlayOneShot(reloadSound);
        //   mag.gameObject.GetComponent<ParentConstraint>().constraintActive = true;
        magazineInSlot = true;
        hasSlide = false;
    }
    public void DropMag()
    {
        Debug.Log("Mag Drop");
        attachedMagazine.GetComponent<MeshRenderer>().enabled = false;
        magazine.gameObject.GetComponent<MeshRenderer>().enabled = true;
        magazine = null;
        ammo = null;
        source.PlayOneShot(reloadSound);
        //attachedMagazine.GetComponent<MeshRenderer>().enabled = false;
        // mag.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //   mag.gameObject.GetComponent<ParentConstraint>().constraintActive = false;
        magazineInSlot = false;
    }

    public void GrabGun()
    {

        List<Collider> gunColliders = this.gameObject.GetComponentInParent<XRGrabInteractable>().colliders;
        Physics.IgnoreLayerCollision(9, 11, true); // 9 = Player 11 = Gun
        gunInHand = true;
        ToggleSlider();

        foreach (Collider gunCol in gunColliders)
        {
            //            gunCol.enabled = false;
        }
    }

    public void DropGun()
    {
        List<Collider> gunColliders = this.gameObject.GetComponentInParent<XRGrabInteractable>().colliders;
        Physics.IgnoreLayerCollision(9, 11, false); // 9 = Player 11 = Gun
        gunInHand = false;
        ToggleSlider();

        foreach (Collider gunCol in gunColliders)
        {
            //            gunCol.enabled = false;
        }
    }
    public void ToggleSlider()
    {
        XROffsetGrabInteractable slider = this.gameObject.GetComponentInChildren<XROffsetGrabInteractable>();
        if (gunInHand)
        {
            slider.enabled = true;
            slider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            slider.enabled = false;
            slider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    public void Slide()
    {
        hasSlide = true;
        source.PlayOneShot(reloadSound);

    }
    public void CloseSlider()
    {
        gunAnimator.SetBool("EmptyMag", false);
        slider.GetComponent<Rigidbody>().mass = 1;
    }
}