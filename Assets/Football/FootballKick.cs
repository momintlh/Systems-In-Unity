using UnityEngine;

public class FootballKick : MonoBehaviour
{
    public Camera mainCamera;
    public float kickForce = 5f;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            dir = hit.point - gameObject.transform.position;
            dir.y = 0f;
            dir.Normalize();

            aimIndicator.transform.position = gameObject.transform.position + dir * radius;
            aimIndicator.transform.LookAt(gameObject.transform.position);
            aimIndicator.transform.rotation = Quaternion.Euler(-90, aimIndicator.transform.rotation.eulerAngles.y, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(dir * kickForce, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 passDir = dir + Vector3.up * 0.3f;
            rb.AddForce(passDir * kickForce, ForceMode.Impulse);
        }
    }
}
