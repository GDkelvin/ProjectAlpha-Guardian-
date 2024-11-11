using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XayahUltimate : MonoBehaviour
{
    public float energy;
    private float currentEnergy;
    private float cooldownTime;

    private void Start()
    {
        currentEnergy = 0;
        cooldownTime = 0;
    }
    private void Update()
    {
        GainEnergy();   
    }

    private float GainEnergy()
    {
        cooldownTime += Time.deltaTime;
        if(cooldownTime >= 2)
        {
            currentEnergy += 1;
            cooldownTime = 0;
        }
        
        return currentEnergy;
    }

    public void UltAnimation()
    {
        XayahAttack xayahAttack = FindObjectOfType<XayahAttack>();
        xayahAttack.ShootFeatherUlt(-100);
        xayahAttack.ShootFeatherUlt(-102);
        xayahAttack.ShootFeatherUlt(-104);
        xayahAttack.ShootFeatherUlt(-106);
        xayahAttack.ShootFeatherUlt(-108);
        xayahAttack.ShootFeatherUlt(-110);
        xayahAttack.ShootFeatherUlt(-112);
    }

}
