using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyAITD : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private float targetRange = 7f;

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
    private bool searchingForPlayer = false;

    //-------------Enemy Attacking
    private float NextTimeToFire = 0f;
    private Rigidbody2D rbEnemyRef;
    [SerializeField] private float TimeBetweenShoot = 1f;
    [SerializeField] private float shootSpeed = 7f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform EFirePoint;
    private Vector3 lookDir;
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchPlayer());
            }

            return;
        }

        // Start a new path to the target position, return the rusult to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator SearchPlayer()
    {
        GameObject new_Target = GameObject.FindGameObjectWithTag("Player");
        if(new_Target == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchPlayer());
        }
        else
        {
            target = new_Target.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath()); // update path again
            yield break;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchPlayer());
            }

           yield break;
        }

        // Start a new path to the target position, return the rusult to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
       
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
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchPlayer());
            }

            return;
        }

        // Always look at the player
        if (Vector3.Distance(target.position, transform.position) < targetRange)
        {
            lookDir = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }

        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;

        //  Move the AI and Fire to the Player
        if( Vector3.Distance(target.position , transform.position) < targetRange)
        {
            rb.AddForce(dir, fMode);
            
            Attack();

        }


        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if(dist < nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    private void Attack()
    {

        if (Time.time > NextTimeToFire)
        {
            
            GameObject newBullet = Instantiate(bulletPrefab, EFirePoint.position, EFirePoint.rotation);

            rbEnemyRef = newBullet.GetComponent<Rigidbody2D>();
            rbEnemyRef.AddForce(shootSpeed * Time.fixedDeltaTime * EFirePoint.up, ForceMode2D.Impulse);

            Destroy(newBullet, 0.5f);

            NextTimeToFire = Time.time + TimeBetweenShoot;
        }
    }
}
