using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomMover : MonoBehaviour
{
    [SerializeField]
    private Vector2 xBounds;
    
    [SerializeField]
    private Vector2 zBounds;

    [SerializeField]
    private Vector2 minMaxResetTime;

    private float timeToReset;

    private NavMeshAgent myAgent;

    private Vector3 targetPosition;


    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(timeToReset <= 0f)
        {
            SetNewLoctation();
        }
        else
        {
            timeToReset -= Time.deltaTime;
        }
        Debug.DrawLine(transform.position, targetPosition, Color.blue);
    }

    private void SetNewLoctation()
    {
        timeToReset = Random.Range(minMaxResetTime.x, minMaxResetTime.y);
        targetPosition = new Vector3(Random.Range(xBounds.x, xBounds.y), 0f,Random.Range(zBounds.x, zBounds.y));
        myAgent.SetDestination(targetPosition);
    }
}
