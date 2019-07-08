using LGDShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LGDShop.Domain.Services
{
    public class KeyGenerationService : IKeyGenerationService
    {
        /// <summary>
        /// generate unique random key, using RNGCryptoServiceProvider which is better than Random()
        /// </summary>
        /// <param name="size">default is 14</param>
        /// <returns></returns>
        private string GenerateUniqueKey(int size = 14)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }


        //public string GenerateDisplayId<TEntity>() where TEntity : class
        //{
        //    switch (nameof(TEntity).ToString())
        //    {
        //        case nameof(Department):
        //            return "dep_" + GenerateUniqueKey();
        //        case nameof(Employee):
        //            return "emp_" + GenerateUniqueKey();
        //        case nameof(Position):
        //            return "pos_" + GenerateUniqueKey();
        //        default:
        //            return GenerateUniqueKey();
        //    }            
        //}

    }
}
