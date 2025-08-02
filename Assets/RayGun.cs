using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public OVRInput.RawButton fireButton;
    public LineRenderer linePrefab;
    public GameObject rayImpactPrefab;
    public Transform shootingPoint;
    public float maxLineLength = 5f;
    public float lineShowTime = 0.3f;
    public LayerMask hitLayerMask;
    public AudioSource audioSource;
    public AudioClip fireSound;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(fireButton))
        {
            Fire();
        }

    }

    public void Fire()
    {

        audioSource.PlayOneShot(fireSound);

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, maxLineLength, hitLayerMask);


        Vector3 endPosition = Vector3.zero;
        if (hasHit)
        {
            Debug.Log("Hit: " + hitInfo.collider.name);
            endPosition = hitInfo.point;

            MyGhost ghost = hitInfo.collider.GetComponentInParent<MyGhost>();
            if (ghost != null)
            {
                ghost.Kill();
            }
            else
            {
                GameObject impact = Instantiate(rayImpactPrefab, hitInfo.point, Quaternion.LookRotation(-hitInfo.normal));
                Destroy(impact, 1f);
            }

        }
        else
        {
            Debug.Log("No hit detected, using max line length.");
            endPosition = shootingPoint.position + shootingPoint.forward * maxLineLength;
        }




        LineRenderer line = Instantiate(linePrefab);

        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);

        
        line.SetPosition(1, endPosition);

        Destroy(line.gameObject, lineShowTime);
    }
}
