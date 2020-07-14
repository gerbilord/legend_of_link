using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = GetAxisBinary("Horizontal");
        change.y = GetAxisBinary("Vertical");

        if(Input.GetButtonDown("melee attack")  && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());

        }
        else if (currentState == PlayerState.walk)
        {
            MoveCharacter();
        }

    }

    private IEnumerator AttackCo()
    {
        currentState = PlayerState.attack;
        animator.SetBool("attacking", true);

        yield return null; // Wait 1 frame. (Don't want to restart attacking)

        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(.3f); // .33f is length of attack animation
        currentState = PlayerState.walk;
  
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
        else if (value < sensitivity * -1.0F)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
