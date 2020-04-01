using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeamer : MonoBehaviour
{
    private simArea areaComponent;
    private GameObject area;
    Rigidbody m_JointRb;
    bool m_State;

    public bool GetState()
    {
        return m_State;
    }

    private void Start()
    {
        area = gameObject.transform.parent.gameObject;
        areaComponent = area.GetComponent<simArea>();
    }

    public void ResetSwitch(int spawnAreaIndex)
    {
        areaComponent.PlaceObject(gameObject, spawnAreaIndex);
        m_State = false;
    }

    public void MoveAgent(float[] act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = Mathf.FloorToInt(act[0]);
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
            case 5:
                dirToGo = transform.up * -0.1f;
                break;
            case 6:
                dirToGo = transform.up * 0.1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 200f);
        m_JointRb.AddForce(dirToGo * 2f, ForceMode.VelocityChange);
    }

}
