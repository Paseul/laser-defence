using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using MLAgents;
using MLAgents.Sensors;

public class droneAgent : Agent
{
    public GameObject area;
    simArea m_MyArea;
    Rigidbody m_AgentRb;
    laserBeamer m_BeamerLogic;
    public GameObject areaSwitch;
    public bool useVectorObs;

    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        m_MyArea = area.GetComponent<simArea>();
        m_BeamerLogic = areaSwitch.GetComponent<laserBeamer>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (useVectorObs)
        {
            sensor.AddObservation(m_BeamerLogic.GetState());
            sensor.AddObservation(transform.InverseTransformDirection(m_AgentRb.velocity));
        }
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
        m_AgentRb.AddForce(dirToGo * 2f, ForceMode.VelocityChange);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-1f / maxStep);
        MoveAgent(vectorAction);
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new float[] { 3 };
        }
        if (Input.GetKey(KeyCode.W))
        {
            return new float[] { 1 };
        }
        if (Input.GetKey(KeyCode.A))
        {
            return new float[] { 4 };
        }
        if (Input.GetKey(KeyCode.S))
        {
            return new float[] { 2 };
        }
        if (Input.GetKey(KeyCode.Space))
        {
            return new float[] { 6 };
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return new float[] { 5 };
        }
        return new float[] { 0 };
    }

    public override void OnEpisodeBegin()
    {
        var enumerable = Enumerable.Range(0, 9).OrderBy(x => Guid.NewGuid()).Take(9);
        var items = enumerable.ToArray();

        m_MyArea.CleanSimArea();
        m_AgentRb.isKinematic = true;
        m_AgentRb.velocity = Vector3.zero;
        m_AgentRb.isKinematic = false;
        m_MyArea.PlaceObject(gameObject, items[1]);
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));

        m_BeamerLogic.ResetSwitch(items[0]);

        for (var i = 1; i < 9; i++)
        {
            m_MyArea.CreateBuilding(2, items[i]);
            m_MyArea.CreateCar(1, items[i]);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("turret"))
        {
            SetReward(2f);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("building") || collision.gameObject.CompareTag("car"))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}
