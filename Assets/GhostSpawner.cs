using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class GhostSpawner : MonoBehaviour
{
    public float spawnTimer = 1f;
    public GameObject ghostPrefab;

    public float minEdgeDistance = 0.3f;
    public MRUKAnchor.SceneLabels spwanLabels;
    public float normalOffset = 0.1f; // Offset slightly above the surface

    public int spawnTry = 100;

    private float timer = 0f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    private void SpawnGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        int currentTry = 0;

        while (currentTry < spawnTry)
        {
            bool foundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, new LabelFilter(spwanLabels), out Vector3 position, out Vector3 normal);

            if ((foundPosition))
            {
                Vector3 randomPositionNormalOffset = position + normal * normalOffset; // Offset slightly above the surface
                randomPositionNormalOffset.y = 0;

                GameObject ghost = Instantiate(ghostPrefab, randomPositionNormalOffset, Quaternion.identity);
                ghost.transform.LookAt(transform.position);
                return;
            }
            else
            {
                Debug.LogWarning("Failed to find a valid position for ghost spawn after " + spawnTry + " attempts.");
                currentTry++;
            }
        }




    }
}
