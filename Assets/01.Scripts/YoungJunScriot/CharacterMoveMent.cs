//작성자 : 박영준
//설명 : 캐릭터 움직임 및 회전

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMoveMent : MonoBehaviour
{
    Coroutine moveCo = null;
    Coroutine rotCo = null;

    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if (moveCo != null)
        {
            StopCoroutine(moveCo);
            moveCo = null;
        }

        moveCo = StartCoroutine(MovingToPosition(pos, done));

        if (Rot)
        {
            if (rotCo != null)
            {
                StopCoroutine(rotCo);
                rotCo = null;
            }

            rotCo = StartCoroutine(RotatingToPosition(pos));
        }
    }

    IEnumerator MovingToPosition(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position; // 방향벡터 - 나로부터 대상까지
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = this.transform.GetComponent<Pickup>().enemy.moveSpeed * Time.deltaTime;
            if (delta > dist)
            {
                delta = dist;
            }

            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }

        done?.Invoke(); // 다움직이고나면 람다식으로 IDLE 실행하도록.
    }

    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized; // 본인 -> 목적지 로 향하는 방향벡터
        float Angle = Vector3.Angle(transform.forward, dir); // 두벡터사이의 각도구하기.
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }

        while (Angle > 0.0f)
        {
            float delta = 180.0f * Time.deltaTime;
            if (delta > Angle)
            {
                delta = Angle;
            }

            Angle -= delta;
            transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            yield return null;
        }
    }
}