using UnityEngine;

public class SnapMug : MonoBehaviour
{
    public Transform refPoint;
    public float snapDistance = 10f;

    [SerializeField] Rigidbody rb;

    void Update()
    {
        Snap();
    }

    void Snap()
    {
        float distanceToRef = Vector3.Distance(transform.position, refPoint.position);

        if (distanceToRef < snapDistance)
        {
            transform.position = refPoint.position;
            rb.isKinematic = true;
        } else
        {
            rb.isKinematic = false;
        }
    }
}
