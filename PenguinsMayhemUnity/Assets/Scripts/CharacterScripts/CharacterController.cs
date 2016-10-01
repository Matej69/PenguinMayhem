using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    Profile profile;
    Weapon weaponScript;
    Bounds CollBounds;

    Animator animator;

    int mask;
    float skin = 0.02f;

    [HideInInspector]
    public Vector2 velocity;
    float maxSpeedHoriz = 3.5f;
    float speedHoriz = 1;
    float accHoriz = 0.25f;

    float gravity = 0.3f;
    bool isGrounded = false;
    public struct JumpingInfo{
        public float jumpDec;
        public float startingJumpForce;
        public float jumpForce;
        public bool isJumping;
        public JumpingInfo(bool a) { jumpDec = 1; jumpForce = 4.7f; startingJumpForce = 4.7f; isJumping = false; }
        public float getCurrentJumpForce() { return (jumpForce = (jumpForce > 0 && jumpForce <= startingJumpForce) ? jumpForce - jumpDec : 0); }
        public void ResetJump() { jumpForce = startingJumpForce; isJumping = false; }
    }
    JumpingInfo jumpingInfo = new JumpingInfo(true);

    public struct RaysInfo{
        public Vector2 startingRayPos;
        public int numOfRays;
        public float rayDistance;                  
    }
    



    // Use this for initialization
    void Start () {
        profile = gameObject.GetComponent<CharacterAttributes>().profilePointer;
        weaponScript = transform.FindChild("Gun").gameObject.GetComponent<Weapon>();
        CollBounds = GetComponent<BoxCollider2D>().bounds;
        animator = transform.FindChild("Penguin").gameObject.GetComponent<Animator>();

        mask = LayerMask.NameToLayer("Platform");
        velocity = new Vector2(0, 0);
        
    }
	
	// Update is called once per frame
	void LateUpdate() {

        CalculateVelocityOnInput();
        ApplyJump();
        ApplyGravity();
        HorizontalCollision(ref velocity);
        VerticalCollision(ref velocity);
        MoveBy(velocity);
        


    }

    void CalculateVelocityOnInput(){
        //HORIZONTAL MOVEMENT
        if (InputManager.keyHold[profile.keyConfigMap[Profile.keyType.LEFT]]) {
            AddSpeedUntilMax();
            velocity = new Vector2(-1 * speedHoriz * Time.deltaTime, velocity.y);
            SetScaleDirX(-1);
            animator.SetBool("isWalking", true);
        }
        else if (InputManager.keyHold[profile.keyConfigMap[Profile.keyType.RIGHT]]) {
            AddSpeedUntilMax();
            velocity = new Vector2(speedHoriz * Time.deltaTime, velocity.y);
            SetScaleDirX(1);
            animator.SetBool("isWalking", true);            
        }
        else { 
            velocity = new Vector2(0, velocity.y);
            speedHoriz = 0;
            animator.SetBool("isWalking", false);            
        }
        //VERTICAL MOVEMENT
        if (InputManager.keyHold[profile.keyConfigMap[Profile.keyType.JUMP]] && isGrounded && !jumpingInfo.isJumping && !IsFalling()){
            jumpingInfo.isJumping = true;
            isGrounded = false;
         }
        //SHOOTING
        if (InputManager.keyHold[profile.keyConfigMap[Profile.keyType.SHOT]]) {
            if (weaponScript != null && weaponScript.CanShoot() ) {     //if gun is destroyed we will not be able to shot (weaponScript == null)
                weaponScript.Shot();
            }
        }
    }


    void HorizontalCollision(ref Vector2 deltaMovement) {
        Vector2 position = gameObject.transform.position;
        //info about ray
        RaysInfo rayInfo = new RaysInfo();
        rayInfo.numOfRays = 8;
        rayInfo.rayDistance = CollBounds.size.y / (rayInfo.numOfRays - 1);
        //get side player is turned on -> calculate starting ray cordinate
        rayInfo.startingRayPos = new Vector2(0, position.y - CollBounds.extents.y + skin);
        rayInfo.startingRayPos.x = (velocity.x > 0) ? position.x + CollBounds.extents.x : position.x - CollBounds.extents.x;
        //loop and create every ray
        for (int i = 0; i < rayInfo.numOfRays; ++i)
        {
            Vector2 curRayPos = rayInfo.startingRayPos;
            curRayPos.y = rayInfo.startingRayPos.y + (rayInfo.rayDistance * i);
            Ray ray = (IsFacingRight()) ? new Ray(curRayPos, Vector3.right) : new Ray(curRayPos, Vector3.left);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(deltaMovement.x), 1 << mask);
            if (hit){
                velocity.x = (IsFacingRight()) ? hit.distance : -hit.distance;
            }
            Debug.DrawRay(ray.origin, deltaMovement);
        }
    }

    void VerticalCollision(ref Vector2 deltaMovement){
        Vector2 position = gameObject.transform.position;
        //info about ray
        RaysInfo rayInfo = new RaysInfo();
        rayInfo.numOfRays = 8;
        rayInfo.rayDistance = (CollBounds.size.x - skin*2) / (rayInfo.numOfRays - 1);
        //get side player is turned on -> calculate starting ray cordinate
        rayInfo.startingRayPos = new Vector2(position.x - CollBounds.extents.x + skin, 0);
        rayInfo.startingRayPos.y = (IsJumping()) ? position.y + CollBounds.extents.y : position.y - CollBounds.extents.y;
        //loop and create every ray
        for (int i = 0; i < rayInfo.numOfRays; ++i)
        {
            Vector2 curRayPos = rayInfo.startingRayPos;
            curRayPos.x = rayInfo.startingRayPos.x + (rayInfo.rayDistance * i);
            Ray ray = (IsJumping()) ? new Ray(curRayPos, Vector3.up) : new Ray(curRayPos, Vector3.down);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(deltaMovement.y), 1 << mask);
            if (hit){
                if (IsFalling()) {
                    velocity.y = -hit.distance;
                    isGrounded = true;
                }
                else {
                    velocity.y = 0;
                    jumpingInfo.isJumping = false;
                }
            }
            Debug.DrawRay(ray.origin, deltaMovement);
        }
    }

    
    void ApplyGravity(){
        velocity.y -= gravity * Time.deltaTime;
    }
    void ApplyJump() {
        if (jumpingInfo.jumpForce <= 0 || jumpingInfo.jumpForce > jumpingInfo.startingJumpForce)
            jumpingInfo.ResetJump();
        else if (jumpingInfo.isJumping)
            velocity.y += jumpingInfo.getCurrentJumpForce() * Time.deltaTime;
    }
    void MoveBy(Vector2 _velocity){
        transform.Translate(_velocity);
    }

    void AddSpeedUntilMax() {
        if (speedHoriz < maxSpeedHoriz)
            speedHoriz += accHoriz;
    }
    


    //*************Get info about object**************
    public bool IsFacingRight(){
        return gameObject.transform.localScale.x >= 0;
    }
    bool IsJumping(){
        return velocity.y > 0;
    }
    bool IsFalling(){
        return velocity.y < 0;
    }
    void SetScaleDirX(int _scaleValue) {
        if (_scaleValue == 1)
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        else if (_scaleValue == -1)
            gameObject.transform.localScale = new Vector2(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
    }



}
