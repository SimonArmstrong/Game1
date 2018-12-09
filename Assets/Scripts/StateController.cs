using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public Transform waypoints;
    public State currentState;
    public FieldOfView fov;
    public EnemyStats enemyStats;
    [HideInInspector]
    public Transform eyes;
    public State remainState;

    [HideInInspector] public Transform player;


    //[HideInInspector]
    public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector2 moveVec;


    private bool aiActive;

    public Unit unit;


    void Start()
    {
        unit = GetComponent<Unit>();
        if (waypoints == null) return;
        enemyStats = GetComponent<EnemyStats>();
        foreach(Transform point in waypoints)
        {
            wayPointList.Add(point);
        }
        SetupAI(true, GameManager.instance.player.transform);
    }

    public void SetupAI(bool aiActivationFromEnemyManager, Transform playerTransform)
    {

        player = playerTransform;
        aiActive = aiActivationFromEnemyManager;
        if (aiActive)
        {
        }
        else
        {
        }
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, enemyStats.lookSphereCastRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}