using System.Collections.Generic;
using UnityEngine;

//Gemini製

public class SafePositionFinder
{
    // メモリ割り当てを避けるためのキャッシュ（最大64個のオブジェクトの接触を想定）
    private static RaycastHit[] hitsBuffer = new RaycastHit[64];
    private static List<Vector2> blockedIntervals = new List<Vector2>(64);
    private static List<Vector2> mergedIntervals = new List<Vector2>(64);
    private static List<float> candidates = new List<float>(128);

    /// <summary>
    /// プレイヤーの左右の空間から、接触物がなく3m以上離れた最も近い座標を探す
    /// </summary>
    /// <param name="playerPos">プレイヤーの座標</param>
    /// <param name="rightDir">プレイヤーの右方向ベクトル（正規化済みであること）</param>
    /// <param name="playerRadius">プレイヤーの半径（0.5m）</param>
    /// <param name="obstacleMask">障害物として判定するレイヤーマスク</param>
    /// <param name="resultPos">見つかった座標（見つからなかった場合は初期座標）</param>
    /// <returns>条件を満たす地点が見つかれば true</returns>
    public static bool TryFindSafePosition(Vector3 playerPos, Vector3 rightDir, float playerRadius, LayerMask obstacleMask, out Vector3 resultPos)
    {
        resultPos = playerPos;
        
        float searchLeft = -40f;
        float searchRight = 40f; // 左20m地点から右へ40mなので、プレイヤー基準で[-20, 20]
        float minDistance = 5f;  // 3m以上離れる
        float totalCastDistance = searchRight - searchLeft; // 40f

        // キャッシュのクリア
        blockedIntervals.Clear();
        mergedIntervals.Clear();
        candidates.Clear();

        // 1. 左20m地点から右に向かって SphereCastNonAlloc を実行
        Vector3 castStartPos = playerPos + rightDir * searchLeft;
        int hitCount = Physics.SphereCastNonAlloc(castStartPos, playerRadius, rightDir, hitsBuffer, totalCastDistance, obstacleMask, QueryTriggerInteraction.Collide);

        // 2. 接触したコライダーの Bounds を1Dの線分区間に射影する
        for (int i = 0; i < hitCount; i++)
        {
            Bounds bounds = hitsBuffer[i].collider.bounds;

            // Boundsの「中心」と「広がり(Extents)」を rightDir 軸上に射影
            float centerProj = Vector3.Dot(bounds.center - playerPos, rightDir);
            float extentsProj = bounds.extents.x * Mathf.Abs(rightDir.x) + 
                                bounds.extents.y * Mathf.Abs(rightDir.y) + 
                                bounds.extents.z * Mathf.Abs(rightDir.z);

            // プレイヤーの半径と安全のための微小なバッファを加味した「進入不可区間」を記録
            float buffer = playerRadius + 0.01f; 
            blockedIntervals.Add(new Vector2(centerProj - extentsProj - buffer, centerProj + extentsProj + buffer));
        }

        // 3. 進入不可区間（Interval）をマージする
        if (blockedIntervals.Count > 0)
        {
            blockedIntervals.Sort((a, b) => a.x.CompareTo(b.x));
            Vector2 current = blockedIntervals[0];

            for (int i = 1; i < blockedIntervals.Count; i++)
            {
                if (blockedIntervals[i].x <= current.y)
                {
                    current.y = Mathf.Max(current.y, blockedIntervals[i].y);
                }
                else
                {
                    mergedIntervals.Add(current);
                    current = blockedIntervals[i];
                }
            }
            mergedIntervals.Add(current);
        }

        // 4. 配置可能な候補地点のリストアップ
        // 初期候補: プレイヤーから正確に左右3mの地点
        candidates.Add(minDistance);
        candidates.Add(-minDistance);

        // その他の候補: 障害物の境界線のすぐ外側
        foreach (var interval in mergedIntervals)
        {
            if (interval.y >= minDistance && interval.y <= searchRight) candidates.Add(interval.y);
            if (interval.x <= -minDistance && interval.x >= searchLeft) candidates.Add(interval.x);
        }

        // 5. 候補地点の中から、「どの進入不可区間にも属していない」かつ「最も近い」地点を選ぶ
        float bestT = float.MaxValue;
        bool found = false;

        foreach (float t in candidates)
        {
            // 条件: 探査範囲内であり、3m以上離れていること
            if (t >= searchLeft && t <= searchRight && Mathf.Abs(t) >= minDistance)
            {
                bool isBlocked = false;
                foreach (var interval in mergedIntervals)
                {
                    // 進入不可区間の「内側」にある場合はNG
                    if (t > interval.x && t < interval.y)
                    {
                        isBlocked = true;
                        break;
                    }
                }

                // ブロックされておらず、かつ今までの候補より近ければ更新
                if (!isBlocked && Mathf.Abs(t) < Mathf.Abs(bestT))
                {
                    bestT = t;
                    found = true;
                }
            }
        }

        // 6. 結果の返却
        if (found)
        {
            resultPos = playerPos + rightDir * bestT;
            return true;
        }

        return false;
    }
}