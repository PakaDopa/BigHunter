using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 desiredPosition = new(player.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
