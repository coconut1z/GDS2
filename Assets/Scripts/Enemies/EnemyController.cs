﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public NavMeshAgent agent;

    public bool isMovingToAttack;
    public bool isAttacking;

    public int health;
    public int attack;
    public float attackTimer;
    public float timeBetweenAttacks;
    public GameObject town;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start () {
        
    }

	void Update () {
        if (isMovingToAttack) {
            if (!agent.pathPending) {
                if (new Vector3(agent.destination.x - transform.position.x, 0f, agent.destination.z - transform.position.z).sqrMagnitude <= agent.stoppingDistance) {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                        isMovingToAttack = false;
                        isAttacking = true;
                    }
                }
            }
        }

        if (isAttacking) {
            attackTimer += Time.deltaTime;

            if (attackTimer >= timeBetweenAttacks) {
                BaseController._instance.TakeDamage(attack);
                attackTimer -= timeBetweenAttacks;
            }
        }

        this.transform.LookAt(new Vector3(BaseController._instance.transform.position.x, 0.5f, BaseController._instance.transform.position.z));
    }

    public void SetStats(float threatLevel, float difficulty) {
        attack = (int)Math.Round((attack * threatLevel * difficulty), MidpointRounding.AwayFromZero);
        health = (int)Math.Round((health * threatLevel * difficulty), MidpointRounding.AwayFromZero);
    }

    public void MoveToAttack() {
        Vector3 basePos = BaseController._instance.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(Vector3.Lerp(basePos, transform.position, 0.05f), out hit, 4f, NavMesh.AllAreas)) {
            agent.destination = hit.position;
        }
        isMovingToAttack = true;
    }

    public bool IsDeadAfterDamage(int amt) {
        health -= amt;
        health = Mathf.Clamp(health, 0, int.MaxValue);
        return health == 0;
    }
}
