using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform target;
    private Camera m_camera;
    public Transform startPosition;
    public Transform stopPosition;

    private Vector3 cameraBottomLeft;
    private Vector3 cameraTopLeft;
    private Vector3 cameraBottomRight;
    private Vector3 cameraTopRight;
    float viewPortCenterWidth;
    bool cameraLeftStop;
    bool cameraRightStop;
    bool cameraFollow;

    Vector3 pos;

    void Awake()
    {
        m_camera = GetComponent<Camera>();
        pos = m_camera.transform.position;
        cameraLeftStop = false;
        cameraRightStop = false;
        cameraFollow = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        //bool cameraFollow = true;

        cameraBottomLeft = m_camera.ViewportToWorldPoint(new Vector3(0, 0, pos.z));
        cameraTopRight = m_camera.ViewportToWorldPoint(new Vector3(1, 1, pos.z));
        cameraTopLeft = new Vector3(cameraBottomLeft.x, cameraTopRight.y, pos.z);
        cameraBottomRight = new Vector3(cameraTopRight.x, cameraBottomLeft.y, pos.z);

        float LeftcolliderSize = GameObject.Find("StartPosition").GetComponent<BoxCollider2D>().size.x;
        float LeftOffset = LeftcolliderSize / 2;
        float cameraCheckPointLeft = startPosition.position.x + LeftOffset;

        float RightcolliderSize = GameObject.Find("StopPosition").GetComponent<BoxCollider2D>().size.x;
        float RightOffset = RightcolliderSize / 2;
        float cameraCheckPointRight = stopPosition.position.x + RightOffset;

        if(cameraCheckPointLeft > cameraTopLeft.x)
        {
            viewPortCenterWidth = (Mathf.Abs(cameraTopRight.x) + Mathf.Abs(cameraTopLeft.x)) / 2;
            float changePos = cameraCheckPointLeft + viewPortCenterWidth;
            m_camera.transform.position = new Vector3(changePos, pos.y, pos.z);
            cameraFollow = false;
            cameraLeftStop = true;
        }
        else if(cameraCheckPointRight < cameraTopRight.x)
        {
            viewPortCenterWidth = (Mathf.Abs(cameraTopRight.x) - Mathf.Abs(cameraTopLeft.x)) / 2;
            float changePos = cameraCheckPointLeft - viewPortCenterWidth;
            m_camera.transform.position = new Vector3(changePos, pos.y, pos.z);
            cameraFollow = false;
            cameraLeftStop = true;
        }

        if(cameraLeftStop)
        {
            if(target.position.x > cameraTopLeft.x + viewPortCenterWidth)
            {
                cameraFollow = true;
                cameraLeftStop = false;
            }
        }

        if (cameraRightStop)
        {
            if (target.position.x > cameraTopRight.x - viewPortCenterWidth)
            {
                cameraFollow = true;
                cameraRightStop = false;
            }
        }

        if (cameraFollow)
        {
            //var pos = m_camera.transform.position;
            m_camera.transform.position = new Vector3(target.position.x, pos.y, pos.z);
        }
    }
}
