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

    private float _rollSpeed = 5;
    private bool _isMooving;
    private bool _isFall;

    Vector3 _center;
    Vector3 _axis;

    RaycastHit _hit;
    [SerializeField] LayerMask _contactWallLayer;
    //https://mstfmrt07.medium.com/making-a-bloxorz-game-in-unity-part-i-roll-the-cube-67113d48415a
    //플레이어 상단에 레이 쏴서 레이에 닿는 콜라이더 레이어에 따른 방향값을 토대로 기준점 구하기

    private void Start()
    {
        F_Initialize();
    }

    private void Update()
    {
        F_Move();
        _contactWall.position = transform.position;
        _mainCamera.position = transform.position + new Vector3(0, 5, -6.5f);
    }

    void F_Initialize()
    {
        _isMooving = false;
        _isFall = true;
        _player.position = new Vector3(0, 2.5f, 0);
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void F_Move()
    {
        if (_isMooving) return;

        if (Input.GetKeyDown(KeyCode.A)) F_GetRollPoint(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D)) F_GetRollPoint(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W)) F_GetRollPoint(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S)) F_GetRollPoint(Vector3.back);
    }

    void F_GetRollPoint(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, transform.up, out _hit, 5f, _contactWallLayer)) //Player 상단에 ray 쏘기
        {
            switch (_hit.collider.name)
            {
                case "Y":
                    _center = transform.position + Vector3.down + dir * 0.5f;
                    break;
                case "Z":
                    if (dir == Vector3.left || dir == Vector3.right)
                        _center = transform.position + Vector3.down * 0.5f + dir * 0.5f;
                    else
                        _center = transform.position + dir + (Vector3.down * 0.5f);
                    break;
                case "X":
                    if(dir == Vector3.left || dir == Vector3.right)
                        _center = transform.position + dir + (Vector3.down * 0.5f);
                    else
                        _center = transform.position + Vector3.down * 0.5f + dir * 0.5f;
                    break;
            }
        }

        //_rollPoint.transform.position = _center;
        _axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(F_Rolling(_center, _axis));
    }

    IEnumerator F_Rolling(Vector3 v_center, Vector3 v_axis)
    {
        _isMooving = true;

        for (int i = 0; i < (90 / _rollSpeed); i++)
        {
            transform.RotateAround(v_center, v_axis, _rollSpeed); //회전 기준점:center 회전 방향:axis
            yield return new WaitForSeconds(0.01f);
        }

        _isMooving = false;
    }
}
