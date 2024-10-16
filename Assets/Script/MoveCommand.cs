using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent _agent;
    private Vector3 _destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        _agent = agent;
        _destination = destination;
    }

    public override void Execute() { 
        _agent.SetDestination(_destination);
    }

    public override bool _isComplete => ReachedDestination();//lamba expression

    private bool ReachedDestination() {
        if (_agent.remainingDistance > 0.1f)
        {
            return false;
        }
        else { 
            return true;
        }
    }
}
