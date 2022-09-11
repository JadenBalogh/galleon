using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Boat
{
    private const float VIEWPORT_FIRE_VERTICAL_OFFSET = 0.2f;

    public static Player Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private DeathScreen deathScreen;
    [SerializeField] private float shootCooldown = 1f;

    private bool canShoot = true;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    protected override void Start()
    {
        ChannelIndex = -1;
        base.Start();
    }

    protected override void UpdatePosition()
    {
        if (!GameManager.IsStarted)
        {
            return;
        }

        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootCooldown());

            Vector2 viewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (Mathf.Abs(viewportPos.y - 0.5f) > VIEWPORT_FIRE_VERTICAL_OFFSET)
            {
                FireCannon(viewportPos.y > 0.5f ? 1 : -1);
            }
            else
            {
                int moveDir = viewportPos.x > 0.5f ? 1 : -1;
                moveIndex = Mathf.Clamp(moveIndex + moveDir, 0, movePositions.Length - 1);
                targetPos = movePositions[moveIndex].position;
            }
        }
    }

    public override void AddHealth(int healing)
    {
        base.AddHealth(healing);
        healthText.text = "Health: " + health + "/" + maxHealth;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthText.text = "Health: " + health + "/" + maxHealth;
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    public override void Sink()
    {
        base.Sink();
        deathScreen.EndGame();
    }
}
