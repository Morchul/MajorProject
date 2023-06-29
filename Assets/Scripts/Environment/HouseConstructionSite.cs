using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseConstructionSite : SmartObject
{
    [SerializeField]
    private House housePrefab;

    protected override void Awake()
    {
        base.Awake();

        GetComponent<BuildComponent>(ComponentIDs.BUILD).OnBuildFinished += BuildFinished;
    }

    public void BuildFinished()
    {
        Instantiate(housePrefab, transform.position, Quaternion.identity);
    }
}
