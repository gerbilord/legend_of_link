﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRaidus;
    public float attackRadius;
    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();       
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, this.transform.position) <= chaseRaidus
            && Vector3.Distance(target.position, this.transform.position) >= attackRadius)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                                                          target.position,
                                                          this.moveSpeed * Time.deltaTime);

        }
    }
}
