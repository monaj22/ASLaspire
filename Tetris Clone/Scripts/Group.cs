//Source: https://noobtuts.com/unity/2d-tetris-game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public static float fallPeriod = 1.00f;
    float lastFall = 0;

    public static HashSet<KeyCode> refireKeys = new HashSet<KeyCode> {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D
    };
    private Dictionary<KeyCode, float> buttonDownTimes = new Dictionary<KeyCode, float>();
    private Dictionary<KeyCode, float> lastFireTimes = new Dictionary<KeyCode, float>();
    public static float refireWaitTime = 0.5f;
    public static float refireTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (!isValidGridPos()) {
            Playfield.isFinished = true;
            Destroy(gameObject);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFire(KeyCode.A)) {
            tryMove(new Vector3(-1, 0, 0));
        } else if (shouldFire(KeyCode.D)) {
            tryMove(new Vector3(1, 0, 0));
        } else if (shouldFire(KeyCode.Q)) {
            tryRotate(new Vector3(0, 0, -90));
        } else if (shouldFire(KeyCode.E)) {
            tryRotate(new Vector3(0, 0, 90));
        }  else if (shouldFire(KeyCode.S) || Time.time - lastFall >= fallPeriod) {
            if (!tryMove(new Vector3(0, -1, 0))) {
                Playfield.deleteFullRows();
                FindObjectOfType<Spawner>().spawnNext();
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    bool shouldFire(KeyCode key) {
        if (Input.GetKeyDown(key)) {
            if (refireKeys.Contains(key)) {
                buttonDownTimes[key] = Time.time;
                lastFireTimes[key] = Time.time;
            }
            return true;
        } else if (Input.GetKey(key)) {
            if (refireKeys.Contains(key) && Time.time - buttonDownTimes[key] >= refireWaitTime
                && Time.time - lastFireTimes[key] >= refireTime) {
                lastFireTimes[key] = Time.time;
                return true;
            } else {
                return false;
            }
        } else if (Input.GetKeyUp(key)) {
            buttonDownTimes.Remove(key);
            lastFireTimes.Remove(key);
            return false;
        }

        return false;
    }

    bool isValidGridPos() {
        foreach (Transform child in transform) {
            Vector2 v = Playfield.roundVec2(child.position);
            if (!Playfield.isInsideBorder(v)) {return false;}

            Transform atPos = Playfield.grid[(int) v.x, (int) v.y];
            if (atPos != null 
                    && atPos.parent != transform) {
                return false;
            }
        }
        return true;
    }

    //Can we refacator this? This O(n^2) solution looks messy to me
    void updateGrid() {
        for (int y = 0; y < Playfield.h; ++y) {
            for (int x = 0; x < Playfield.w; ++x) {
                Transform atPos = Playfield.grid[x,y];
                if (atPos != null && atPos.parent == transform) {
                    Playfield.grid[x,y] = null;
                }
            }
        }

        foreach (Transform child in transform) {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int) v.x, (int) v.y] = child;
        }
    }

    bool tryMove(Vector3 direction) {
        transform.position += direction;

        if (isValidGridPos()) {
            updateGrid();
            return true;
        }

        transform.position -= direction;
        return false;
    }

    bool tryRotate(Vector3 rotation) {
        transform.Rotate(rotation);

        if (isValidGridPos()) {
            updateGrid();
            return true;
        }

        transform.Rotate(-1 * rotation);
        return false;
    }
}
