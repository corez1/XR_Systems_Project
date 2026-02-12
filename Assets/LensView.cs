using UnityEngine;

public class LensFollow : MonoBehaviour
{
    public Transform playerHead;
    public MeshRenderer lensRenderer;

    void LateUpdate()
    {
        transform.rotation = playerHead.rotation;

        float lensTilt = transform.parent.eulerAngles.z;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, lensTilt);
    }
}