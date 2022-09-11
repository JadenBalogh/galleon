using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Boat : MonoBehaviour
{
    [SerializeField] private Animation2D wakeAnim;
    [SerializeField] private Animation2D wreckAnim;
    [SerializeField] protected Cannonball cannonballPrefab;
    [SerializeField] protected float cannonballSpeed = 5f;
    [SerializeField] protected Transform topCannon;
    [SerializeField] protected Transform bottomCannon;
    [SerializeField] public Transform[] movePositions;
    [SerializeField] protected float moveTime = 0.5f;
    [SerializeField] private float sunkFloatSpeed = 2f;
    [SerializeField] private float fadeTime = 2f;
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] private Color flashDamageColor = Color.red;
    [SerializeField] private Color flashHealingColor = Color.green;
    [SerializeField] private float flashDuration = 0.1f;

    public int ChannelIndex { get; set; }
    public UnityEvent<int> OnSunk { get; set; }

    protected int moveIndex = 1;
    protected Vector2 targetPos = Vector2.zero;
    private Vector2 currVel;
    private bool isSunk = false;
    protected int health;

    private SpriteRenderer spriteRenderer;
    private new Collider2D collider2D;
    private Animator2D animator2D;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        animator2D = GetComponent<Animator2D>();
        OnSunk = new UnityEvent<int>();

        health = maxHealth;
    }

    protected virtual void Start()
    {
        animator2D.Play(wakeAnim, true);
    }

    protected virtual void Update()
    {
        if (isSunk)
        {
            transform.position += Vector3.right * sunkFloatSpeed * Time.deltaTime;
            return;
        }

        UpdatePosition();
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref currVel, moveTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected void FireCannon(int direction)
    {
        if (isSunk)
        {
            return;
        }

        Vector2 cannonPos = direction > 0 ? topCannon.position : bottomCannon.position;
        Vector2 cannonVel = Vector2.up * direction * cannonballSpeed;

        Cannonball cannonball = Instantiate(cannonballPrefab, cannonPos, Quaternion.identity);
        cannonball.Fire(gameObject.tag, cannonVel);
    }

    public virtual void AddHealth(int healing)
    {
        health = Mathf.Min(health + healing, maxHealth);
        StartCoroutine(FlashDamage(flashHealingColor));
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashDamage(flashDamageColor));

        if (health <= 0)
        {
            Sink();
        }
    }

    private IEnumerator FlashDamage(Color flashColor)
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = Color.white;
    }

    public virtual void Sink()
    {
        if (isSunk) return;

        isSunk = true;
        collider2D.enabled = false;
        animator2D.Play(wreckAnim, true);
        spriteRenderer.sortingOrder = -1;
        OnSunk.Invoke(ChannelIndex);

        StartCoroutine(FadeTimer());
    }

    private IEnumerator FadeTimer()
    {
        float fadeTimer = 0;
        while (fadeTimer < fadeTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1 - fadeTimer / fadeTime);
            fadeTimer += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    protected abstract void UpdatePosition();
}
