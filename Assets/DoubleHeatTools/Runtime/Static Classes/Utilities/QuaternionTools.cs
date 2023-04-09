using UnityEngine;

namespace DoubleHeat.Utilities {

    public static class QuaternionTools {

        public static Quaternion FromToRotationByQuaternion (Quaternion from, Quaternion to) {
            return to * Quaternion.Inverse(from);
        }
    }
}