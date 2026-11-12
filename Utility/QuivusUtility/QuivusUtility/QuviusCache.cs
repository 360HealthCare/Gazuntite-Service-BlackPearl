using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusUtility
{
    public static class QvuisCache
    {
        static Dictionary<string, object> _c = new Dictionary<string, object>();

        static int _cacheTimeout = 0;

        public static void AddToCache(string cachename, string key, object value)
        {
            string qKey = GetKey(cachename, key);
            if (_c.ContainsKey(qKey))
            {
                _c.Remove(qKey);
            }
            _c.Add(qKey, new CacheObject(value, DateTime.Now));
        }

        public static object GetFromCache(string cachename, string key)
        {
            object o = null;
            string tiqKey = GetKey(cachename, key);
            if (_c.ContainsKey(tiqKey))
            {
                CacheObject co = (CacheObject)_c[tiqKey];
                // Return the value in the cache
                // if it has not expired
                if (co._insertDtTm.AddMinutes(GetExpireTimeMin(cachename)) > DateTime.Now)
                {
                    o = co._o;
                }
                else
                {
                    _c.Remove(tiqKey);
                    o = null;
                }
            }
            return o;
        }


        private static string GetKey(string cachename, string key)
        {
            return cachename + key;
        }

        /// <summary>
        /// Currently returns a constant.
        /// Eventually different caches can have different times
        /// which might be returned from the web.config
        /// </summary>
        /// <param name="cachename">The name of the current cache</param>
        /// <returns></returns>
        static private int GetExpireTimeMin(string cachename)
        {
            if (_cacheTimeout == 0)
                _cacheTimeout = Convert.ToInt16(200);
            return _cacheTimeout;
        }

        class CacheObject
        {
            public CacheObject(Object o, DateTime dt) { _o = o; _insertDtTm = DateTime.Now; }

            /// <summary>
            /// The object to be cached
            /// </summary>
            public Object _o;

            /// <summary>
            /// The time the object was inserted
            /// </summary>
            public DateTime _insertDtTm;
        }

    }
  
}
