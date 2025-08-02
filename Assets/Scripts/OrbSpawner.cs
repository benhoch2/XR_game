using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class OrbSpawner : MonoBehaviour
{
    public int numberOfOrbsToSpawn = 3;
    public GameObject orbPrefab;
    public float height;

    public List<GameObject> spawnedOrbs;

    public int maxSpawnAttempts = 100;
    private int currentSpawnAttempts = 0;

    public static OrbSpawner Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);
    }

    public virtual void SpawnOrbs()
    {
        for (int i = 0; i < numberOfOrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;

            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            while (currentSpawnAttempts < maxSpawnAttempts)
            {
                bool hasFound = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP, 1, new LabelFilter(MRUKAnchor.SceneLabels.FLOOR), out randomPosition, out Vector3 normal);
                if (hasFound)
                {
                    break; // Exit the loop if a valid position is found
                }
                currentSpawnAttempts++;
            }

            randomPosition.y = height; // Offset the orb above the surface

            GameObject orb = Instantiate(orbPrefab, randomPosition, Quaternion.identity);
            spawnedOrbs.Add(orb);
        }
    }
}
