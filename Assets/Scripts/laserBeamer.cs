using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeamer : MonoBehaviour
{
    private simArea areaComponent;
    private GameObject area;
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
}
