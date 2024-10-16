using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class RobotDialog : MonoBehaviour, INPC
{
    public GameObject Dialog;
    public TMP_Text DialogContent;

    private bool _dialogOn = false;

    private PlayerInput _input;

    public string[] _dialogQueue;
    private int _dialogindex;
    private int _dialoglength;

    void Awake()
    {
        _input = PlayerInput.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        _dialogindex = 0;
        _dialoglength = _dialogQueue.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (_dialogOn)
        {
            if (_input._dialogPressed)
            {
                NextDialog();
            }
        }
    }

    public void OnPlayerEnter()
    {
        changeState(true);
        DialogContent.text = _dialogQueue[0];
        _dialogindex++;
    }
    public void OnPlayerExit()
    {
        changeState(false);
    }

    private void changeState(bool aSwitch) {
        Dialog.SetActive(aSwitch);
        _dialogOn = aSwitch;
    }

    private void NextDialog()
    {
        if (_dialogOn)
        {
            if (_input._dialogPressed)
            {
                if (_dialogindex < _dialoglength)
                {
                    DialogContent.text = _dialogQueue[_dialogindex];
                    _dialogindex++;
                }
                else
                {
                    OnPlayerExit();
                }

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit();
        }
    }
}
