using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerBehaviour : MonoBehaviour
{
    private CharacterController CharacterController;
    
    private const float Lane_Distance = 1.0f;
    private const float TurnSpeed = 0.05f;

    //
    private bool isRunning = false;

    // Speed Modifier
    private float originalSpeed = 5.0f;
    private float speed;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.0f;
    private float speedIncreaseAmount = 0.1f;


    private float lane = 1.0f;
    private float verticalVelocity;
    private float gravity = 12;
    private float jumpForce = 3f;
    

    private Animator anim;

    public  bool alive = true;
    void Start()
    {
        speed = originalSpeed;
        CharacterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        
    if(!isRunning)
        {
            return;
        }

    
    
  /*  if(!alive)
        {
          // Death();
        } */

    if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            //Change the modifier text
            GameManager.instance.UpdateModifier(speed - originalSpeed);
        }

        // Gatherer of inputs on which lane player should be
        if (MobileInputs.Instance.SwipeLeft)
        {
            moveLane(false);
        }
        if (MobileInputs.Instance.SwipeRight)
        {
            moveLane(true);
        }

        // Calculate future position
        Vector3 targetPos = transform.position.z * Vector3.forward;

        if (lane == 0)
        {
            targetPos += Vector3.left * Lane_Distance;
        }
        if (lane == 2)
        {
            targetPos += Vector3.right * Lane_Distance;
        }

        // moveDelta Calculate
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPos - transform.position).normalized.x * speed;

        bool Grounded = isGrounded();
        anim.SetBool("Grounded", Grounded);

        if (Grounded) // check if Grounded
        {
            verticalVelocity = -0.1f;


            if (MobileInputs.Instance.SwipeUp)
            {
                //jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            //   fast fall mechanic

            if(MobileInputs.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            } 

        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //MoveCharacter
        CharacterController.Move(moveVector * Time.deltaTime);

        //Rotate Character with movement
        Vector3 dir = CharacterController.velocity;
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, TurnSpeed);


    }

    private void moveLane(bool goingRight)
    {
        lane += (goingRight) ? 1 : -1;
        lane = Mathf.Clamp(lane, 0, 2);

    }

    private bool isGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3(CharacterController.bounds.center.x,
            (CharacterController.bounds.center.y - CharacterController.bounds.extents.y) + 0.2f,
            CharacterController.bounds.center.z),
            Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.red, 1.0f);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

  /*  public  void Death()
    {
        alive = false;
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    } */

    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.instance.OnDeath();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch(hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }

}
