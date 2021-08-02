using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCamera;

    float camera_dist = 0f;
    public float camera_width = -10f;
    public float camera_height = 4f;
    public float camera_fix = 3f;
    Vector3 dir;


    void Start()
    {
        Player = GameObject.Find("Player");
        MainCamera = GameObject.Find("MainCamera");

        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);
        dir = new Vector3(0, camera_height, camera_width).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ray_target = transform.up * camera_height + transform.forward * camera_width;

        RaycastHit hitinfo;
        Physics.Raycast(transform.position, ray_target, out hitinfo, camera_dist);
        if(hitinfo.point!=Vector3.zero)
        {
            MainCamera.transform.position = hitinfo.point;
            MainCamera.transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            MainCamera.transform.localPosition = Vector3.zero;
            MainCamera.transform.Translate(dir * camera_dist);
            MainCamera.transform.Translate(dir * -1 * camera_fix);
        }
    }
}
