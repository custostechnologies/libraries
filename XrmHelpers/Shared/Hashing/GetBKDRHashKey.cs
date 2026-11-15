namespace Custos.XrmHelpers.Shared.Hashing
{
    public class GetBKDRHashKey
    {
        /* helper to calculate new hash using a standard BKDR hashing algorithm */
        /* mirrors hashing implementation in Power Query                        */
        /* takes a concatenated string (with '|' separator), converts each      */
        /* char to its corresponding integer, adds padding and then calculates  */
        /* the hash                                                             */
        public static int Calculate(string plaintext)
        {

            long seed = 1313;
            long modulo = 2147483647;
            long state = 0;

            try
            {
                for (int i = 0; i < plaintext.Length; i++)
                {
                    // convert character to 3-digit integer
                    int charInt = plaintext[i];

                    // convert integer into a string with padding
                    string paddedStr = charInt.ToString("D3");

                    // parse to long
                    long current = long.Parse(paddedStr);

                    // caculate state
                    state = (state + (current * seed)) % modulo;
                }

                return (int)state;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in ComputeBKDRHashKey: {0}", ex);
                throw;
            }
        }
    }
}