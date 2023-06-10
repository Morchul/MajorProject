using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ComponentPerformanceTest
{

    private const int testIteractions = 1000;
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [Test, Performance]
    public void UnityGetComponentPerformanceTest()
    {
        Human human = GameObject.Find("Human").GetComponent<Human>();

        Measure.Method(
            () => { GetComponentUnity(human); })
            .Run();
    }

    [Test, Performance]
    public void NewGetComponentPerformanceTest()
    {
        Human human = GameObject.Find("Human").GetComponent<Human>();

        Measure.Method(
            () => { GetComponentNew(human); })
            .Run();
    }

    private void GetComponentNew(Human human)
    {
        for (int i = 0; i < testIteractions; ++i)
        {
            human.GetComponent<MoveComponent>(ComponentIDs.MOVE);
        }
    }

    private void GetComponentUnity(Human human)
    {
        for (int i = 0; i < testIteractions; ++i)
        {
            human.GetComponent<MeshRenderer>();
        }
    }
}
