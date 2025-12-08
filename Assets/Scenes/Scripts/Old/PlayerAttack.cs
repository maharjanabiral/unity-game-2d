using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _comboWindow = 1.0f;
    private Animator _animator;
    private int _comboStep = 0;
    private float _lastAttackTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var attackInput= Input.GetButtonDown("Attack");
        //attack animation trigger
        if(attackInput)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(Time.deltaTime - _lastAttackTime > _comboWindow)
        {
            Debug.Log("Resetting combo");
            _comboStep = 1;
        }
        else _comboStep++;
        _comboStep = Mathf.Clamp(_comboStep, 1, 2);
        _animator.SetInteger("ComboStep", _comboStep);
        _animator.SetTrigger("Attack");
        _lastAttackTime = Time.deltaTime;

    }
}
