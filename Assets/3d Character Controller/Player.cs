using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] Animator animator;

    [SerializeField] float speed = 15f;

    [SerializeField] Vector3 moveDirection;

    [SerializeField]
    private float rotationSpeed = 5;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Start()
    {

    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        animator.SetBool("isRunning", input.magnitude != 0);

        if (input.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        cc.Move(speed * Time.deltaTime * input.normalized);
    }
}