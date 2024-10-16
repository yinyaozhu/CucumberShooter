using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    Queue<Command> _commands = new Queue<Command>();

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _pointerPrefeb;
    [SerializeField] private Camera _cam;

    private Command _currentCommand;

    public override void Interact()
    {
        if (_input._commandPressed)
        {
            Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hitInfo)) {

                if (hitInfo.transform.CompareTag("Ground")) {
                    GameObject pointer = Instantiate(_pointerPrefeb);
                    pointer.transform.position = hitInfo.point;
                    _commands.Enqueue(new MoveCommand(_agent, hitInfo.point));
                } else if (hitInfo.transform.CompareTag("Builder")){
                    _commands.Enqueue(new BuildCommand(_agent, hitInfo.transform.GetComponent<Builder>()));
                }

            }
        }
        ProcessCommands();
    }

    private void ProcessCommands()
    {
        if (_currentCommand != null && !_currentCommand._isComplete)
        {
            return;
        }

        if (_commands.Count == 0 )
        {
            return;
        }

        //remove from queue then excute it
        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();

    }
}
