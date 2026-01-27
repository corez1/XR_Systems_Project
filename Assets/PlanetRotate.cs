using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float speed = 250.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
