using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float t;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + Vector3.back * 10f, t);
    }
}
