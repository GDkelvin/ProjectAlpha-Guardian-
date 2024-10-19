using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeMinionStats : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] public float hp;//Max HP
    [SerializeField] public Image hpBar; //Drag Hp image to this in editor
    public float currentHP { get; private set; }

    [Header("Defense")]
    [SerializeField] public float def;

    [Header("Attack")]
    [SerializeField] public float atk;

    [SerializeField] public float res;
    [SerializeField] public float atkSpeed;
    [SerializeField] public float movementSpeed;

    private MinionShowStats minionShowStats;
    private GameController gameController;
    float timer;

    //detect Tower
    TowerStats tower;
    private void Awake()
    {
        tower = FindObjectOfType<TowerStats>();
        minionShowStats = FindObjectOfType<MinionShowStats>();
        gameController = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameController.HideMinionStats();
        }
        
    }
    private void OnMouseDown()
    {
        timer = 5f;
        minionShowStats.UpdateStats(this); 
        gameController.ShowMinionStats();
        
    }

    //HP system
    public void SetHP()
    {
        currentHP = hp;
        minionShowStats.UpdateStats(this);
    }

    public void UpdateHP(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0f, hp); //When update hp, this wont make hp overflow the max HP, ex: when healing
        UpdateHPBar();
        minionShowStats.UpdateStats(this);
    }

    public void UpdateHPBar()
    {
        float targetFillAmount = currentHP / hp;
        hpBar.fillAmount = targetFillAmount;
    }

    public void DamageTower()
    {
        tower.UpdateHP(-atk);
    }
    //Def
    

}
