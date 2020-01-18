#region NAMESPACES
using System.Collections;
using UnityEngine;
#endregion
public class ToadEnemyAI : MonoBehaviour
{
    #region VARIABLES
    [Header("Weapon Settings")]
    public Weapon weapon;
    [HideInInspector] public int attackDamage;
    [Header("Combat Settings")]
    public Transform hitPoint;
    public float hitRange;
    public float attackRange;
    public float engageRange;
    public float arrowSpeed = 10f;
    public float attackTimer;
    public float attackDelay;
    public LayerMask playerLayerMask;
    bool attacking;
    [Header("Movement Settings")]
    public Vector2 moveDir;
    public int moveSpeed;
    public int paceDuration;
    float moveTimer = 0;
    GameObject player;
    Vector2 playerDistance;
    bool left;
    bool chase;
    [Header("Animation Settings")]
    public Animator animator;
    public float attackAnimationDuration_A;
    public float attackAnimationDuration_B;
    #endregion
    //UNITY FUNCTIONS
    #region START FUNCTION
    void Start()
    {
        player = GameObject.Find("Player");
        attackDamage = weapon.damage;
    }
    #endregion
    #region UPDATE FUNCTION
    void Update()
    {
        attackTimer += Time.deltaTime;
        playerDistance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        if (playerDistance.magnitude < hitRange && attackTimer > attackDelay)
            StartCoroutine(Attack());
        else if (playerDistance.magnitude < engageRange && attacking == false)
            Chase();
        else if (moveTimer > paceDuration && attacking == false)
            Pace();
        else if(attacking == false)
        {
            moveTimer += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
        }
    }
    #endregion
    #region ON DRAW GIZMO SELECTED FUNCTION
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(256, 0, 0);
        if (transform.position != null)
            Gizmos.DrawWireSphere(transform.position, engageRange);
        else
            return;
        if (hitPoint != null)
        {
            Gizmos.color = new Color(0, 0, 256);
            Gizmos.DrawWireSphere(hitPoint.position, attackRange);
            Gizmos.color = new Color(0, 256, 0);
            Gizmos.DrawWireSphere(hitPoint.position, hitRange);
        }
        else
            return;
    }
    #endregion
    //ENEMY AI FUNCTIONS
    #region ATTACK FUNCTION
    IEnumerator Attack()
    {
        attacking = true;
        attackTimer = 0;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackAnimationDuration_A);
        animator.SetTrigger("CompleteAttack");
        Collider2D[] playerHits = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, playerLayerMask);
        foreach (Collider2D player in playerHits)
            player.GetComponent<PlayerCollision>().TakeDamage(attackDamage);
        yield return new WaitForSeconds(attackAnimationDuration_B);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        attacking = false;
    }
    #endregion
    #region PACE FUNCTION
    void Pace()
    {
        moveDir *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        moveTimer = 0;
    }
    #endregion
    #region CHASE FUNCTION
    void Chase()
    {
        if (player.transform.position.x - gameObject.transform.position.x > 0 && moveDir != new Vector2(1, 0))
        {
            moveDir = new Vector2(1, 0);
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (player.transform.position.x - gameObject.transform.position.x < 0 && moveDir != new Vector2(-1, 0))
        {
            moveDir = new Vector2(-1, 0);
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
    }
    #endregion
}