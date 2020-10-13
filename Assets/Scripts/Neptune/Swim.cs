using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public GameObject Player;
    public GameObject Direction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.StartsWith(Names.HAND))
        {
            Debug.Log("Hand touches ball");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Stay(collision);
    }

    private void Stay(Collision collision)
    {
        if (collision.collider.tag == Names.HANDLEFT)
        {
            Debug.Log("Hand left touching ball, camera with angle : " + Direction.transform.eulerAngles.x);
            if (Helper.IsSwimmingLeft(true))
            {
                //Player.transform.position -= Direction.transform.forward * Time.deltaTime;
                //Player.transform.position -= Direction.transform.TransformDirection(Direction.transform.position);
                Move();
            }
        }

        if (collision.collider.tag == Names.HANDRIGHT)
        {
            Debug.Log("Hand right touching ball, camera with angle : " + Direction.transform.eulerAngles.x);
            if (Helper.IsSwimmingRight(true))
            {
                //Player.transform.position -= Player.transform.forward * Time.deltaTime;
                //Player.transform.rotation = Quaternion.Euler(0, Direction.transform.eulerAngles.y, 0);
                //Player.transform.rotation = Quaternion.Slerp(Direction.transform.rotation,
                //                                     Quaternion.LookRotation(Direction.transform.position),
                //                                     1 * Time.deltaTime);
                //Player.transform.position -= Direction.transform.forward * Time.deltaTime;  
                //Player.transform.position -= Direction.transform.TransformDirection(Direction.transform.position);
                Move();
            }
        }
    }

    private void Move()
    {

        //Player.transform.position = Vector3.MoveTowards(Player.transform.position,
        //    this.transform.position, Time.deltaTime);

        //Player.transform.rotation = Quaternion.Euler(0, Direction.transform.eulerAngles.x, 0);

        //var rot = Quaternion.Euler(0, Direction.transform.eulerAngles.x, 0);
        //Debug.Log("angle = " + rot);
        //Player.transform.position -= rot * Vector3.forward * Time.deltaTime;

        //Vector3 targetDir = Player.transform.position - Direction.transform.position;
        //var angleBetween = Vector3.Angle(Player.transform.forward, targetDir);
        //Debug.Log(angleBetween);
        //Player.transform.position -= GetPointOnUnitSphereCap(Direction.transform.forward, angleBetween) * Time.deltaTime;

        Player.transform.position = Vector3.MoveTowards(Player.transform.position, this.transform.position,
            Time.deltaTime);

        //Player.transform.position -= Direction.transform.forward * Time.deltaTime;
        //Player.transform.Translate(Vector3.forward * -Time.deltaTime);
        //Vector3 dir = new Vector3(Direction.transform.position.x, 1, 
        //    Direction.transform.position.z);
        //Player.GetComponent<Rigidbody>().AddForce(dir);

        //float xPos = Input.GetAxis("Horizontal") * Time.deltaTime;
        //float yPos = Input.GetAxis("Vertical") * Time.deltaTime;
        //Vector3 newPos = new Vector3(xPos, 0, yPos);
        //Player.transform.position = Player.transform.position + newPos;
        //var _direction = new Vector3(xPos, 0, yPos);
        //_direction = _direction.normalized;
        //if (_direction.sqrMagnitude == 0.01f)
        //    Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, 
        //        Quaternion.LookRotation(_direction), Time.deltaTime * 10);
        //Player.GetComponent<Rigidbody>().MovePosition(Player.transform.position);
    }

    private void Exit(Collision collision)
    {
        if (collision.collider.tag == Names.HANDLEFT)
        {
            Debug.Log("Hand left touching ball");
            Helper.IsSwimmingLeft(false);
        }

        if (collision.collider.tag == Names.HANDRIGHT)
        {
            Debug.Log("Hand right touching ball");
            Helper.IsSwimmingRight(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Exit(collision);
    }


    public static Vector3 GetPointOnUnitSphereCap(Quaternion targetDirection, float angle)
    {
        var angleInRad = Random.Range(0.0f, angle) * Mathf.Deg2Rad;
        var PointOnCircle = (Random.insideUnitCircle.normalized) * Mathf.Sin(angleInRad);
        var V = new Vector3(PointOnCircle.x, PointOnCircle.y, Mathf.Cos(angleInRad));
        return targetDirection * V;
    }
    public static Vector3 GetPointOnUnitSphereCap(Vector3 targetDirection, float angle)
    {
        return GetPointOnUnitSphereCap(Quaternion.LookRotation(targetDirection), angle);
    }
}
