using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    public int workerCost;
    public Transform workerParent;
    public ResourceCount resourceCount;

    public void SpawnWorkerClick(GameObject worker)
    {
        //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

        //Vector3 adjustZ = new Vector3(worldPoint.x, worldPoint.y, 0);
        
        //Spawn(adjustZ, worker);
        resourceCount = FindObjectOfType<ResourceCount>().GetComponent<ResourceCount>();
        Spawn(worker);
    }

    private void Spawn(GameObject worker)
    {
        if (resourceCount.numCoins >= workerCost)
        {
            Instantiate(worker, Vector3.zero, Quaternion.identity, workerParent);
            resourceCount.numCoins -= workerCost;
        }
    }
}
