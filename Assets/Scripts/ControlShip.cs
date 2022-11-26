using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class ControlShip : MonoBehaviour
{
    float aspect = (float)Screen.width / Screen.height;
    Camera cam;
    float height;
    float width;

    float shipMoveSpeed;
    float shootSpeed;
    float shootRange;
    float aimDegree = 35f;
    
    public float BounceHeight = 0.0001f;
    public float BounceRate = 3.0f;
    public float BounceSync = -0.75f;

    Vector2 currentMovement;
    Vector2 currentPos;

    Transform harpoon;
    Transform harpoonHolder;
    Transform harpoonString;
    Vector3 harpoonBasePos;
    Vector3 harpoonHolderRot;
    Vector3 harpoonRotBase;

    public ControlHarpoon harpoonControl;

    bool isMovementLocked = false;
    bool isHarpoonShot = false;

    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2;
        width = height * aspect;      

        currentMovement = Vector2.zero;
        currentPos = transform.position;

        harpoonHolder = transform.GetChild(0);
        harpoonHolderRot = harpoonHolder.rotation.eulerAngles;
        
        harpoon = harpoonHolder.GetChild(0).transform;
        harpoonBasePos = harpoon.position;
        harpoonRotBase = new Vector3(0f, 0f, -aimDegree);
        harpoonString = harpoonHolder.GetChild(1);
        Vector3 dir = (harpoon.position - harpoonHolder.position);

        Vector3 scale = harpoonString.localScale;
        float dist = (harpoon.position - harpoonHolder.position).magnitude;
        scale.y = dist;
        harpoonString.localScale = scale;
        harpoonString.position = transform.position + dir.normalized * (dir.magnitude / 2);
        harpoonString.position = transform.position + (harpoon.position - harpoonHolder.position).normalized * (dist / 2);
    }

    void Update(){
        shootSpeed = UpgradeManager.Instance.harpoonSpeed;
        shootRange = UpgradeManager.Instance.harpoonRange;
        shipMoveSpeed = UpgradeManager.Instance.shipSpeed;
        
        Vector3 pos = transform.position;
        pos += new Vector3(currentMovement.x, currentMovement.y, 0f) * shipMoveSpeed * Time.deltaTime;
        pos.x = Mathf.Max(cam.transform.position.x - width + 10, Mathf.Min(cam.transform.position.x + width - 10, pos.x));
        transform.position = pos;
        
        Vector3 scale = harpoonString.localScale;
        float dist = (harpoon.position - harpoonHolder.position).magnitude;
        scale.y = dist;
        harpoonString.localScale = scale;
        harpoonString.position = transform.position + (harpoon.position - harpoonHolder.position).normalized * (dist / 2);
        // float t = Time.time * BounceRate + transform.position.y * BounceSync;
        // float bounce = (float)(Mathf.Sin (t)) * BounceHeight;
        // transform.position = new Vector3(transform.position.x, transform.position.y + bounce, transform.position.z);
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            Vector2 movement = context.ReadValue<Vector2>();
            if (!isMovementLocked){
                if (movement.y < -0.5f){
                    AimHarpoon();
                }
                movement.y = 0f;
                currentMovement = movement;
            }
            else if (!isHarpoonShot){
                if (movement.y < -0.5f){
                    ShootHarpoon();
                }
            }
        }
        else if (context.phase == InputActionPhase.Canceled){
            currentMovement = Vector2.zero;
        }
    }

    IEnumerator LerpHarpoonPosition(Vector2 endValue, float duration)
    {
        harpoonBasePos = harpoon.position;
        //to position
        float time = 0;
        Vector2 startValue = harpoon.position;
        while (time < duration)
        {
            harpoon.position = Vector2.Lerp(startValue, endValue, time / duration);
            harpoon.position += Vector3.back;
            time += Time.deltaTime;
            yield return null;
        }
        harpoon.position = endValue;
        harpoon.position += Vector3.back;

        //comeback
        harpoon.GetComponent<Collider2D>().enabled = false;
        startValue = harpoon.position;
        endValue = harpoonBasePos;
        time = 0;
        while (time < duration)
        {
            harpoon.position = Vector2.Lerp(startValue, endValue, time / duration);
            harpoon.position += Vector3.back;
            time += Time.deltaTime;
            yield return null;
        }
        harpoon.position = endValue;
        harpoon.position += Vector3.back;
        isMovementLocked = false;
        isHarpoonShot = false;
        harpoonControl.CollectAllCaughtFishes();
        harpoonControl.CollectAllCaughtTrashes();
    }

     IEnumerator LerpHarpoonRotation()
    {
        while (!isHarpoonShot){
            float duration = 1.2f;
            //to right
            float time = 0f;
            float targetAngle = aimDegree;
            float startAngle = -aimDegree;
            float currentAngle = startAngle;
            while (time < duration)
            {
                if (isHarpoonShot){
                    break;
                }
                currentAngle = Mathf.Lerp(startAngle, targetAngle, time / duration);
                harpoonHolder.rotation = Quaternion.Euler(0f, 0f, currentAngle);
                time += Time.deltaTime;
                yield return null;
            }
            if (isHarpoonShot){
                break;
            }
            harpoonHolder.rotation = Quaternion.Euler(0f, 0f, targetAngle);

            //to left
            time = 0f;
            targetAngle = -aimDegree;
            startAngle = aimDegree;
            currentAngle = startAngle;
            while (time < duration)
            {
                if (isHarpoonShot){
                    break;
                }
                currentAngle = Mathf.Lerp(startAngle, targetAngle, time / duration);
                harpoonHolder.rotation = Quaternion.Euler(0f, 0f, currentAngle);
                time += Time.deltaTime;
                yield return null;
            }
            if (isHarpoonShot){
                break;
            }
            harpoonHolder.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        }
    }

    void ShootHarpoon() {
        isHarpoonShot = true;
        StartCoroutine(LerpHarpoonPosition(harpoon.TransformPoint(harpoon.localPosition + Vector3.down * shootRange), shootRange / shootSpeed));
    }

    void AimHarpoon() {
        isMovementLocked = true;
        StartCoroutine(LerpHarpoonRotation());
    }
}