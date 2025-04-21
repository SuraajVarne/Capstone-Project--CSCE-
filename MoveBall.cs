using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPos + new Vector3(movement, 0, 0);
    }
}
