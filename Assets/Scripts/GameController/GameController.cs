using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject showStats;
    MinionShowStats minionStatsDisplay;
    private void Awake()
    {
        minionStatsDisplay = FindObjectOfType<MinionShowStats>();
    }
    private void Start()
    {
        showStats.SetActive(false);
    }

    public void ShowMinionStats()
    {
        showStats.SetActive(true);
    }

    public void HideMinionStats()
    {
        showStats.SetActive(false);
    }

    
}
