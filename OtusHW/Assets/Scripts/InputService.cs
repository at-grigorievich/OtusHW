using UnityEngine;

public static class InputService
{
    public static Vector2 GetAxis()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        return new Vector2(x, y);
    }
}