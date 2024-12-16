using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int totalAmmos = 0;
    public int totalScore = 0;

    public void resetScore()
    {
        totalScore = 0;
    }
}
