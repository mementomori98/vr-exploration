using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiButWorkingThisTime : MonoBehaviour
{
    private Transform target;

    private float range = 10.0f;
    private float speed = 2.0f;

    private float aimRotationSpeed = 10.0f;
    private string playerTargetTag = "Player";

    void Start()
    {
        //Invoke Target update twice per second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * aimRotationSpeed).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.position += direction.normalized * (speed * Time.deltaTime);
    }

    void UpdateTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTargetTag);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= range)
        {
            target = player.transform;
        }
        else
        {
            target = null;
        }
    }

    //Check range of turret
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}