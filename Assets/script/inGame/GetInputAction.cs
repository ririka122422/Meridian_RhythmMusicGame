using UnityEngine;
using System.Collections.Generic;

public class gDefine
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

public class FingerInput
{
    public Vector2 StartPosistion;
    public Vector2 NowPosistion;
    public Vector2 EndPosistion;
}

public class GetInputAction : MonoBehaviour
{
    public inGame_GameManager _gameManager;

    private Dictionary<int, Vector2> fingerStartPositions = new Dictionary<int, Vector2>();
    private Vector2 mouseStartPosition;
    private const float SWIPE_THRESHOLD = 50f;

    private void Start()
    {
        fingerStartPositions.Clear();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        MouseInput();
#elif UNITY_ANDROID
        HandleMultiTouch();
#endif
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;
            inGame_Track track = GetTrackByTouchPosistion(mouseStartPosition);
            if (track != null)
            {
                track.OnTapTrack();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseStartPosition = Input.mousePosition;
            inGame_Track track = GetTrackByTouchPosistion(mouseStartPosition);
            if (track != null)
            {
                track.OnFlickTrack("Flick");
            }
        }
    }

    void HandleMultiTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            int fingerId = touch.fingerId;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 BeganPosistion = touch.position;
                    if (!fingerStartPositions.ContainsKey(fingerId))
                    {
                        fingerStartPositions.Add(fingerId, BeganPosistion);
                    }
                    inGame_Track track = GetTrackByTouchPosistion(BeganPosistion);
                    if (track != null)
                    {
                        track.OnTapTrack();
                    }
                    break;

                case TouchPhase.Moved:
                    if (fingerStartPositions.ContainsKey(fingerId))
                    {
                        Vector2 startPos = fingerStartPositions[fingerId];
                        Vector2 currentPos = touch.position;
                        if (Vector2.Distance(startPos, currentPos) > SWIPE_THRESHOLD)
                        {
                            gDefine.Direction direction = DetermineDirection(startPos, currentPos);
                            inGame_Track Track = GetTrackByTouchPosistion(startPos);
                            if (Track != null)
                            {
                                Track.OnFlickTrack("Flick");
                            }
                            fingerStartPositions.Remove(fingerId);
                        }
                    }
                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:
                    if (fingerStartPositions.ContainsKey(fingerId))
                    {
                        Vector2 startPos = fingerStartPositions[fingerId];
                        Vector2 endPos = touch.position;
                        gDefine.Direction direction = DetermineDirection(startPos, endPos);
                        inGame_Track Track = GetTrackByTouchPosistion(startPos);
                        if (Track != null)
                        {
                            Track.OnFlickTrack("Flick");
                        }
                        fingerStartPositions.Remove(fingerId);
                    }
                    break;

                case TouchPhase.Canceled:
                    break;
            }
        }
    }

    gDefine.Direction DetermineDirection(Vector2 startPos, Vector2 endPos)
    {
        if (Mathf.Abs(startPos.x - endPos.x) > Mathf.Abs(startPos.y - endPos.y))
        {
            return startPos.x > endPos.x ? gDefine.Direction.Left : gDefine.Direction.Right;
        }
        else
        {
            return startPos.y > endPos.y ? gDefine.Direction.Down : gDefine.Direction.Up;
        }
    }

    inGame_Track GetTrackByTouchPosistion(Vector2 TouchPosistion)
    {
        Ray ray = Camera.main.ScreenPointToRay(TouchPosistion);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Track") && _gameManager.isDropping && !_gameManager.isFinish && !_gameManager.isPause)
            {
                inGame_Track track = hit.collider.GetComponent<inGame_Track>();
                if (track != null)
                {
                    return track;
                }
            }
        }
        return null;
    }
}
