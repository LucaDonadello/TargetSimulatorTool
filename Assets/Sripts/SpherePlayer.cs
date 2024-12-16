using System.Collections;
using TMPro;
using UnityEngine;

public class SphereAudioPlayer : MonoBehaviour
{
    private AudioSource audioPlay;
    private GameObject Player;
    private GameObject Target;
    Player playerScript;
    TextMeshProUGUI totalScoreText;


    void Start()
    {
        audioPlay = GameObject.FindGameObjectWithTag("AudioSourceTag").GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<Player>();
        totalScoreText = GameObject.Find("TotalScore").GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Target = collision.gameObject;
            if (Target.name == "Target1" || Target.name == "Target4" || Target.name == "Target5")
            {
                playerScript.totalScore += 10;
            }
            if (Target.name == "Target2")
            {
                playerScript.totalScore += 50;
            }
            if(Target.name == "Target3")
            {
                playerScript.totalScore += 5;
            }
            if (audioPlay != null)
            {
                audioPlay.Play();
                StartCoroutine(StopAudioSource(2f));
            }
        }
    }

    private void Update()
    {
        totalScoreText.text = "Total Points: " + playerScript.totalScore.ToString();
    }

    private IEnumerator StopAudioSource(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioPlay.Stop();
    }
}
