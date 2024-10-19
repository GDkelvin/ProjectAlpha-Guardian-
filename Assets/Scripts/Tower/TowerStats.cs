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
        if (this == null)
        {
            return;
        }
            
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0f, hp); //When update hp, this wont make hp overflow the max HP, ex: when healing
        
        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateHPBar();
        }
    }

   
    public void UpdateHPBar()
    {
        if (this != null && hpBar != null)
        {
            float targetFillAmount = currentHP / hp;
            hpBar.fillAmount = targetFillAmount;
        }
    }
}
