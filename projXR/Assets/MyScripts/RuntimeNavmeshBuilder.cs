using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);
    }

    public void BuildNavMesh()
    {
        StartCoroutine(BuildNavmeshRoutine());
    }

    public IEnumerator BuildNavmeshRoutine()
    {
        if (navMeshSurface != null)
        {
            yield return new WaitForEndOfFrame();
            navMeshSurface.BuildNavMesh();
        }
    }
}
