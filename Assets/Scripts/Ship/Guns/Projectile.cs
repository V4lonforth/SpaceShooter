using UnityEngine;

public class Projectile : MonoBehaviour
{
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

    public void Launch(string parentTag, Vector2 direction)
    {
        liveTime = maxLiveTime;
        ParentTag = parentTag;
        rigidbody.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody.velocity = direction * speed;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
