using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPerception : MonoBehaviour
{
    [SerializeField] protected string tagName = "";
    [SerializeField] private float distance = 1;
    [SerializeField] private float maxAngle = 45;

    public string TagName { get { return tagName; } }
    public float Distance { get { return distance; } }
    public float MaxAngle { get { return maxAngle; } }

    public abstract GameObject[] GetGameObjects();

}
