using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
# endif

[ExecuteAlways]
public class PlayerAnimator_AC : MonoBehaviour
{
    [SerializeField] Transform targetTra;

    public float distance;
    public float height;
    public Vector2 angle;

    void OnRenderObject()
    {
        Vector3 rot = targetTra.eulerAngles;
        Quaternion rotation = Quaternion.Euler(rot.x + angle.x*360f, rot.y + angle.y*360f, 0f);
        Vector3 direction = rotation * Vector3.forward;

        transform.position = targetTra.position + new Vector3(0,height,0) + (direction * distance);
        transform.LookAt(targetTra);
    }

}
