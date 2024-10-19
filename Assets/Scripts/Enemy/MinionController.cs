using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    MeleeMinionMovement meleeMinionMovement;
    MeleeMinionStats meleeMinionStats;

    private Animator anim;
    private int attackCount = 1;

    //Raycast
    [SerializeField] private GameObject raycast;
    [SerializeField] private float raycastDistance;
    private bool isAttacking = false;


    private void Awake()
    {
        meleeMinionMovement = GetComponent<MeleeMinionMovement>();
        meleeMinionStats = GetComponent<MeleeMinionStats>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        meleeMinionStats.SetHP();

    }

    private void Update()
    {
        meleeMinionMovement.Run();
        
        RaycastHit2D hit = Physics2D.Raycast(raycast.transform.position, Vector2.left, raycastDistance);
        if (hit.collider != null && hit.collider.CompareTag("Tower"))
        {
            //Detected
            Debug.DrawRay(raycast.transform.position, Vector2.left * hit.distance, Color.red);
            if (!isAttacking)
            {
                isAttacking = true;
                Attack();
            }
        }
        else
        {
            //Not detected
            isAttacking = false;
            Debug.DrawRay(raycast.transform.position, Vector2.left * raycastDistance, Color.green);

        }
        
    }

    private void Attack()
    {
        Debug.Log(attackCount);
        anim.SetTrigger($"attack_{attackCount}");
        
    }

    public void AttackAnimationEnd()
    {
        if (attackCount == 3)
        {
            attackCount = 0;
        }
        attackCount++;
        isAttacking = false;
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 1);
    }
    */
}
