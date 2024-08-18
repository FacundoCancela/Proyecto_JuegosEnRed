using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour, IControl
{
    [SerializeField] private IActor _playerModel;
    [SerializeField] private IView _playerView;

    private void Awake()
    {
        _playerModel = GetComponent<IActor>();
        _playerView = GetComponent<IView>();
    }

    private void Update()
    {
        GetInputs();
    }

    public void GetInputs()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _playerModel.Jump();
        }

        float x = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(x, 0).normalized;
        _playerModel.Move(dir);
    }
}
