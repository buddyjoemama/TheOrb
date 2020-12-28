using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingEnemy : Enemy
{
    public List<Transform> boundaries;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Move()
    {
        int index = 0;

        Vector3 velocity = Vector3.zero;
        while (true)
        {
            while (!this.Approached(transform.position, boundaries[index].position))
            {
                transform.position = Vector3.SmoothDamp(transform.position, boundaries[index].position, ref velocity, .75f);
                yield return new WaitForFixedUpdate();
            }

            index += 1;

            if (index >= boundaries.Count)
                index = 0;

            yield return new WaitForFixedUpdate();
        }
    }

    private bool Approached(Vector3 position1, Vector3 position2)
    {
        int xDiff = Mathf.Abs((int)position1.x - (int)position2.x);
        int yDiff = Mathf.Abs((int)position1.y - (int)position2.y);
        int zDiff = Mathf.Abs((int)position1.z - (int)position2.z);

        return xDiff <= 2 && yDiff <= 2 && zDiff <= 2;
    }
}
