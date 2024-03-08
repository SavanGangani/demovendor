﻿//using ServiceStack.Redis;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyVendors.BAL.CacheLayer
//{
//    public class NotificationKeyValue
//    {
//        //private readonly RedisEndpoint _redisEndpoint;
//        //public NotificationKeyValue()
//        //{
//        //    var host = "localhost"; //ConfigurationManager.AppSettings["RedisHost"].ToString();
//        //    var port = 6379;       //Convert.ToInt32(ConfigurationManager.AppSettings["RedisPort"]);
//        //    _redisEndpoint = new RedisEndpoint(host, port);
//        //}

//        public bool IsKeyExists(string key)
//        {
//            using (var redisClient = new RedisClient(_redisEndpoint))
//            {
//                if (redisClient.ContainsKey(key))
//                {
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }

//        public void SetStrings(string key, string value)
//        {
//            using (var redisClient = new RedisClient(_redisEndpoint))
//            {
//                redisClient.SetValue(key, value);
//            }
//        }

//        public string GetStrings(string key)
//        {
//            using (var redisClient = new RedisClient(_redisEndpoint))
//            {
//                return redisClient.GetValue(key);
//            }
//        }

//        public bool StoreList<T>(string key, T value, TimeSpan timeout)
//        {
//            try
//            {
//                using (var redisClient = new RedisClient(_redisEndpoint))
//                {
//                    redisClient.As<T>().SetValue(key, value, timeout);
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public T GetList<T>(string key)
//        {
//            T result;

//            using (var client = new RedisClient(_redisEndpoint))
//            {
//                var wrapper = client.As<T>();

//                result = wrapper.GetValue(key);
//            }

//            return result;
//        }

//        public long Increment(string key)
//        {
//            using (var client = new RedisClient(_redisEndpoint))
//            {
//                return client.Increment(key, 1);
//            }
//        }

//        public long Decrement(string key)
//        {
//            using (var client = new RedisClient(_redisEndpoint))
//            {
//                return client.Decrement(key, 1);
//            }
//        }
//    }
//}
