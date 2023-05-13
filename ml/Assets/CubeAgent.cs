using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent
{
    public GameObject ball;
    private Transform ballTransform;
    private Rigidbody ballRigidbody;
    private Vector3 ballOriginalPosition;
    public GameObject cube;
    private Transform cubeTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Get all ball components
        ballTransform = ball.GetComponent<Transform>();
        ballRigidbody = ball.GetComponent<Rigidbody>();
        ballOriginalPosition = ballTransform.localPosition;
        // Get all cube components
        cubeTransform = cube.GetComponent<Transform>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset the ball
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballTransform.SetLocalPositionAndRotation(ballOriginalPosition, Quaternion.identity);

        // Reset the cube
        cubeTransform.rotation = Quaternion.identity;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);

        // Add all cube observations. We only need its rotation
        sensor.AddObservation(cubeTransform.rotation);

        // Add all ball observations
        sensor.AddObservation(ballTransform.localPosition);
        sensor.AddObservation(ballRigidbody.velocity);
        sensor.AddObservation(ballRigidbody.angularVelocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
        var x = actions.ContinuousActions[0];
        var y = actions.ContinuousActions[1];
        var z = actions.ContinuousActions[2];

        cubeTransform.rotation = new Quaternion(x, y, z, 0);


        if (ballTransform.localPosition.magnitude >= 5)
        {
            AddReward(-1);
            EndEpisode();
        } else if (ballTransform.localPosition.magnitude == 0)
        {
            AddReward(1);
        }
        else
        {
            AddReward(1 / ballTransform.localPosition.magnitude);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //base.Heuristic(in actionsOut) 
        
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}