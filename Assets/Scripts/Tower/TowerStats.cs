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

    private void Update()
    {
        Vector3 position = transform.position;

        // Get camera bounds
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Clamp the position to stay within the camera bounds
        position.x = Mathf.Clamp(position.x, -cameraWidth / 2, cameraWidth / 2);
        position.y = Mathf.Clamp(position.y, -cameraHeight / 2, cameraHeight / 2);

        transform.position = position;
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
