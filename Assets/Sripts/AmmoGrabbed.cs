using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine.UI;
using TMPro;

public class AmmoGrabbed : MonoBehaviour
{
    public GameObject Ammo;
    private GameObject Player;
    Player playerScript;
    private HandGrabInteractable handGrabInteractable;
    private Vector3 initialPosition;
    private float grabTime;
    private bool isGrabbed = false;
    TextMeshProUGUI totalAmmosText;

    void Start()
    {
        handGrabInteractable = GetComponent<HandGrabInteractable>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<Player>();
        totalAmmosText = GameObject.Find("TotalAmmos").GetComponent<TextMeshProUGUI>();

        if (handGrabInteractable != null)
        {
            handGrabInteractable.WhenInteractorViewAdded += OnGrabbed;
            handGrabInteractable.WhenInteractorViewRemoved += OnReleased;
        }
        else
        {
            Debug.LogError("HandGrabInteractable component not found!");
        }
    }

    private void OnGrabbed(IInteractorView interactor)
    {
        Debug.Log("Ammo grabbed!");
        initialPosition = Ammo.transform.position;
        grabTime = Time.time;
        isGrabbed = true;
    }

    private void OnReleased(IInteractorView interactor)
    {
        Debug.Log("Ammo released!");
        isGrabbed = false;
    }

    void Update()
    {
        if (isGrabbed && Time.time - grabTime >= 3f)
        {
            if (Vector3.Distance(Ammo.transform.position, initialPosition) > 0.01f)
            {
                Debug.Log("Destroying Ammo");
                playerScript.totalAmmos += 7;
                Destroy(Ammo);
                isGrabbed = false;
                totalAmmosText.text = "Total  Shots: " + playerScript.totalAmmos.ToString();
            }
        }
    }

    void OnDestroy()
    {
        if (handGrabInteractable != null)
        {
            handGrabInteractable.WhenInteractorViewAdded -= OnGrabbed;
            handGrabInteractable.WhenInteractorViewRemoved -= OnReleased;
        }
    }
}
