using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobCreation : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(" JOB MADE");
        SubmitJob();
    }
    public void SubmitJob()
    {
        JobType time = new JobType();
        time.jobFinishedEvent += SubsribeToJob;

        JobSystem.SubmitJob(time);
    }

    void SubsribeToJob(float result)
    {
        Debug.Log("Subscribed called with result: " + result);
    }
}
