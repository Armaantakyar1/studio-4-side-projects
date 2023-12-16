using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class JobSystem
{
    public static void SubmitJob<T>(Job<T> job)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback((object state) => { job.Execute(); }), null);
    }
}
