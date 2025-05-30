using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject poolPrefab;
    private Queue<GameObject> poolQueue;

    private void Awake()
    {
        poolQueue = new();

    }

    public void Rent()
    {

    }
}
