using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; // 1
    public float gotHayDestroyDelay; // 2
    private bool hitByHay; // 3

    public float dropDestroyDelay; // 1
    private Collider myCollider; // 2
    private Rigidbody myRigidbody; // 3

    private SheepSpawner sheepSpawner;

    public float heartOffset; // 1
    public GameObject heartPrefab; // 2

    private bool hasBeenDropped = false;

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Dog"))
        {
            transform.Translate(Vector3.back * runSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        }
    }
    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);

        hitByHay = true; // 1
        runSpeed = 0; // 2

        Destroy(gameObject, gotHayDestroyDelay); // 3

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; // 1
        tweenScale.targetScale = 0; // 2
        tweenScale.timeToReachTarget = gotHayDestroyDelay; // 3

        SoundManager.Instance.PlaySheepHitClip();

        GameStateManager.Instance.SavedSheep();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);

            if (gameObject.CompareTag("Dog"))
            {
                GameStateManager.Instance.GameOver();

                SoundManager.Instance.PlaySheepDroppedClip();

                Destroy(gameObject);
            }
            else
            {
                HitByHay();
            }

        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }
    private void Drop()
    {
        if (hasBeenDropped) return;
        hasBeenDropped = true;

        GameStateManager.Instance.DroppedSheep();
        sheepSpawner.RemoveSheepFromList(gameObject);

        myRigidbody.isKinematic = false; // 1
        myCollider.isTrigger = false; // 2
        Destroy(gameObject, dropDestroyDelay); // 3

        SoundManager.Instance.PlaySheepDroppedClip();
    }

}
