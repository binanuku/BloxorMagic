using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] GameObject _rollPoint; 회전할 때 기준점 표시
    private Rigidbody _playerRb;
    private BoxCollider _playerCol;

    private float _rollSpeed = 3; //회전 속도
    private float _currentTime;
    private int _moveCount;

    private bool _isMooving; //회전하는 동안 추가 회전이 안됨    true = 회전 x, false = 회전 o
    private bool _isStart;   //바닥에 닿기 전 움직임 방지        true = 움직임 x, false = 움직임 o
    private bool _isDead;    //사망하면 조작 안됨               true = 사망 o, false = 사망 x


    [Header("Hit Object")]
    [SerializeField] LayerMask _contactWallLayer; //블럭(플레이어)의 현재 회전상태를 알려주는 콜라이더 레이어
    private RaycastHit _hit;

    private Vector3 _center; //회전 중심점
    private Vector3 _axis;   //회전 방향

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerCol = GetComponent<BoxCollider>();
        F_Initialize();
    }

    private void Update()
    {
        F_Function();
        //Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
    }

    private void F_Initialize()
    {
        _isMooving = false;
        _isDead = false;
        _isStart = false;
        transform.position = new Vector3(0, 2.5f, 0); //스테이지가 바뀜/현재 스테이지 재시작 = 플레이어 위치 초기화c
    }

    private void F_Function()
    {
        PlayerManager.Instance._contactWall.position = transform.position; //레이에 맞는 콜라이더는 플레이어와 같이 움직여야함
        PlayerManager.Instance._mainCamera.position = transform.position + new Vector3(0, 4, -5.5f);//플레이어를 따라 움직이는 카메라

        if (Input.GetKeyDown(KeyCode.R)) //R키 누르면 현재 씬 재시작
            UIManager.Instance.F_OnClickRetry();
        if (Input.GetKeyDown(KeyCode.Escape))//Esc키 누르면 SettingUI 켜고 끔
            UIManager.Instance.F_OnClickSettingUI();
        F_CheckTime(); //현재 시간 실시간 체크

        F_Movement(); //플레이어 움직임 조작
    }

    private void F_CheckTime() //현재 시간 실시간 체크
    {
        if (_isStart && !_isDead) //시작하고 죽거나 클리어하지 않으면
        {
            _currentTime += Time.deltaTime;
            UIManager.Instance.F_GetCurrentTime((int)_currentTime); //현재 시간 UI에 적용
        }
    }

    private void F_Movement()
    {
        if (_isMooving || _isDead) return; //죽거나 회전할 때 예외처리

        //W A S D & 방향키로 조작
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) F_GetRollPoint(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) F_GetRollPoint(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) F_GetRollPoint(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) F_GetRollPoint(Vector3.back);
    }


    private void F_GetRollPoint(Vector3 dir) //조작키를 누르면 플레이어 위치에 따라 회전 기준점 변경
    {
        if (Physics.Raycast(transform.position, transform.up, out _hit, 5f, _contactWallLayer)) //Player 상단 ray 쏘기
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
                    if (dir == Vector3.left || dir == Vector3.right)
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
        _playerRb.isKinematic = true; //회전중에 얼리기
        _isMooving = true; //회전중 추가회전 x
        _moveCount++;
        UIManager.Instance.F_GetMoveCount(_moveCount.ToString()); //움직인 횟수 화면에 표시

        for (int i = 0; i < (90 / _rollSpeed); i++)
        {
            transform.RotateAround(v_center, v_axis, _rollSpeed); //회전 기준점:center 회전 방향:axis
            yield return new WaitForSeconds(0.01f);
        }

        _playerRb.isKinematic = false; //회전 종료 후 풀기
        _isMooving = false; //회전 가능
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("DeadPanel")) //사망 체크 패널
        {
            _isDead = true; // 조작 안되게 함
            _playerCol.isTrigger = true; //떨어지게 함
            UIManager.Instance.F_OnDeadUI(); //사망 UI 띄우기
            UIManager.Instance.F_GetData(); //최종 시간, 움직인 횟수 넣기
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //스테이지 시작할 때 바닥에 닿기 전 조작 방지
        if (collision.transform.CompareTag("Panel") && !_isStart)
        {
            _isDead = false;
            _isStart = true;
        }
    }
}
