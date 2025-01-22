// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("2Q3mQe9oxagEzHvvfPg927zKy0Msnh0+LBEaFTaaVJrrER0dHRkcH54dExwsnh0WHp4dHRyaUrwxy/QHXkpCXUSkU/OI5XF16lc/QXcZbRjezqTMsG/FwbREs1YWup5PS06jc8gVUpcN2OjJkJazcJC5BLKY6g7/FvsN4DJsDmyuPo1n4VRS49u+Jxywz1Ln7k4DEGF0pJzXcaXO3h3hGjpNeCREVpMupYWabEMz7XDSNp3+KfT5PYAZEDEpUjWUhlnNnL3FmpQJvPtVK1uicmRfBUJdycx7wz66vaQv+P0fKcNxyZarWtQgy/IxC83PgQWaccLqq49ktKwRlUJtxIo6NqTALeSKM6EnSdwgBaIxGeUWiFNw+odVkA+YcXoy3x4fHRwd");
        private static int[] order = new int[] { 12,12,12,9,9,12,8,9,12,12,13,13,12,13,14 };
        private static int key = 28;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
