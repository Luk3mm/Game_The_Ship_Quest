using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float maxDistance;

    private Vector2 direction;
    private Vector2 originPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        float traveledDistance = Vector2.Distance(originPoint, transform.position);

        if(traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 initialDirection)
    {
        direction = initialDirection.normalized;
        originPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDamage enemy = collision.GetComponent<EnemyDamage>();
            if(enemy != null)
            {
                enemy.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
