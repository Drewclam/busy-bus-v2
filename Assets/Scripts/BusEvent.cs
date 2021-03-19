using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BusEvent : MonoBehaviour {
    public delegate void Complete(EventType eventType, float timeElapsed, float totalTime);
    public static Complete OnComplete;
    public enum EventType {
        Drive,
        Fare,
        Rowdy
    }

    Coroutine eventRoutine;
    Coroutine timeoutRoutine;
    protected bool hasResponded;
    protected float timeToWait {
        get; set;
    }
    protected EventType type {
        get; set;
    }
    float timeElapsed;

    private void OnEnable() {
        OnComplete += RatePerformance;
    }

    void StartEvent() {
        timeElapsed = 0f;
        eventRoutine = StartCoroutine(EventRoutine());
        timeoutRoutine = StartCoroutine(Timeout());
    }

    IEnumerator EventRoutine() {
        yield return new WaitUntil(() => hasResponded);
        StopCoroutine(timeoutRoutine);
        Evaluate();
    }

    IEnumerator Timeout() {
        while (timeElapsed <= timeToWait) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        StopCoroutine(eventRoutine);
        Fail();
    }

    void Evaluate() {
        if (IsResponseCorrect()) {
            Pass();
        } else {
            Fail();
        }
    }

    void Pass() {
        gradeEvent.GradeDrive(timeElapsed, timeToWait);
    }

    void Fail() {
        gradeEvent.Fail();
    }

    protected virtual bool IsResponseCorrect() {
        Debug.LogWarning("Not implemented");
        return true;
    }

    void RatePerformance(EventType eventType, float timeElapsed, float totalTime) {
    }
}
