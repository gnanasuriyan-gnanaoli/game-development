using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public static event System.Action OnGuardHasSpottedPlayer;
    public Transform pathHolder;
    public Light spotLight;
    Transform playerTransform;
    public LayerMask viewMask;
    Color originalSpotLightColor;

    float wait = 1;
    public float speed = 5;
    public float rotateSpeed = 90;
    public float timeToSpotPlayer = 1f;


    float viewAngle;
    float playerVisibleTimer;
    public float viewDistance;
    
    void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        originalSpotLightColor = spotLight.color;
        viewAngle = spotLight.spotAngle;
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i=0; i < pathHolder.childCount; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;

        }
        StartCoroutine(FollowPath(waypoints));
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.forward*10, Color.green);
        Debug.DrawLine(transform.position, (playerTransform.position)*100, Color.red);
        if (CanSeePlayer())
        {
            spotLight.color = Color.green;
            playerVisibleTimer += Time.deltaTime;
            if(playerVisibleTimer >= timeToSpotPlayer)
            {
                spotLight.color = Color.red;
                if(OnGuardHasSpottedPlayer != null)
                    OnGuardHasSpottedPlayer();
            }
        }
        else
        {
            playerVisibleTimer = 0;
            spotLight.color = originalSpotLightColor;
        }
    }

    bool CanSeePlayer()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < viewDistance)
        {
            
            Vector3 directionToPlayer = (playerTransform.position -transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < (viewAngle / 2f))
            {
                if(!Physics.Linecast(transform.position, playerTransform.position, viewMask))
                {
                    return true;
                }
                
            }
        }
        return false;

    }
    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWayPointIndex = 1;
        Vector3 targetPosition = waypoints[targetWayPointIndex];
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(transform.position == targetPosition)
            {
                targetWayPointIndex = (targetWayPointIndex + 1) % waypoints.Length;
                targetPosition = waypoints[targetWayPointIndex];
                yield return new WaitForSeconds(wait);
                yield return StartCoroutine(TurnToFace(waypoints[targetWayPointIndex]));
            }
            yield return null;
        }

    }


    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, 90 * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
