using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask DetectEnemy;
    public LayerMask GroundLayer;
    public int angle;

    private bool isMoving = true;
    private bool firstHit = true; //xem minion nào ăn dmg trước

    

    //List để lưu những minion đã ăn dmg
    private List<MeleeMinionStats> damagedMinions = new List<MeleeMinionStats>();

    XayahAttack xayah;

    private void Awake()
    {
        xayah = FindObjectOfType<XayahAttack>();

    }
    private void Start()
    {
        //i will fix this
        //transform.rotation = Quaternion.Euler(0, 0, angle);
        Vector3 scale = transform.localScale;
        scale.y = -Mathf.Abs(scale.y); // Ensure the y-scale is negative
        transform.localScale = scale;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, DetectEnemy);
            RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, 0.6f, transform.up, distance, DetectEnemy); // Adjust the radius (0.5f) as necessary
            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Enemy"))
            {
                //Minion
                //Lấy box collider của từng minion và kiểm tra minion đã ăn damage chưa
                //Mỗi minion chỉ ăn damage trong 1 frame
                MeleeMinionStats minion = hitInfo.collider.GetComponent<MeleeMinionStats>();
                if (minion != null && !damagedMinions.Contains(minion))
                {
                    if (firstHit)
                    {
                        xayah.NormalAttack(minion, 0.9f);
                        firstHit = false;
                    }
                    else
                    {
                        xayah.NormalAttack(minion, 0.45f);
                    }
                    damagedMinions.Add(minion);
                    
                }
            }

            //check if hit ground
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, distance, GroundLayer);
            if (groundHit.collider != null)
            {
                isMoving = false;
            }

            //Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            //transform.Translate(direction * speed * Time.deltaTime);


        }

    }

    public void Withdraw()
    {
        if (xayah != null)
        {
            Vector3 targetPosition = xayah.transform.position;
            targetPosition.y -= 0.5f; // Subtract 1 from the y position
            //Debug.Log($"Feather is withdrawing to Xayah's position: {targetPosition}");

            StartCoroutine(MoveToTarget(targetPosition, 0.2f)); 
        }
    }

    private IEnumerator MoveToTarget(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Destroy the feather after moving
    }

    public List<MeleeMinionStats> GetDamagedMinions()
    {
        return damagedMinions;
    }

    //Use later
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
 