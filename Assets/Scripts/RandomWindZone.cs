using UnityEngine;

public class RandomWindZone : MonoBehaviour
{
    private Transform target;

    private float leftTime;

    private void Awake()
    {
        leftTime = Random.Range(1, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock")) target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock")) target = null;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        leftTime -= Time.fixedDeltaTime;

        if (leftTime > 0) return;

        leftTime = Random.Range(1, 2);

        float x = Random.Range(-1000, 1000) * Mathf.Clamp(target.localPosition.y / 5, 1, 4);
        float z = Random.Range(-1000, 1000) * Mathf.Clamp(target.localPosition.y / 5, 1, 4);

        target.GetComponent<Rigidbody>().AddForce(new Vector3(x, 0, z) * Time.fixedDeltaTime);
    }
}
