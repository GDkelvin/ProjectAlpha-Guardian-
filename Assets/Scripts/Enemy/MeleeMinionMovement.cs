using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeleeMinionMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    public void Run()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        anim.SetTrigger("run");
    }
}
