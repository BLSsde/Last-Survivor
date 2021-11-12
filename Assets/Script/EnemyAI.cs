using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    // how many times we update our path in a second
    public float updateRate = 2f;

    // Caching
    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;    // calculated path
    public float Speed = 300f;  // The AI's Speed per second
    public ForceMode2D fMode;
    [HideInInspector] public bool pathIsEnded = false;

    //the max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWayPointDistance = 3f;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            Debug.LogError("No player found? PANIC!");
            return;
        }

        // Start a new path to the target position, return the rusult to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if( target == null)
        {
            //TODO: insert a player search here
            yield break;
        }

        // Start a new path to the target position, return the rusult to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path. Did it have an error " + p.error);
        if(! p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            //TODO: insert a player search here
            return;
        }

        // TODO: Always look at the player

        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;

        //  Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if(dist < nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
