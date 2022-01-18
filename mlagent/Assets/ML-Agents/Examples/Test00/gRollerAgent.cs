using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
//mlAgent ���� ����

public class gRollerAgent : Agent
{
    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        //���ο� ���Ǽҵ� ���۽�, �ٽ� ������Ʈ�� �������� �ʱ�ȭ ����
        if(this.transform.localPosition.y < 0) // ������Ʈ�� floor �Ʒ��� ������ ��� �߰� �ʱ�ȭ
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(10, 0.5f, 0);
        }

        //Ÿ���� ��ġ�� ���Ǽҵ� ���۽� �����ϰ� ����
        /*float rx = 0;
        float rz = 0;

        while (true)
        {
            rx = Random.value * 18 - 9;

            if (rx > 12 || rx < -12)
                break;
        }
        while (true)
        {
            rz = Random.value * 18 - 9;
            if (rz > 6 || rz < -6)
                break;
        }
        Target.localPosition = new Vector3(rx, 0.5f, rz);
        */

        Target.localPosition = new Vector3(Random.value * 18 - 9, 0.5f, Random.value * 18 - 9);
    }

    //<summary>
    //��ȭ�н� ���α׷����� ���������� ����
    //</summary>
    //<param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {

        //Ÿ�ٰ� ������Ʈ�� �������� �����Ѵ�.
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        //���� ������Ʈ�� �̵����� �����Ѵ�.
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }


    //<summary>
    //��ȭ�н��� ����, ��ȭ�н��� ���� �ൿ�� �����Ǵ� �� 
    //</summary>
    public float forceMultiplier = 10;

    //public GameObject viewModel = null; 

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        //�н��� ���� �н��� ������ �ؼ��Ͽ� �̵��� ��Ų��.

        //Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);
        
        //���� �̵��Ҷ� Ÿ���� �ٶ�
        //viewModel.transform.LookAt(Target);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        //Ÿ���� ã���� ������������ �ְ�, ���Ǽҵ带 �����Ų��.
        //Reached target
        if(distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        //�� �Ʒ��� �������� �н��� ����ȴ�.
        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
           // SetReward(-0.1f);
            EndEpisode();
        }
    }

    //<summary>
    //�ش� �Լ��� �������� Ȥ�� ��Ģ���ִ� �ڵ��� ���۽�Ű�� ���� �Լ��̴�.
    //</summary>
    //<param name="actionsOut"></param>

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

}
