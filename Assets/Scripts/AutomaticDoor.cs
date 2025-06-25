using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Collider2D doorCollider;

    private int playerInArea = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        anim.SetBool("open", false);
        doorCollider.enabled = true;
    }

    private void OpenDoor()
    {
        anim.SetBool("open", true);
        doorCollider.enabled = false;
    }

    private void CloseDoor()
    {
        anim.SetBool("open", false);
        doorCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea++;
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea--;

            if(playerInArea <= 0)
            {
                CloseDoor();
            }
        }
    }
}
