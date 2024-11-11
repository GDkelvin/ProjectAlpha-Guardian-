using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject characterPrefab;  
    public Vector2 spawnLocation = new Vector2(-8, 1.11f);
    public ChangeBorder spawn;
    public GameObject currentCharacter;
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    public bool isMoving = false;
    public LayerMask groundLayer;
    public Animator anim;

    public void Spawn()
    {
        if (currentCharacter != null)
        {
            Debug.Log("Xayah already exists");
            return; 
        }

        currentCharacter = Instantiate(characterPrefab, spawnLocation, Quaternion.identity);
        anim = currentCharacter.GetComponent<Animator>();
    }

    void Update()
    {
        spawn = FindObjectOfType<ChangeBorder>();
        
        if (spawn.selected == true)
        {
            if (Input.GetKeyDown(KeyCode.P)) Spawn();

            if (Input.GetMouseButtonDown(1))
            {
                SetTargetPosition();
            }
        }

        if (isMoving && currentCharacter != null)
        {
            MoveCharacterToTarget();
            if (currentCharacter.GetComponent<XayahAttack>() != null)
            {
                currentCharacter.GetComponent<XayahAttack>().StopAttacking();
            }
        }
        else if (currentCharacter != null)
        {
            anim.SetFloat("Movement", 0f);  
        }
    }

    void SetTargetPosition()
    {
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;  
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.down, Mathf.Infinity, groundLayer);

        if (hit.collider != null)
        {
            targetPosition = new Vector3(mouseWorldPos.x, hit.point.y + 1, 0);
            Debug.Log("Target Position: " + targetPosition);
        }
        FaceTarget(targetPosition);
        isMoving = true;

        anim.SetFloat("Movement", Mathf.Abs(transform.position.x - targetPosition.x));
    }

  

    void MoveCharacterToTarget()
    {
        currentCharacter.transform.position = Vector3.MoveTowards(
            currentCharacter.transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime 
        );

        float distanceToTarget = Mathf.Abs(currentCharacter.transform.position.x - targetPosition.x);
        anim.SetFloat("Movement", distanceToTarget);

        if (Vector3.Distance(currentCharacter.transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            anim.SetFloat("Movement", 0f);  
        }
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 direction = target - currentCharacter.transform.position;
        if (direction.x > 0)
        {
            currentCharacter.transform.localScale = new Vector3(1, 1, 1);  
        }
        else
        {
            currentCharacter.transform.localScale = new Vector3(-1, 1, 1); 
        }
    }
}
