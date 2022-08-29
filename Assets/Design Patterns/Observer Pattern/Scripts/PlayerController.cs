using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Animation Setting")]
    Animator anim;
    static readonly int Idle = Animator.StringToHash("Idle");
    static readonly int Hurt = Animator.StringToHash("Hurt");

    [SerializeField] float jumpForce;

    private Rigidbody2D rb;

    public delegate void TakeDamage(float damageAount);
    public static event TakeDamage takeDamage;
    public float damageAmount;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // void OnEnable()
    // {
    //     MouseInput.OnLmbDown += HurtState;
    //     MouseInput.OnLmbUp += IdleState;
    // }

    // void OnDisable()
    // {
    //     MouseInput.OnLmbDown += HurtState;
    //     MouseInput.OnLmbUp += IdleState;
    // }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void IdleState()
    {
        // change sprite to hurt, animate like a programmer
        anim.CrossFade(Idle, 0f, 0);
        Debug.Log("Idle state!");

    }

    public void HurtState(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            anim.CrossFade(Hurt, 0f, 0);
            takeDamage?.Invoke(damageAmount);
            Jump();
            Debug.Log("Hurt state!" + context.phase);
        }
        else
        {
            IdleState();
        }

    }


    //temp
    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // TODO: create a health system and health bar 

}
