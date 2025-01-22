// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("XxSUUlKQz6WAxD4DUQsrK0rw+xGMPr2ejLG6tZY69DpLsb29vbm8vxFoHSdMCL1o5bWeKN9PJwpYnSFnQbT1elCEQ1iT9YCTmyyhYQVU6Ku2jX/Py/ukj1hktmshz2qefLJvjfMV0xplgSgZy9v6t0mwgqvku8QBeHa3YkDHmEfmxBKUOhn5IVF0yPBzKLJb7QRuu7QDO/p1FqW+oyB9y1CslNWK+tTSrW6jo7XUr1Z103JYYi5/kSoOnSd7b7/twa6B2lr5XRSBOolpQoiKbTI2Vws0H/nFP4IA/gPhnxqvRMbDhrmr2OcNAzPOqxe9Pr2zvIw+vba+Pr29vFWtiORU1HRiDRV5scl9tElRXdl9v1hfDA2H3fGI6FpvYYafJb6/vby9");
        private static int[] order = new int[] { 8,8,7,9,6,11,6,11,13,9,10,11,13,13,14 };
        private static int key = 188;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
