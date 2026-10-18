using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 90f;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Coin touched by {other.name}");
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
