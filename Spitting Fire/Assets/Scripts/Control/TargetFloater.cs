using UnityEngine;
/// <summary>
/// Just float the GameObject up and down
/// </summary>
public class TargetFloater : MonoBehaviour
{
    private float height;

    private void Start()
    {
        height = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, height + Mathf.Sin(Time.time), transform.position.z);
    }
}