using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject showStats;
    private MeleeMinionStats selectedMinion;

    
    private XayahMovement xayahMovement;

    private void Awake()
    {
        
    }
    private void Start()
    {
        showStats.SetActive(false);

        xayahMovement = FindObjectOfType<XayahMovement>();
    }

    private void Update()
    {
        
    }

    public void ShowMinionStats(MeleeMinionStats minion)
    {
        selectedMinion = minion;
        showStats.SetActive(true);
    }

    public void HideMinionStats()
    {
        showStats.SetActive(false);
    }

    public MeleeMinionStats GetSelectedMinion()
    {
        return selectedMinion;
    }

    

    
}
