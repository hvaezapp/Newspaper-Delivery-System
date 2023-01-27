using System;
using System.IO;

namespace NDS.Utility
{

    public static class AppUtility
    {



        public static int ToInt(this object val)
        {
            return Convert.ToInt32(val);
        }
        public static byte ToByte(this object val)
        {
            return Convert.ToByte(val);
        }

        public static long ToLong(this object val)
        {
            return Convert.ToInt64(val);
        }


        public static string GenerateGuidToken(int len)
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0 , len);
        }


        public static string GenerateGuidToken()
        {

            return Guid.NewGuid().ToString().Replace("-", "");
        }


        public static string GenerateActiveCode(int len)
        {
            string code = "";

            Random r = new Random(DateTime.Now.Second);

            for (int i = 1; i <= len; i++)
            {
                code += r.Next(0, 10).ToString();
            }
            return code;
        }



        public static string ToPrice(this object dec)
        {
            string Src = dec.ToString();
            Src = Src.Replace(".0000", "");
            if (!Src.Contains("."))
            {
                Src = Src + ".00";
            }
            string[] price = Src.Split('.');
            if (price[1].Length >= 2)
            {
                price[1] = price[1].Substring(0, 2);
                price[1] = price[1].Replace("00", "");
            }

            string Temp = null;
            int i = 0;
            if ((price[0].Length % 3) >= 1)
            {
                Temp = price[0].Substring(0, (price[0].Length % 3));
                for (i = 0; i <= (price[0].Length / 3) - 1; i++)
                {
                    Temp += "," + price[0].Substring((price[0].Length % 3) + (i * 3), 3);
                }
            }
            else
            {
                for (i = 0; i <= (price[0].Length / 3) - 1; i++)
                {
                    Temp += price[0].Substring((price[0].Length % 3) + (i * 3), 3) + ",";
                }
                Temp = Temp.Substring(0, Temp.Length - 1);
                // Temp = price(0)
                //If price(1).Length > 0 Then
                //    Return price(0) + "." + price(1)
                //End If
            }
            if (price[1].Length > 0)
            {
                return Temp + "." + price[1];
            }
            else
            {
                return Temp;
            }
        }




        public static long GetPriceWithWage(this long price, int wage)
        {

            return price - ((price * wage) / 100);
        }

        
    }




}
