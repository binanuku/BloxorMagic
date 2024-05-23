using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] GameObject _rollPoint; 회전할 때 기준점 표시
    [SerializeField] Transform _contactWall; //플레이어가 쏘는 레이가 충돌할 벽
    [SerializeField] Transform _mainCamera; //카메라
    Rigidbody _playerRb;

    private float _rollSpeed = 3; //회전 속도
    private bool _isMooving; //회전하는동안 추가 회전이 안되게 함
    private bool _isOut; //사망하면 조작이 안되게 함

    Vector3 _center;
    Vector3 _axis;

    RaycastHit _hit;
    [SerializeField] LayerMask _contactWallLayer;

    private void Start()
    {
        _playerRb = transform.GetComponent<Rigidbody>();
        F_Initialize();
    }

    private void Update()
    {
        F_Move();
        Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
        _contactWall.position = transform.position; //레이에 맞는 콜라이더는 플레이어와 같이 움직여야함
        _mainCamera.position = transform.position + new Vector3(0, 4, -5.5f);//플레이어를 따라 움직이는 카메라
    }

    public void F_Initialize()
    {
        _isMooving = false;
        _isOut = false;
        transform.position = new Vector3(0, 2.5f, 0); //스테이지가 바뀌면서 플레이어 위치 초기화
        _contactWall.position = transform.position;
    }

    void F_Move() //W A S D 키로 조작하는 함수
    {
        if (_isMooving || _isOut) return;

        if      (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) F_GetRollPoint(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) F_GetRollPoint(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) F_GetRollPoint(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) F_GetRollPoint(Vector3.back);
    }


    void F_GetRollPoint(Vector3 dir) //조작키를 누르면 플레이어 위치에 따라 회전 기준점이 바뀜
    {
        if (Physics.Raycast(transform.position, transform.up, out _hit, 5f, _contactWallLayer)) //Player 상단에 ray 쏘기
        {
            switch (_hit.collider.name) //ray에 맞은 물체 이름
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
        _axis = Vector3.Cross(Vector3.up, dir); //두 벡터의 수직 벡터 반환
        StartCoroutine(F_Rolling(_center, _axis)); //중심점과 수직 벡터를 넣어 회전
    }

    IEnumerator F_Rolling(Vector3 v_center, Vector3 v_axis)
    {
        _playerRb.constraints = RigidbodyConstraints.FreezeAll; //회전중에 얼리기
        _isMooving = true;

        for (int i = 0; i < (90 / _rollSpeed); i++)
        {
            transform.RotateAround(v_center, v_axis, _rollSpeed); //회전 기준점:center 회전 방향:axis
            yield return new WaitForSeconds(0.01f);
        }

        _playerRb.constraints &= ~RigidbodyConstraints.FreezePositionY; //YPosition만 풀기
        _isMooving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("DeadPanel"))
        {
            _isOut = true; // 조작 안되게
        }
    }
}
