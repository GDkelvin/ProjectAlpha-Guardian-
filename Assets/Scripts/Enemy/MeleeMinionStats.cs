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

    public bool isAlive = true;
    
    private MinionShowStats minionShowStats;
    private GameController gameController;
    

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
    }
    
    private void OnMouseDown()
    {
        if (currentHP > 0)
        {
            gameController.ShowMinionStats(this);
            minionShowStats.UpdateStats(this);
        }
    }

    //HP system
    public void SetHP()
    {
        currentHP = hp;
        minionShowStats.UpdateStats(this);
    }

    public void UpdateHP(float amount)
    {
        if (!isAlive) return;

        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0f, hp); //When update hp, this wont make hp overflow the max HP, ex: when healing
        UpdateHPBar();

        if (gameController.GetSelectedMinion() == this)
        {
            minionShowStats.UpdateStats(this);
        }

        if (currentHP <= 0f)
        {
            isAlive = false;
            if (gameController.GetSelectedMinion() == this)
            {
                gameController.HideMinionStats();
            }
            Destroy(gameObject); 
        }
    }

    public void UpdateHPBar()
    {
        float targetFillAmount = currentHP / hp;
        hpBar.fillAmount = targetFillAmount;
    }

    public void DamageTower()
    {
        if (!isAlive) return;
        tower.UpdateHP(-atk);
    }
    //Def
    
    public float ReducedDmg()
    {
        float G, H; //H is dmg received, G is dmg reduced
        G = def / (def + 100);
        H = 1 - G;
        return H;
    }

    //Trong thương
    public float Vul()
    {
        float vul = 2f; //Trong thương 25%
        return vul;
    }

    //Miễn st
    public float Res()
    {
        float r = 1 - res; //st nhận : 0.75
        return r;
    }

}
