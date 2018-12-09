using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Transform target;
    float speed = 1.5f;
    //[SerializeField]
    Vector2[] path;
    int targetIndex;
    bool hasRequestedPath = false;
    public StateController stateController;

    private void Start()
    {
        target = GameManager.instance.player.transform;
        stateController = GetComponent<StateController>();
    }

    public void RequestPath()
    {
        if(!hasRequestedPath){
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            hasRequestedPath = true;
        }
    }

    public void StopMovement()
    {
        StopCoroutine("FollowPath");
        hasRequestedPath = false;
    }

    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        hasRequestedPath = false;
        if (pathSuccessful && gameObject.activeSelf)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {

        if (path.Length == 0) StopCoroutine(FollowPath());
        Vector2 curWaypoint = path[0];

        while (true)
        {
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y),curWaypoint) <= 0.2f)
            {
                targetIndex++;
                if (targetIndex >= path.Length) yield break;
                curWaypoint = path[targetIndex];
            }
            stateController.moveVec = curWaypoint - (Vector2)transform.position;
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(path[i], 0.1f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else Gizmos.DrawLine(path[i-1], path[i]);
            }
        }
    }
}
