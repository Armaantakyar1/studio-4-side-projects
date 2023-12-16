using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public abstract class Job<T>
{
    public delegate void JobFinished(T result);
    public JobFinished jobFinishedEvent;
    public abstract void Execute();
}
