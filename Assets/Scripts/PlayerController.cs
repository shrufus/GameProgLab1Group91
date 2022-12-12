using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed = 3;
    [SerializeField] private bool FacingRight;

    [SerializeField] private Vector2 moveDir;
    private BoxCollider2D _boxCollider2D;

    public float xTransform, yTransform;

    #endregion Fields

    private void Start()
    {
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    #region Movement

    

    #endregion Movement

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down * 0.1f, groundLayerMask);
        Debug.Log(raycastHit2D.collider.IsTouchingLayers(groundLayerMask));
        return raycastHit2D.collider.IsTouchingLayers(groundLayerMask);
    }

   
}