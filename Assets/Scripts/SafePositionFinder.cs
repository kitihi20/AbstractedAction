using UnityEngine;

//Made by Gemmini

public class SafePositionFinder
{
    /// <summary>
    /// AABBの肥大化問題を回避し、正確なコライダー形状に基づいて安全な位置を探す
    /// </summary>
    /// <param name="playerPos">プレイヤーの初期座標</param>
    /// <param name="rightDir">プレイヤーの右方向ベクトル</param>
    /// <param name="playerRadius">プレイヤーの半径（0.5m）</param>
    /// <param name="obstacleMask">障害物として判定するレイヤーマスク</param>
    /// <param name="resultPos">見つかった座標</param>
    /// <returns>条件を満たす地点が見つかれば true</returns>
    public static bool TryFindSafePositionAccurate(Vector3 playerPos, Vector3 rightDir, Vector3 backDir, float playerRadius, LayerMask obstacleMask, out Vector3 resultPos, out float resultDist)
    {
        resultPos = playerPos;
        
        float searchMax = 20f;   // 最大探索距離（左右20m）
        float minDistance = 3f;  // 最小距離（3m以上離れる）
        resultDist = minDistance;
        
        // 探索のステップ幅（0.25m刻み。半径0.5mの球がすり抜けない十分な精度）
        float step = 0.5f;      

        // 3m地点から20m地点まで、内側から外側へ向かって探索
        for (float t = minDistance; t <= searchMax; t += step)
        {
            resultDist = t;
            // --- 右側の判定 ---
            Vector3 rightPos = playerPos + rightDir * t;
            // CheckSphereは「接触していればtrue」を返すため、!で反転（接触していなければ安全）
            if (!Physics.CheckSphere(rightPos, playerRadius, obstacleMask, QueryTriggerInteraction.Collide))
            {
                resultPos = rightPos;
                return true; // 見つかった瞬間に処理を終了（超軽量）
            }

            // --- 左側の判定 ---
            Vector3 leftPos = playerPos + rightDir * (-t);
            if (!Physics.CheckSphere(leftPos, playerRadius, obstacleMask, QueryTriggerInteraction.Collide))
            {
                resultPos = leftPos;
                return true; // 見つかった瞬間に処理を終了
            }

            // --- 後ろ側の判定 ---
            Vector3 backPos = playerPos + backDir * (-t);
            if (!Physics.CheckSphere(backPos, playerRadius, obstacleMask, QueryTriggerInteraction.Collide))
            {
                resultPos = backPos;
                return true; // 見つかった瞬間に処理を終了
            }
        }

        // 左右20mを探しても空きスペースが無かった場合
        return false;
    }
}