using UnityEngine;

namespace OregoFramework.Util
{
    public static class VectorUtils
    {
        public static Vector3 ToShortAngle(this Vector3 eulerAngle)
        {
            return new Vector3(
                eulerAngle.x.ToCircleAngle(),
                eulerAngle.y.ToCircleAngle(),
                eulerAngle.z.ToCircleAngle()
            );
        }

        public static bool IsNaV(this Vector3 vector)
        {
            return vector.x.IsNaN() || vector.y.IsNaN() || vector.z.IsNaN();
        }
    }
}