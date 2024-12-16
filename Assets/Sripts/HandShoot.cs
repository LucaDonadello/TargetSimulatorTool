using UnityEngine;
using Oculus.Interaction;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class HandPointer : MonoBehaviour
{
    public Transform indexFingerTip;
    //public GameObject spherePrefab;
    //public GameObject bulletPrefab;
    public List<GameObject> prefabList = new List<GameObject>();
    public List<float> forceIndex = new List<float>();
    private int counter = 0;

    [SerializeField]
    private ActiveStateSelector pistolGestureSelector;
    [SerializeField]
    private ActiveStateSelector shootGestureSelector;
    [SerializeField]
    private ActiveStateSelector bunnyGestureSelectorLeft;
    [SerializeField]
    private ActiveStateSelector bunnyGestureSelectorRight;

    private bool isPistolGestureActive = false;
    private bool isShootGestureActive = false;

    // Cooldown variables
    private bool canShoot = true; // Determines if shooting is allowed
    public float shootCooldown = 1f; // Cooldown duration in seconds

    private GameObject Player;
    Player PlayerScript;
    TextMeshProUGUI totalAmmosText;

    void Start()
    {
        if (pistolGestureSelector != null)
        {
            pistolGestureSelector.WhenSelected += OnPistolGestureActivated;
            pistolGestureSelector.WhenUnselected += OnPistolGestureDeactivated;
        }
        if (shootGestureSelector != null)
        {
            shootGestureSelector.WhenSelected += OnShootGestureActivated;
            shootGestureSelector.WhenUnselected += OnShootGestureDeactivated;
        }
        if (bunnyGestureSelectorLeft != null)
        {
            bunnyGestureSelectorLeft.WhenSelected += OnBunnyGestureActivated;
            bunnyGestureSelectorLeft.WhenUnselected += OnBunnyGestureDeactivated;
        }
        if (bunnyGestureSelectorRight != null)
        {
            bunnyGestureSelectorRight.WhenSelected += OnBunnyGestureActivated;
            bunnyGestureSelectorRight.WhenUnselected += OnBunnyGestureDeactivated;
        }

        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<Player>();
        totalAmmosText = GameObject.Find("TotalAmmos").GetComponent<TextMeshProUGUI>();
        //forceIndex.Add(750f);
        //forceIndex.Add(1000f);
    }

    void Update()
    {
        // Allow shooting only if the pistol gesture is active and shooting gesture is triggered
        if (isPistolGestureActive && isShootGestureActive && canShoot)
        {
            ShootBullet();
            totalAmmosText.text = "Total  Shots: " + PlayerScript.totalAmmos.ToString();
            StartCoroutine(ShootCooldown());
            isShootGestureActive = false; // Reset the shoot gesture flag to prevent repeated firing
        }
    }

    void ShootBullet()
    {
        if (PlayerScript.totalAmmos > 0)
        {
            Debug.Log(forceIndex[counter]);
            GameObject bullet = Instantiate(prefabList[counter], indexFingerTip.position, Quaternion.identity);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(indexFingerTip.forward * forceIndex[counter]);
            }

            Destroy(bullet, 5f); // Destroy the sphere after 5 seconds to prevent clutter
            PlayerScript.totalAmmos -= 1;
        }
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false; // Prevent further shooting
        yield return new WaitForSeconds(shootCooldown); // Wait for cooldown duration
        canShoot = true; // Allow shooting again after cooldown
    }

    void OnPistolGestureActivated()
    {
        isPistolGestureActive = true;
    }

    void OnPistolGestureDeactivated()
    {
        //isPistolGestureActive = false;
    }

    void OnShootGestureActivated()
    {
        isShootGestureActive = true;
    }

    void OnShootGestureDeactivated()
    {
        isShootGestureActive = false;
    }


    void OnBunnyGestureActivated()
    {
        counter += 1;
        if ( counter >= prefabList.Count)
        {
            counter = 0;
        }
    }

    private void OnBunnyGestureDeactivated()
    {
        // Nothing here
    }


    void OnDestroy()
    {
        if (pistolGestureSelector != null)
        {
            pistolGestureSelector.WhenSelected -= OnPistolGestureActivated;
            pistolGestureSelector.WhenUnselected -= OnPistolGestureDeactivated;
        }

        if (shootGestureSelector != null)
        {
            shootGestureSelector.WhenSelected -= OnShootGestureActivated;
            shootGestureSelector.WhenUnselected -= OnShootGestureDeactivated;
        }
    }
}
