using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute();
    public abstract bool _isComplete {  get; } // with no set
}
