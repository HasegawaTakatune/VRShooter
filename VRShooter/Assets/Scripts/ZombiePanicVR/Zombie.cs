using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private ZOMBIE_STATUS state = ZOMBIE_STATUS.STOP;
    public ZOMBIE_STATUS State
    {
        get { return state; }
        set
        {
            if (state == ZOMBIE_STATUS.DEAD) return;
            state = value;
            switch (state)
            {
                case ZOMBIE_STATUS.MOVE: StartCoroutine(Move()); break;
                case ZOMBIE_STATUS.STOP: Speed = 0; break;
                case ZOMBIE_STATUS.ATTACK: StartCoroutine(Attack()); break;
                case ZOMBIE_STATUS.DEAD: Dead(); break;
            }
        }
    }

    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            animator.SetFloat("Speed", speed);
            forward = transform.forward * speed * Time.deltaTime;
        }
    }

    private int health = 20;

    private Vector3 forward;

    [SerializeField] private Animator animator;

    private Barricade barricade;

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Speed = Random.Range(1.0f, 2.0f);
        State = ZOMBIE_STATUS.MOVE;
    }

    private IEnumerator Move()
    {
        while (state == ZOMBIE_STATUS.MOVE)
        {
            yield return null;
            transform.position += forward;
        }
    }

    private IEnumerator Attack()
    {
        Speed = 0;
        animator.SetBool("Attack", true);
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0); ;

        while (!info.IsTag("Attack"))
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        float time = info.length / 2.0f;
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (state == ZOMBIE_STATUS.ATTACK) break;
            barricade.Damage(1);

            yield return new WaitForSeconds(time);
            if (state == ZOMBIE_STATUS.ATTACK) break;
        }
    }

    private void Dead()
    {
        Speed = 0;
        animator.SetBool("Dead", true);
        StopAllCoroutines();
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Layer.BARRICADE)
        {
            barricade = collision.gameObject.GetComponent<Barricade>();
            State = ZOMBIE_STATUS.ATTACK;
        }
    }

    public void Damage(int dmg)
    {
        if (state == ZOMBIE_STATUS.DEAD) return;
        health -= dmg;
        if (health <= 0)
            State = ZOMBIE_STATUS.DEAD;
    }
}