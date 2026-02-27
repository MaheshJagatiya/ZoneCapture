using UnityEngine;

//manage Player Movement
//Work both for mobile,Desktop and Editor
//desktop and editor work with arrow key
//mobile work with Joystick
//Player speed can manage by Speed perameter
//Enemy detection with collider
//Player face movement with thier direction

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] FixedJoystick joystick;
    bool IsJoystic = false;

    private void Start()
    {
        transform.position = GridManager.Instance.GridToWorld(0, 0);

#if UNITY_EDITOR || UNITY_STANDALONE
        joystick.gameObject.SetActive(false);
        joystick = null;
#else
         joystick.gameObject.SetActive(true);
         IsJoystic = true;
#endif

        //IsJoystic = true;
    }
    void Update()
    {
        float h = IsJoystic ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");
        float v = IsJoystic ? joystick.Vertical : Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0) return; // skip if no input

        Vector2 dir;

        if (Mathf.Abs(h) > Mathf.Abs(v))
            dir = new Vector2(Mathf.Sign(h), 0);
        else
            dir = new Vector2(0, Mathf.Sign(v));

        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        RotatePlayer(dir);
        ClampPosition();
    }
    void RotatePlayer(Vector2 dir)
    {
        if (dir == Vector2.zero) return;

        if (dir == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        else if (dir == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, 180);

        else if (dir == Vector2.up)
            transform.rotation = Quaternion.Euler(0, 0, 90);

        else if (dir == Vector2.down)
            transform.rotation = Quaternion.Euler(0, 0, -90);
    }
    void ClampPosition()
    {
        var g = GridManager.Instance;

        float minX = -g.width / 2f;
        float maxX = g.width / 2f - 1;

        float minY = -g.height / 2f;
        float maxY = g.height / 2f - 1;

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            GameEvents.OnPlayerDied?.Invoke();
    }
    //if (h != 0)
    //    dir = new Vector2(h, 0);
    //else if (v != 0)
    //    dir = new Vector2(0, v);

}
