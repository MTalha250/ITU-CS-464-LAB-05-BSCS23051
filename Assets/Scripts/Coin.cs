using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum Kind { Gold, Red, Blue }

    [SerializeField] Kind kind = Kind.Gold;
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Coin touched by {other.name}");
        if (other.CompareTag("Player"))
        {
            if (kind == Kind.Red) GameManager.Instance.CollectRedCoin();
            else if (kind == Kind.Blue) GameManager.Instance.CollectBonusCoin();
            else GameManager.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
}
