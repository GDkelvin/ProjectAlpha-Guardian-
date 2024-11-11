using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XayahStats : MonoBehaviour
{
    public float hp;
    public Image hpBar;
    public float currentHP;

    public float def;
    public float atk;
    public float atkSpeed;
    public float movementSpeed;
    public float res;

    private void Start()
    {
        SetHP();
    }

    public void SetHP()
    {
        currentHP = hp;
    }

    public void UpdateHP(float amount) 
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0f, hp); //When update hp, this wont make hp overflow the max HP, ex: when healing
        UpdateHPBar();
        
        if (currentHP <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHPBar()
    {
        float targetFillAmount = currentHP / hp;
        hpBar.fillAmount = targetFillAmount;
    }

    public void Death()
    {
        Destroy(gameObject);
        SpawnCharacter spawnCharacterScript = FindObjectOfType<SpawnCharacter>();
        if (spawnCharacterScript != null)
        {
            spawnCharacterScript.currentCharacter = null;  // Reset the reference in the SpawnCharacter script
        }
    }
}
