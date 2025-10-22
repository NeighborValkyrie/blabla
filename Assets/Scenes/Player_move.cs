using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    Vector3 moveV = Vector3.zero;
    Animator myAnim;
    float speed = 7.0f;
    SpriteRenderer mySR;

    Rigidbody2D myRigid;
    bool isDead = false;
    bool isJump = false;
    CapsuleCollider2D myCapsule;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();
        myRigid = GetComponent<Rigidbody2D>();
        myCapsule = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveV = Vector3.right;
                myAnim.SetBool("run", true);
                mySR.flipX = false;

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveV = Vector3.left;
                myAnim.SetBool("run", true);
                mySR.flipX = true;
            }
            else
            {
                moveV = Vector3.zero;
                myAnim.SetBool("run", false);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
            {
                myRigid.AddForce(Vector3.up * 200.0f);
                myAnim.SetBool("jump", true);
                isJump = true;



            }
            transform.Translate(moveV * speed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            myAnim.SetBool("jump", false);
            isJump = false;
        }
        if (collision.gameObject.CompareTag("Dead"))
        {
            myAnim.SetTrigger("dead");
            isDead = true;
            myCapsule.size = new Vector3(0.1f, 0.1f, 0);
            Invoke("setConstraintAll", 2.0f);
        }
    }
    void setConstraintAll()
    {
        myRigid.constraints = RigidbodyConstraints2D.FreezeAll;

    }
}
