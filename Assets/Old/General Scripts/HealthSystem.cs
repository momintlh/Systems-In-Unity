using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public float myHealth;

    void OnEnable()
    {
        PlayerController.takeDamage += TakeDamage;
    }

    void OnDisable()
    {

        PlayerController.takeDamage -= TakeDamage;
    }


    public void TakeDamage(float damageAount)
    {
        myHealth -= damageAount;

        if (myHealth <= 0)
        {
            myHealth = 0;
            Debug.LogErrorFormat("Health is 0!");
            gameObject.SetActive(false);
        }
    }
}
