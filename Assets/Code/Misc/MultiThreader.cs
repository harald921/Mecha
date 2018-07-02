using System;
using System.Threading;
using System.Collections.Generic;

public static class MultiThreader
{
    static List<Action> _mainThreadCallbacks = new List<Action>();
    static List<IJob> _currentJobs = new List<IJob>();

    public static void ManualUpdate()
    {
        InvokeFinishedJobs();

        while (_mainThreadCallbacks.Count > 0)
        {
            _mainThreadCallbacks[0].Invoke();
            _mainThreadCallbacks.RemoveAt(0);
        }
    }

    static void InvokeFinishedJobs()
    {
        for (int i = _currentJobs.Count - 1; i >= 0; i--)
        {
            IJob currentJob = _currentJobs[i];
            if (currentJob.isFinished)
            {
                currentJob.MainThreadCallback();
                _currentJobs.RemoveAt(i);
            }
        }
    }

    public static void DoThreaded<T>(Func<T> inMethodToThread, Action<T> inMainThreadCallback)
    {
        Job<T> newJob = new Job<T>();

        lock (_currentJobs)
            _currentJobs.Add(newJob);

        newJob.methodToThread = inMethodToThread;
        newJob.mainThreadCallback = inMainThreadCallback;

        newJob.jobThread = new Thread(newJob.Execute);
        newJob.jobThread.Start();
    }

    public static void InvokeOnMain(Action inAction) =>
        _mainThreadCallbacks.Add(inAction);

    interface IJob
    {
        bool isFinished { get; }
        void Execute();
        void MainThreadCallback();
    }

    class Job<T> : IJob
    {
        public bool isFinished => !jobThread.IsAlive;

        public Func<T> methodToThread;       // The method that needs to be threaded. It returns an object.
        public Action<T> mainThreadCallback; // The callback that should run when the work is complete. It takes an object as a parameter.

        public Thread jobThread;             // The thread which the threaded method is run on

        public T result;                     // The result of the thread. This will be set whenever the job is done.


        public void Execute() =>
            result = methodToThread();

        public void MainThreadCallback() =>
            mainThreadCallback(result);
    }
}