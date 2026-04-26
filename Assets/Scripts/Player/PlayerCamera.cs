using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Vector3 constraintPos;

    float dtime;

    Transform follower;
    Transform lookat;

    void Start()
    {
        
    }

    void Update()
    {
        dtime = Time.deltaTime;

        if(lookat)
        {
            Vector3 lookVec = (lookat.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookVec);
        }

        if(follower)
        {
            Pose pose = new Pose(transform.position, Quaternion.Euler(0,transform.eulerAngles.y,0));
            Vector3 pos = follower.position + 
            pose.right * constraintPos.x +
            pose.up * constraintPos.y +
            pose.forward * constraintPos.z;
            transform.position = Vector3.Lerp(transform.position, pos, dtime*8);
        }
    }

    public void SetFollower(Transform tra)
    {
        follower = tra;
    }

    public void LookAt(Transform tra)
    {
        lookat = tra;
    }
}
