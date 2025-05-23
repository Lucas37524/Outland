using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    public GameObject winScreen;

    // ---- Player Death ---- //
    public Rigidbody rb;
    public bool isDead = false;
    public GameObject deathUI;

    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;

    // ---- Player Calories ---- //
    public float currentCalories;
    public float maxCalories;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    public GameObject playerBody;

    // ---- Player Hydration ---- //
    public float currentHydrationPercent;
    public float maxHydrationPercent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(DecreaseHydration());
        StartCoroutine(CheckHealthDecay()); // Start health decay coroutine
    }

    IEnumerator DecreaseHydration()
    {
        while (true)
        {
            if (currentHydrationPercent > 0)
            {
                currentHydrationPercent -= 1;
            }

            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator CheckHealthDecay()
    {
        while (true)
        {
            if (currentHydrationPercent < 20 || currentCalories < 300)
            {
                currentHealth -= 2;
                currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0

                if (currentHealth <= 0)
                {
                    Die();
                }
            }

            yield return new WaitForSeconds(2); // Health decreases every 5 seconds if conditions are met
        }
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled >= 3)
        {
            distanceTravelled = 0;
            if (currentCalories > 0)
            {
                currentCalories -= 1;
            }
        }
    }

    public void setHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void setCalories(float newCalories)
    {
        currentCalories = Mathf.Clamp(newCalories, 0, maxCalories);
    }

    public void setHydration(float newHydration)
    {
        currentHydrationPercent = Mathf.Clamp(newHydration, 0, maxHydrationPercent);
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        

        Debug.Log("Player is dead");

        // Lock movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // Show death UI
        if (deathUI != null)
        {
            deathUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void gameWon()
    {
        if (isDead) return;

        isDead = true;



        Debug.Log("Player has won");

        // Lock movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // Show death UI
        if (winScreen != null)
        {
            winScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
