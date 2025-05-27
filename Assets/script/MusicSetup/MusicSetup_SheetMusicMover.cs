using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class MusicSetup_SheetMusicMover : MonoBehaviour
{
    public MusicSetup_Manager _mc;
    public float OffsetY = 3; // 0 是原點，每3單位是一拍距離
    private Coroutine moveCoroutine; // 儲存 Coroutine 的引用

    private void Start()
    {

    }

    public void GoTotBeat(int BeatIndex)
    {
        transform.position = new Vector3(0, -OffsetY * BeatIndex, 0);
    }

    public void StartMoving(int BeatIndex)
    {
        gameObject.transform.position = new Vector3(0, -OffsetY * BeatIndex, 0);

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveOverTime());
    }

    public void StopMoving()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    private IEnumerator MoveOverTime()
    {
        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 endPosition = new Vector3(0, -3 * _mc.beatNum, 0);

        float startTime = _mc.nowBeat * _mc.beatleng;
        float endTime = _mc.MusicLeng;
        //print($"{_mc.beatNum * _mc.beatleng} / {_mc.MusicLeng}");
        while (startTime < endTime)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, startTime / endTime);
            //print($"Pos  > {gameObject.transform.position.y / endPosition.y} > {gameObject.transform.position.y} : {endPosition.y} ");
            //print($"Time > {startTime / endTime} > {startTime} : {endTime} ");
            startTime += Time.deltaTime;
            yield return null;
        }

        // 確保移動結束時位置精確到達終點
        transform.position = endPosition;

        // 結束後清除引用
        moveCoroutine = null;
        print("MoveFinish");
    }
}
