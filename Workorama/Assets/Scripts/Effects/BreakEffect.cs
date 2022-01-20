using UnityEngine;

/// <summary>
/// Class specialized in making a breakeffect for a specific object
/// </summary>
public class BreakEffect : MonoBehaviour
{
    // How many pieces should there be
    [SerializeField] private int piecesPerAxis;

    // Delay before breaking
    [SerializeField] private float delay;

    // Force applied to break object
    [SerializeField] private float force;

    // Radius defined for each piece to fall into
    [SerializeField] private float radius;

    // Time for the piece objects to get destroyed
    [SerializeField] private float destroyPiecesTime;

    // Check if object has collided
    private bool collided;

    private void Start() => collided = false;

    /// <summary>
    /// Detect collison and invoke the method to break object with delay
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        collided = true;

        if (collision.gameObject.tag == "Hammer")
        {
            if (collided == true)
            {
                FindObjectOfType<SoundManager>().Play("ShatterGlass");
                Invoke("CreateFractures", delay);
            }
        }
    }

    /// <summary>
    /// Create all the pieces in every position 
    /// </summary>
    private void CreateFractures()
    {
        for (int x = 0; x < piecesPerAxis; x++)
        {
            for (int y = 0; y < piecesPerAxis; y++)
            {
                for (int z = 0; z < piecesPerAxis; z++)
                    CreatePieces(new Vector3(x, y, z));
            }
        }

        //Destroy original object
        Destroy(gameObject);
    }

    /// <summary>
    /// Creation of each piece
    /// </summary>
    /// <param name="coordinates"> Set coordinate for the explosion of
    /// each piece </param>
    private void CreatePieces(Vector3 coordinates)
    {
        // Create a cube
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Set material to match its original object
        Renderer rd = piece.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        // Set scale to fraction of original cube
        piece.transform.localScale = transform.localScale / piecesPerAxis;

        // Set position of first piece by calculating the 2 centers of original
        // and first fractured piece
        Vector3 firstPiece = transform.position - transform.localScale / 2 +
            piece.transform.localScale / 2;

        // Other piece
        piece.transform.position = firstPiece + Vector3.Scale(coordinates,
            piece.transform.localScale);

        // Add rigidbody to each piece to fall and explode
        Rigidbody rb = piece.AddComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, radius);

        // Destroy pieces after timedelay
        if (destroyPiecesTime > 0)
            Destroy(piece, destroyPiecesTime);
    }
}
