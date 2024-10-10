using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeMinionStats : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private float hp;//Max HP
    [SerializeField] private Image hpBar; //Drag Hp image to this in editor
    private float currentHP;

    [Header("Def")]
    [SerializeField] private float def;
    [SerializeField] private float atk;
    [SerializeField] private float res;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float movementSpeed;

    //detect Tower
    TowerStats tower;
    private void Awake()
    {
        tower = FindObjectOfType<TowerStats>();
    }
    //HP system
    public void SetHP()
    {
        currentHP = hp;
    }

    public void UpdateHP(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0f, hp); //When update hp, this wont make hp overflow the max HP, ex: when healing
        UpdateHPBar();
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
