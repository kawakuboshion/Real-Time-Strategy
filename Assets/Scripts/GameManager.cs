using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Unit;
    [SerializeField] private List<GameObject> _selectUnit;
    private bool _isUnitSelected = false;
    private bool _isRangeSelection = false;
    private Vector3 _startSelectPosition;
    private Vector3 _endSelectPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isRangeSelection)
        {
            //�N���b�N���������ɃJ��������Ray���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.GetComponent<Unit>() != null)
                {
                    Debug.Log("���j�b�g��I��");
                    _selectUnit.Add(hit.collider.gameObject);
                    _isUnitSelected = true;
                    return;
                }
                //Ray�ɓ�����ʒu�ɑ��݂���I�u�W�F�N�g���擾���A�n�ʂ̃I�u�W�F�N�g��������s��
                else if (hit.collider.gameObject.CompareTag("Ground") && _isUnitSelected)
                {
                    Debug.Log("�ړ��J�n");
                    for(int i = 0; i < _selectUnit.Count; i++)
                    {
                        _selectUnit[i].GetComponent<NavMeshAgent>().destination = new Vector3(hit.point.x, 0.5f, hit.point.z);
                    }
                    _selectUnit.Clear();
                    _isUnitSelected =false;
                    return;
                }
                _startSelectPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1000f));
                _isRangeSelection = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0) && _isRangeSelection)
        {
            _endSelectPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            foreach(var box in _Unit)
            {
                if (IsBetween(box.transform.position,_startSelectPosition,_endSelectPosition))
                {
                    Debug.Log("���j�b�g��I��");
                    _selectUnit.Add(box);
                    _isUnitSelected = true;
                }
            }
            Debug.Log("�͈͑I������");
            _isRangeSelection=false;
        }
    }

    /// <summary>
    /// target��a��b�̊Ԃ̐��l��(float�p)
    /// </summary>
    private bool IsBetween(float target, float a, float b)
    {
        if (a > b)
        {
            return target <= a && target >= b;
        }
        return target <= b && target >= a;
    }

    /// <summary>
    /// target��a��b�̊Ԃ̐��l��(Vector3�p)
    /// </summary>
    private bool IsBetween(Vector3 target, Vector3 a, Vector3 b)
    {
        Debug.Log(IsBetween(target.x, a.x, b.x) && IsBetween(target.y, a.y, b.y) && IsBetween(target.z, a.z, b.z));
        return IsBetween(target.x, a.x, b.x) && IsBetween(target.y, a.y, b.y) && IsBetween(target.z, a.z, b.z);
    }
}
