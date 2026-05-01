using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] AnimationCurve move_curve;

    float dtime;

    Vector3 movepos;
    Quaternion rot;

    float move_time;
    float move_time_now;
    Vector3 move_startpos;
    Vector3 move_targetpos;

    Transform lookAt_Tra;

    void Start()
    {
        movepos = transform.position;
    }

    void Update()
    {
        dtime = Time.deltaTime;

        Update_Move();
        Update_LookAt();

        Update_Confirm();
    }

    void Update_Move()
    {
        if(move_time_now <= 0) { return; }
        
        move_time_now -= dtime;
        float t = move_curve.Evaluate(1 - move_time_now/move_time);

        movepos = Vector3.Lerp(move_startpos, move_targetpos, t);
    }

    void Update_LookAt()
    {
        Vector3 vec = lookAt_Tra.position - transform.position;
        vec.y = 0;
        vec = vec.normalized;
        Quaternion target = Quaternion.LookRotation(vec);
        rot = Quaternion.Lerp(rot, target, dtime * 8);
    }

    void Update_Confirm()
    {
        transform.position = movepos;
        transform.rotation = rot;
    }

    public void Move(Vector3 pos, float time)
    {
        move_time = time;
        move_time_now = time;
        move_startpos = transform.position;
        move_targetpos = pos;
    }

    public void LookAt(Transform tra)
    {
        lookAt_Tra = tra;
    }

    public float GetTargetSQRDist(Vector3 pos)
    {
        return (transform.position - pos).sqrMagnitude;
    }
}
