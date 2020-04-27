using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSystemBlock : MonoBehaviour
{
    public float Value; // Set the initial value in the inspector
    public List<CircuitSystemBlock> CompBlock; // Set the inputs in the inspector :: "Comp" stands for component.

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (IsSwitchedOn())
            OnAction();
        else
            OffAction();
    }

    public virtual void Init() { }

    public virtual bool IsSwitchedOn() { return false; }

    public virtual void OnAction() { }
    public virtual void OffAction() { }
}
