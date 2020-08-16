using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRaidus;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        this.currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        myRigidbody = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Called once per physics update
    void FixedUpdate()
    {
        CheckDistance();       
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, this.transform.position) <= chaseRaidus
            && Vector3.Distance(target.position, this.transform.position) >= attackRadius
            && (currentState == EnemyState.idle || currentState == EnemyState.walk ))
        {

            Vector3 temp = Vector3.MoveTowards(this.transform.position,
                                                            target.position,
                                                            this.moveSpeed * Time.deltaTime);

            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("wakeUp", true);
        }
        else if (Vector3.Distance(target.position, this.transform.position) > chaseRaidus)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            if (direction.x >= 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else
        {
            if (direction.y >= 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else
            {
                SetAnimFloat(Vector2.down);
            }
        }

    }
    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState) // bad for animator to change state a bunch
        {
            currentState = newState;
        }
    }
}
