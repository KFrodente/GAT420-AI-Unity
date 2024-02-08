using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodeHolder : MonoBehaviour
{
    public static NodeHolder Instance;

    public List<AINavNode> nodes;

    private void Awake()
    {
        Instance = this;
    }
}
