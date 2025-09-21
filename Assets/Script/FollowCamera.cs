using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;          // Assign the Player transform in the Inspector
    public Vector3 offset = new Vector3(0, 20, 0);  // Offset from the player
    public float smoothSpeed = 5f;    // Camera follow speed
    public bool lookAtPlayer = true;  // Enable camera rotation

    private void Start()
    {
        StartCoroutine(WaitTillTargetAppears());
    }

    IEnumerator WaitTillTargetAppears()
    {
        while (target == null)
        {
            GameObject playerObj = GameObject.Find("Player(Clone)");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            yield return null; // wait 1 frame before checking again
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Smooth follow
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
