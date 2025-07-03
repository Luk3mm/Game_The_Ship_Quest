using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyState
    {
        idle,
        preparing,
        follow,
        back
    }

    public Transform player;
    public Transform initialPosition;
    public float speed;
    public float timePreparation;
    public float distanceToStop;

    private Animator anim;
    private Rigidbody2D rig;
    private bool playerInArea = false;
    private bool isPreparing = false;
    private EnemyState currentState = EnemyState.idle;
    private EnemyDamage enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        enemyDamage = GetComponent<EnemyDamage>();
        currentState = EnemyState.idle;
        anim.SetTrigger("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDamage.isDead)
        {
            return;
        }

        switch (currentState)
        {
            case EnemyState.follow:
                if (playerInArea)
                {
                    MoveTo(player.position);
                }
                else
                {
                    BackToInitialPosition();
                }
                break;

            case EnemyState.back:
                MoveTo(initialPosition.position);

                if (Vector2.Distance(transform.position, initialPosition.position) < distanceToStop)
                {
                    currentState = EnemyState.idle;
                    rig.velocity = Vector2.zero;
                    anim.SetBool("follow", false);
                }
                break;
        }
    }

    private void MoveTo(Vector2 destiny)
    {
        Vector2 newPos = Vector2.MoveTowards(rig.position, destiny, speed * Time.deltaTime);
        rig.MovePosition(newPos);
    }

    public void NotifyEntryPlayer()
    {
        if (!isPreparing)
        {
            playerInArea = true;
            StartCoroutine(PrepareAndFollow());
        }
    }

    public void NotifyExitPlayer()
    {
        playerInArea = false;
    }

    IEnumerator PrepareAndFollow()
    {
        isPreparing = true;
        currentState = EnemyState.preparing;
        rig.velocity = Vector2.zero;
        anim.SetTrigger("prepare");

        yield return new WaitForSeconds(timePreparation);

        if (playerInArea)
        {
            currentState = EnemyState.follow;
            anim.SetBool("follow", true);
        }

        isPreparing = false;
    }

    private void BackToInitialPosition()
    {
        currentState = EnemyState.back;
        anim.SetBool("follow", false);
    }

    public string CurrentState()
    {
        return currentState.ToString();
    }
}
