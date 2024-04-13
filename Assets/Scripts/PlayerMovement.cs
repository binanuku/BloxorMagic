using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject _rollPoint;
    [SerializeField] Transform _contactWall;
    [SerializeField] Transform _mainCamera;
    [SerializeField] Transform _player;
    Rigidbody _playerRb;

    private float _rollSpeed = 3;
    private bool _isMooving;
    private bool _isOut;

    Vector3 _center;
    Vector3 _axis;

    RaycastHit _hit;
    [SerializeField] LayerMask _contactWallLayer;

    private void Start()
    {
        _playerRb = _player.GetComponent<Rigidbody>();
        F_Initialize();
    }

    private void Update()
    {
        F_Move();
        _contactWall.position = _player.position;
        _mainCamera.position = _player.position + new Vector3(0, 5, -6.5f);
    }

    public void F_Initialize()
    {
        _isMooving = false;
        _isOut = false;
        _player.position = new Vector3(0, 2.5f, 0);
        _contactWall.position = _player.position;
    }

    void F_Move()
    {
        if (_isMooving || _isOut) return;

        if (Input.GetKey(KeyCode.A)) F_GetRollPoint(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) F_GetRollPoint(Vector3.right);
        else if (Input.GetKey(KeyCode.W)) F_GetRollPoint(Vector3.forward);
        else if (Input.GetKey(KeyCode.S)) F_GetRollPoint(Vector3.back);
    }

    void F_GetRollPoint(Vector3 dir)
    {
        if (Physics.Raycast(_player.position, _player.up, out _hit, 5f, _contactWallLayer)) //Player 상단에 ray 쏘기
        {
            switch (_hit.collider.name)
            {
                case "Y":
                    _center = _player.position + Vector3.down + dir * 0.5f;
                    break;
                case "Z":
                    if (dir == Vector3.left || dir == Vector3.right)
                        _center = _player.position + Vector3.down * 0.5f + dir * 0.5f;
                    else
                        _center = _player.position + dir + (Vector3.down * 0.5f);
                    break;
                case "X":
                    if(dir == Vector3.left || dir == Vector3.right)
                        _center = _player.position + dir + (Vector3.down * 0.5f);
                    else
                        _center = _player.position + Vector3.down * 0.5f + dir * 0.5f;
                    break;
            }
        }

        //_rollPoint.transform.position = _center;
        _axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(F_Rolling(_center, _axis));
    }

    IEnumerator F_Rolling(Vector3 v_center, Vector3 v_axis)
    {
        _playerRb.constraints = RigidbodyConstraints.FreezeAll; //회전중에 얼리기
        _isMooving = true;

        for (int i = 0; i < (90 / _rollSpeed); i++)
        {
            _player.RotateAround(v_center, v_axis, _rollSpeed); //회전 기준점:center 회전 방향:axis
            yield return new WaitForSeconds(0.01f);
        }

        _playerRb.constraints &= ~RigidbodyConstraints.FreezePositionY; //YPosition만 풀기
        _isMooving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("DeadPanel"))
        {
            _isOut = true;
            _player.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
