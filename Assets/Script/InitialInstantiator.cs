
using UnityEngine;

public class InitialInstantiator : MonoBehaviour
{
    [SerializeField]private GameObject[] Envirements;
    private void Start()
    {
        int index = Random.Range(0, Envirements.Length);
        Envirements[index].SetActive(true);
    }

}
