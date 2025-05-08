using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    public float swimSpeed = 2f;
    public float changeDirectionInterval = 2f;
    public BoxCollider swimArea; // Reference to the BoxCollider that defines the swimming area
    public float rotationSpeed = 5f; // Speed at which the fish rotates to face the direction
    private Vector3 targetPosition;
    private float timer;

    void Start()
    {
        if (swimArea == null)
        {
            Debug.LogError("No Swim Area assigned to the fish!");
            return;
        }
        SetNewTargetPosition();
    }

    void Update()
    {
        MoveFish();
    }

    void MoveFish()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            timer = 0f;
            SetNewTargetPosition();
        }

        // Move the fish towards the target position
        Vector3 directionToTarget = targetPosition - transform.position;

        // Rotate towards the target position
        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, swimSpeed * Time.deltaTime);
    }

    void SetNewTargetPosition()
    {
        // Get random coordinates within the swim area for X and Z
        float x = Random.Range(swimArea.bounds.min.x, swimArea.bounds.max.x);
        float z = Random.Range(swimArea.bounds.min.z, swimArea.bounds.max.z);

        // For Y, randomize within the swim area's Y bounds
        float y = Random.Range(swimArea.bounds.min.y, swimArea.bounds.max.y);

        targetPosition = new Vector3(x, y, z);
    }
}
