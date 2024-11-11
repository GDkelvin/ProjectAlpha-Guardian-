using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPoint : MonoBehaviour
{
    public Image[] skillPointImages; 
    public Animator[] skillPointAnimators;
    public Sprite filledSkillPointSprites;

    private int currentSkillPoint = 0;

    public float duration;
    private float currentTime;
    

    void Start()
    {
        // Make sure all skill points are empty at the start
        foreach (var image in skillPointImages)
        {
            image.fillAmount = 0f;
            image.sprite = null;
        }
    }
    private void Update()
    {
        if (currentSkillPoint < skillPointImages.Length)
        {
            FillSkillPoint();
        }

        
    }

    private void FillSkillPoint()
    {
        currentTime += Time.deltaTime;  
        float fillAmount = Mathf.Clamp01(currentTime / duration);
        Debug.Log($"Fill Amount: {fillAmount}");

        if (fillAmount >= 1f)
        {
            
            if (filledSkillPointSprites == null)
            {
                Debug.LogError("Filled Skill Point Sprite is not assigned!");
            }
            skillPointImages[currentSkillPoint].sprite = filledSkillPointSprites;
            
            if (skillPointAnimators.Length > currentSkillPoint && skillPointAnimators[currentSkillPoint] != null)
            {
                skillPointAnimators[currentSkillPoint].SetTrigger("SkillPoint1");
            }

            currentSkillPoint++;
            currentTime = 0f;  
        }
        else
        {
            // Update fillAmount during the filling process
            skillPointImages[currentSkillPoint].fillAmount = fillAmount;
        }
    }


}
