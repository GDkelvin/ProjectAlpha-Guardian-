using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStats : MonoBehaviour
{ 
    [SerializeField] private float hp;//Max HP
    [SerializeField] private Image hpBar; //Drag Hp image to this in editor
    private float currentHP;
    

    void Start()
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
}
