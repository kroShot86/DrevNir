using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float speed = 10f;
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector2 dashDir;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        GameInput.Instance.OnDashAction += GameInput_OnDashAction;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        
    }

    private void GameInput_OnDashAction(object sender, System.EventArgs e)
    {
        if (!isDashing && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashTime;
            dashCooldownTimer = dashCooldown;
            
            Vector2 moveDir = GameInput.Instance.GetMovementVector().normalized;
            if (moveDir == Vector2.zero) dashDir = Vector2.right; 
            else dashDir = moveDir;
        }
    }

    private void Update()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            HandleDash();
        }
        else
        {
            HandleMovement();
        }
    }

    private void HandleDash()
    {
        rb.MovePosition(rb.position + dashDir * (dashSpeed * Time.fixedDeltaTime));
        dashTimer -= Time.fixedDeltaTime;
        if (dashTimer <= 0)
        {
            isDashing = false;
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector().normalized;
        rb.MovePosition(rb.position + inputVector * (speed * Time.fixedDeltaTime));
    }

    public Vector2 Player_pos()
    {
        return GameInput.Instance.GetMovementVector();
    }
}