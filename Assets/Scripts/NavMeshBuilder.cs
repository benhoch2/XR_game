using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class NavMeshBuilder : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface component not found on this GameObject.");
            return;
        }
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);
        
    }

    public void BuildNavMesh()
    {
        StartCoroutine(BuildNavMeshRoutine());
    }

    public IEnumerator BuildNavMeshRoutine()
    {
        yield return new WaitForEndOfFrame();
        navMeshSurface.BuildNavMesh();
    }
}
