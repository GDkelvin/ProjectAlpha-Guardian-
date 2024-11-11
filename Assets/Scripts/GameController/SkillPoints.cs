using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    private int currentSkillPoint = 0;
    public float skillPointWaitTime;
    private float currentTime = 0;
    

    public Image[] cooldownSkill;
    public Image[] skillPoint;
    

    private void Start()
    {
        
        foreach (var image in cooldownSkill)
        {
            image.fillAmount = 0f;
        }
    }

    private void Update()
    {
        
        if (currentSkillPoint < cooldownSkill.Length)
        {
            FillSkillPoint();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            UseSkillPoint();
        }
    }
    
    
    private float FillSkillPoint()
    {
        currentTime += Time.deltaTime;
        float fillAmount = Mathf.Clamp01(currentTime / skillPointWaitTime);
        cooldownSkill[currentSkillPoint].fillAmount = fillAmount;
        if(fillAmount >= 1)
        {
            cooldownSkill[currentSkillPoint].gameObject.SetActive(false);
            skillPoint[currentSkillPoint].gameObject.SetActive(true);

            currentSkillPoint++;
            currentTime = 0;
        }
        
        return currentSkillPoint;
    }
    public void UseSkillPoint()
    {
        
        if (currentSkillPoint > 0)
        {

            currentSkillPoint--;
            skillPoint[currentSkillPoint].gameObject.SetActive(false);
            cooldownSkill[currentSkillPoint].gameObject.SetActive(true);
            if (currentSkillPoint < cooldownSkill.Length - 1)
            {
                float nextFillAmount = cooldownSkill[currentSkillPoint + 1].fillAmount;

                cooldownSkill[currentSkillPoint].fillAmount = nextFillAmount;

                cooldownSkill[currentSkillPoint + 1].fillAmount = 0f;

                currentTime = nextFillAmount * skillPointWaitTime;
            }

            
            Debug.Log("Skill point used. Remaining points: " + currentSkillPoint);
        }
        else
        {
            Debug.Log("No skill points available to use.");
        }
    }
}
