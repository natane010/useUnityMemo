using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainNaviMeshBake : MonoBehaviour
{
    GameObject gameObject;
    Player player;
    //--------------------------------------------
    //navimesh
    NavMeshSurface navMeshSurface;
    NavMeshAgent navMeshAgent;
    //--------------------------------------------
    public void SetNavMeshComponent()
    {
        gameObject = GameObject.Find("Terrain");
        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
    }
    public void SetNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
