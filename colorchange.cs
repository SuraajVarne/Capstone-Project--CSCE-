using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time, 1);
        rend.material.color = Color.Lerp(Color.red, Color.blue, t);
    }
}
