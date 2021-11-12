using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 230;
    
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, 1);
    }
}
