using Unity;
using UnityEngine;

public static class Extensions {
    public static void MoveTo(this Transform transform, Vector2 pos, float zIndex = 0f) {
        transform.SetPositionAndRotation(new Vector3(pos.x, pos.y, zIndex), new Quaternion (0,0,0,0));
    }

    public static void MoveTo(this Transform transform, Vector3 pos, float zIndex = 0f) {
        transform.MoveTo(new Vector2(pos.x, pos.y), zIndex);
    }

    public static void MoveTo(this Transform transform, float x, float y, float z = 0f) {
        transform.MoveTo(new Vector3(x, y, z));
    }
}