using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL
{
    public class Encrypt_Decrypt
    {
        //Start:23/2/2022, Added By : Dhrupti Thesiya , Description : for password security.
        public string Encryptpassword(string _password)
        {
            try
            {
                string _encrypted = null;

                byte[] encode = new byte[_password.Length];
                encode = Encoding.UTF8.GetBytes(_password);
                _encrypted = Convert.ToBase64String(encode);

                return _encrypted;
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        public string Decryptpassword(string _encryptpassword)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder decode = encoder.GetDecoder();
                byte[] decodedata = Convert.FromBase64String(_encryptpassword);
                int charcount = decode.GetCharCount(decodedata, 0, decodedata.Length);
                char[] decodechar = new char[charcount];
                decode.GetChars(decodedata, 0, decodedata.Length, decodechar, 0);

                string decrypted = new string(decodechar);

                return decrypted;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
