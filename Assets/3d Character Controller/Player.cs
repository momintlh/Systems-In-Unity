using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController cc;

    [SerializeField] float speed = 15f;

    [SerializeField] Vector3 moveDirection;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Start()
    {

    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        cc.Move(speed * Time.deltaTime * moveDirection);
    }
}