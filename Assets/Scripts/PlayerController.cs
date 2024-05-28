using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Object")]
    //[SerializeField] GameObject _rollPoint; ȸ���� �� ������ ǥ��
    private Rigidbody _playerRb;
    private BoxCollider _playerCol;

    [SerializeField] LayerMask _contactWallLayer;
    private RaycastHit _hit;

    private Vector3 _center;
    private Vector3 _axis;

    private float _rollSpeed = 3; //ȸ�� �ӵ�
    private bool _isMooving; //ȸ���ϴ� ���� �߰� ȸ���� �ȵ�    true = ȸ�� x, false = ȸ�� o
    [SerializeField] bool _isStart;   //�ٴڿ� ��� �� ������ ����        true = ������ x, false = ������ o
    [SerializeField] bool _isDead;    //����ϸ� ���� �ȵ�               true = ��� o, false = ��� x


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

    public void F_Initialize()
    {
        _isMooving = false;
        _isDead = true;
        _isStart = false; 
        transform.position = new Vector3(0, 2.5f, 0); //���������� �ٲ�/���� �������� ����� = �÷��̾� ��ġ �ʱ�ȭc
    }

    void F_Function() //W A S D/����Ű�� ����
    {
        PlayerManager.Instance._contactWall.position = transform.position; //���̿� �´� �ݶ��̴��� �÷��̾�� ���� ����������
        PlayerManager.Instance._mainCamera.position = transform.position + new Vector3(0, 4, -5.5f);//�÷��̾ ���� �����̴� ī�޶�

        if (Input.GetKeyDown(KeyCode.R)) //���� �������� �����
            UIManager.Instance.F_OnClickRetry();

        if (_isMooving || _isDead) return;

        if      (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) F_GetRollPoint(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) F_GetRollPoint(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) F_GetRollPoint(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) F_GetRollPoint(Vector3.back);
    }


    void F_GetRollPoint(Vector3 dir) //����Ű�� ������ �÷��̾� ��ġ�� ���� ȸ�� ������ ����
    {
        if (Physics.Raycast(transform.position, transform.up, out _hit, 5f, _contactWallLayer)) //Player ��� ray ���
        {
            switch (_hit.collider.name) //ray�� ���� ��ü �̸�
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
        _axis = Vector3.Cross(Vector3.up, dir); //�� ������ ���� ���� ��ȯ
        StartCoroutine(F_Rolling(_center, _axis)); //�߽����� ���� ���͸� �־� ȸ��
    }

    IEnumerator F_Rolling(Vector3 v_center, Vector3 v_axis)
    {
        _playerRb.isKinematic = true; //ȸ���߿� �󸮱�
        _isMooving = true; //ȸ���� �߰�ȸ�� x

        for (int i = 0; i < (90 / _rollSpeed); i++)
        {
            transform.RotateAround(v_center, v_axis, _rollSpeed); //ȸ�� ������:center ȸ�� ����:axis
            yield return new WaitForSeconds(0.01f);
        }

        _playerRb.isKinematic = false; //ȸ�� ���� �� Ǯ��
        _isMooving = false; //ȸ�� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("DeadPanel")) //��� üũ �г�
        {
            _isDead = true; // ���� �ȵǰ�
            _playerCol.isTrigger = true;
            //������ ���̵� �� �� ����� ���̵� �ƿ�
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�������� ������ �� �ٴڿ� ��� �� ���� ����
        if (collision.transform.CompareTag("Panel") && !_isStart)
        {
            _isDead = false;
            _isStart = true;
        }
    }
}
