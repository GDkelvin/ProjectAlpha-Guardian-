using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeMinionMovement : MonoBehaviour
{
    private MeleeMinionStats meleeMinionStats;
    private Animator anim;
    
    private void Awake()
    {
        meleeMinionStats = FindObjectOfType<MeleeMinionStats>();
        anim = GetComponent<Animator>();
    }
    
    public void Run()
    {
        transform.position += Vector3.left * meleeMinionStats.movementSpeed * Time.deltaTime;
        anim.SetTrigger("run");
    }
}
