using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{

    private GameObject[] allEnemy;
    private Transform target;
    [SerializeField] private float hideDistance;
    [SerializeField] private GameObject arrowIndicator;
    void Update()
    {
        //target = getClosesEnemy();
        if (allEnemy != null)
        {
            var dir = getClosesEnemy().position - transform.position;

            if (dir.magnitude < hideDistance)
            {
                arrowIndicator.SetActive(false);
            }
            else
            {
                arrowIndicator.SetActive(true);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //arrowIndicator.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }

    public Transform getClosesEnemy()
    {
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        float closesDistance = Mathf.Infinity;  // we can also use Mathf.Infinity
        Transform trans = null;
        if(allEnemy != null)
        {
            foreach (GameObject go in allEnemy)
            {
                float currentDistance;
                currentDistance = Vector3.Distance(transform.position, go.transform.position);
                if (currentDistance < closesDistance)
                {
                    closesDistance = currentDistance;
                    trans = go.transform;
                    return trans;
                }

            }
        }
        
        return trans;
    }
}
