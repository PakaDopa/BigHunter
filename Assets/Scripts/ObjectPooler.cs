using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject poolPrefab;
    [SerializeField] private int maxSize = 256;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Transform parentTransform;
    public Transform ParentTransform { get { return parentTransform; } }
    private Queue<GameObject> poolQueue;
    
    private void Awake()
    {
        poolQueue = new();
    }
    public void GetBack(GameObject prefab)
    {
        // 반납해야 할 것을 다시 큐에 넣음
        prefab.SetActive(false);
        if(poolQueue.Count < maxSize)
            poolQueue.Enqueue(prefab);
    }
    public GameObject Rent()
    {
        if (poolPrefab == null)
            return default;

        GameObject obj;
        //풀에 있으면 개를 빼서 줌
        if (poolQueue.Count > 0)
            obj = poolQueue.Dequeue();
        //풀에 없으면 생성해서줌
        else
            obj = Instantiate(poolPrefab, spawnTransform);

        obj.transform.SetPositionAndRotation(spawnTransform.transform.position, Quaternion.identity);
        return obj;
    }
}
