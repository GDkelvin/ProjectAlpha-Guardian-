using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    MeleeMinionMovement meleeMinionMovement;
    MeleeMinionStats meleeMinionStats;

    

    private Animator anim; 

    

    //Raycast
    [SerializeField] private GameObject raycast;
    [SerializeField] private float raycastDistance;
    private float characterDirection = -1;


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

        if (hit.collider != null)
        {
            //Detected
            Debug.DrawRay(raycast.transform.position, Vector2.left * hit.distance, Color.red);
            anim.SetTrigger("attack_1");
        }
        else if(hit.collider == null)
        {
            //Not detected
            Debug.DrawRay(raycast.transform.position, Vector2.left * raycastDistance, Color.green);
        }
    }

    
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 1);
    }
    */
}
