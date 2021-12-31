using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineTargetSet : MonoBehaviour
{
    private CinemachineVirtualCamera CMcam;
    private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        CMcam = GetComponent<CinemachineVirtualCamera>();
        
    }
    private void Start()
    {
        if(target == null)
        {
            StartCoroutine(SearchPlayer());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
        {
            return;
        }
        else
        {
            StartCoroutine(SearchPlayer());
        }
        
    }

    IEnumerator SearchPlayer()
    {
        GameObject new_Target = GameObject.FindGameObjectWithTag("Player");
        if (new_Target == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchPlayer());
        }
        else
        {
            target = new_Target.transform;
            CMcam.m_Follow = target;
            yield break;
        }
    }
}
