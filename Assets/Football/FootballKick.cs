using UnityEngine;

public class FootballKick : MonoBehaviour
{
    public Camera mainCamera;


    public float currentForce = 0f;
    public float maxForce = 20f;
    public float chargeSpeed = 5f;
    public bool isCharging = false;

    public float heightMultiplier = 0.3f;
    public float maxHeightMultiplier = 1;

    public Rigidbody rb;

    public GameObject aimIndicator;
    public float aimSpeed = 5;
    public float angle;
    public float radius = 2f;
    public Vector3 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Aim();
        ChargeForce();
        LowPass();
        HighPass();

        // if (rb.linearVelocity.magnitude > 0.25)
        // {
        //     aimIndicator.SetActive(false);
        // }
        // else
        // {
        //     aimIndicator.SetActive(true);
        // }
    }

    private void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            dir = hit.point - gameObject.transform.position;
            dir.y = 0f;
            dir.Normalize();

            aimIndicator.transform.position = gameObject.transform.position + (dir * radius);
            aimIndicator.transform.LookAt(gameObject.transform.position);
            aimIndicator.transform.rotation = Quaternion.Euler(-90, aimIndicator.transform.rotation.eulerAngles.y, 0);
        }
    }

    private void ChargeForce()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            currentForce = 0f;
        }

        if (isCharging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            heightMultiplier = Mathf.Lerp(0.3f, maxHeightMultiplier, currentForce / maxForce);

            currentForce = Mathf.Clamp(currentForce, 0f, maxForce);
            heightMultiplier = Mathf.Clamp(heightMultiplier, 0f, maxHeightMultiplier);
        }
    }

    private void LowPass()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(dir * currentForce, ForceMode.Impulse);
            isCharging = false;
        }
    }

    private void HighPass()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 passDir = dir + (Vector3.up * heightMultiplier);
            passDir.Normalize();
            rb.AddForce(passDir * currentForce, ForceMode.Impulse);
            isCharging = false;
        }
    }

}