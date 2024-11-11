using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XayahAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    private MeleeMinionStats minion;

    private Animator anim;
    private int attackCount = 1;

    private XayahStats xayahStats;
    

    //Raycast
    [SerializeField] private GameObject raycast;
    [SerializeField] private float raycastDistance;
    private bool isAttacking = false;
    public LayerMask DetectEnemy;

    //Cooldown
    private float timeCoolDown;
    public float startCooldown;

    private void Awake()
    {
        minion = FindObjectOfType<MeleeMinionStats>();
        xayahStats = FindObjectOfType<XayahStats>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        
        timeCoolDown -= Time.deltaTime;
        
        RaycastHit2D hit = Physics2D.Raycast(raycast.transform.position, Vector2.right, raycastDistance, DetectEnemy);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                //Detected
                Debug.DrawRay(raycast.transform.position, Vector2.right * hit.distance, Color.red);
                if (!isAttacking && timeCoolDown <= 0)
                {
                    isAttacking = true;
                    Attack();
                }
            }
        }
        else
        {
            //Not detected
            isAttacking = false;
            Debug.DrawRay(raycast.transform.position, Vector2.right * raycastDistance, Color.green);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.ResetTrigger($"a{attackCount}");
            anim.SetTrigger("Skill");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("Ult");
        }
    }
    public void StartCooldownAfterSpawn()
    {
        timeCoolDown = startCooldown;
    }
    //Trigger animation
    public void Attack()
    {
        if (timeCoolDown <= 0)
        {
            anim.SetTrigger($"a{attackCount}");
            timeCoolDown = startCooldown;
            //Debug.Log($"Attack {attackCount} performed, cooldown reset to {startCooldown}s");
            //Debug.Log(countFeather);

        }
    }
    
    public void ShootFeather(float angle)
    {
        GameObject feather = Instantiate(projectile, shotPoint.position, Quaternion.Euler(0, 0, angle));
    }

    public void ShootFeatherUlt(float angle)
    {
        Vector3 shotPointOffset = new Vector3(-0.2f, 0.5f, 0);
        GameObject feather = Instantiate(projectile, shotPoint.position + shotPointOffset, Quaternion.Euler(0, 0, angle));
    }

    public void SkillAnimation()
    {
        ShootFeather(-97f);
        ShootFeather(-100);
    }

    public void NormalAttackAnimation()
    {
        ShootFeather(-98);
    }

    public void AttackAnimationEnd()
    {
        isAttacking = false;
        if (attackCount >= 2)
        {
            attackCount = 0;
        }
        attackCount++;
    }

    public void ChangeAttackStateAndWithdrawFeather() //Add this for skill and ult
    {
        isAttacking = false;
        WithdrawFeathers();
    }

    //Deal damage
    public void NormalAttack(MeleeMinionStats selectedMinion, float multiplier)
    {
        
        if (selectedMinion.currentHP > 0)
        {
            //Get attack from xayah
            float atk = xayahStats.atk;
            float OriginalDamage = atk * multiplier; //dmg gốc

            float finalizedDmg = OriginalDamage * selectedMinion.ReducedDmg() * (selectedMinion.Vul() - selectedMinion.Res());

            selectedMinion.UpdateHP(-finalizedDmg);
            //Debug.Log($"Dealt {finalizedDmg} damage to {selectedMinion.name}");
        }
    }

    private void WithdrawFeathers()
    {
        Feather[] feathers = FindObjectsOfType<Feather>();

        foreach (Feather feather in feathers)
        {
            List<MeleeMinionStats> hitMinions = feather.GetDamagedMinions();

            foreach (MeleeMinionStats minion in hitMinions)
            {
                if (minion.currentHP > 0)
                {
                    // Apply 60% damage when withdrawing feathers
                    float atk = xayahStats.atk;
                    float damage = atk * 0.6f;
                    minion.UpdateHP(-damage);
                    //Debug.Log($"Dealt {damage} damage to {minion.name}");
                }
            }

            feather.Withdraw(); 
        }
        //ApplyAreaDamage();
    }

    //Tính dmg tổng sau khi có tất cả buff hoặc neft
    public float FinalizedDmg()
    {

        return 0;
    }

    public void StopAttacking()
    {
        isAttacking = false;
        anim.ResetTrigger($"a{attackCount}");  // Reset the current attack trigger
                                               // You can add any additional logic to handle stopping the attack animation here.
    }

}
 