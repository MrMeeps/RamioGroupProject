using System.Collections;
using UnityEngine;
public class EnemyAI : MonoBehaviour
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
    [Header("Movement Settings")]
    public Vector2 moveDir;
    public int moveSpeed;
    public int paceDuration;
    float moveTimer = 0;
    GameObject player;
    Vector2 chaseDirection;
    bool left;
    [Header("Animation Settings")]
    public Animator animator;
    public float attackAnimationDuration;
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
        chaseDirection = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        //Engage Range
        if (chaseDirection.magnitude < engageRange)
            Chase();
        if (chaseDirection.magnitude < hitRange)
            StartCoroutine(Attack());
        //Moving
        if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            moveTimer += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = moveDir * moveSpeed;
        }
        //Pace Switch
        if (moveTimer > paceDuration)
            PaceSwitch();
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
            Gizmos.DrawWireSphere(hitPoint.position, attackRange);
        else
            return;
    }
    #endregion
    //ENEMY AI FUNCTIONS
    #region PACE SWITCH FUNCTION
    void PaceSwitch()
    {
        left = !left;
        moveDir *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        moveTimer = 0;
    }
    #endregion
    #region ATTACK FUNCTION
    IEnumerator Attack()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackAnimationDuration);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Collider2D[] playerHits = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, playerLayerMask);
        foreach (Collider2D player in playerHits)
            player.GetComponent<PlayerCollision>().TakeDamage(attackDamage);
        attackTimer = 0;
    }
    #endregion
    #region
    void Chase()
    {
        if (player.transform.position.x - gameObject.transform.position.x > 0)
            moveDir = new Vector2(1, 0);
        else
            moveDir = new Vector2(-1, 0);
    }
    #endregion
}