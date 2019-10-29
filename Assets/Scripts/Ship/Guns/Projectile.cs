using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject explosionPrefab;

    public float speed;
    public float damage;

    public float maxLiveTime;

    public string ParentTag { get; private set; }

    private float liveTime;

    protected new Rigidbody2D rigidbody;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0f)
            Destroy();
    }

    public virtual void Launch(Gun gun, string parentTag, Vector2 movingDirection, float rotation)
    {
        liveTime = maxLiveTime;
        ParentTag = parentTag;
        rigidbody.rotation = rotation;
        rigidbody.velocity = movingDirection * speed;
    }

    public virtual void Destroy()
    {
        if (explosionPrefab)
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
