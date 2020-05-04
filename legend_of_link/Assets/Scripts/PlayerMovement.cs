using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = GetAxisBinary("Horizontal");
        change.y = GetAxisBinary("Vertical");

        MoveCharacter();
    }

    void UpdateAnimator()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

    }

    void MoveCharacter()
    {
     
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );

        UpdateAnimator();
    }

    float GetAxisBinary(string dir)
    {
        float value = Input.GetAxisRaw(dir);
        float sensitivity = .15F;

        if (value > sensitivity)
        {
            return 1;
        }
        else if(value < sensitivity * -1.0F)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
