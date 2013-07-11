/*
 *Liang
*/
using System;
using System.Data;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.IO;
using System.Reflection;

namespace LiangComUtils.RulesAndConfig
{
	/// <summary>
	/// Summary description for ConfigurationManager.
	/// </summary>
	public abstract class ConfigurationBase
	{
		private object m_SyncObject;

		public ConfigurationBase()
		{
			m_SyncObject = new object();
		}

		#region configuration file manipulation

		/// <summary>
		/// Consts defined in the web.config
		/// </summary>
		/// <param name="configfileConstName">const name</param>
		/// <returns>Abosolute path</returns>
		private static string GetConfigurationFile(bool isFromConfig, string configFilePathName)
		{
            string configFile = configFilePathName;
            if (isFromConfig == true)
            {
                configFile = System.Configuration.ConfigurationManager.AppSettings[configFilePathName];
            }

			// really necessary???
			if (configFile == null)
			{
				configFile = "";
			}

			return Util.GetAbsoluteFilePath(configFile);
		}

		#endregion

		/// <summary>
		/// subclass can override this property to manage its own cache.
		/// </summary>
		protected virtual Cache Cache
		{
			get { return HttpRuntime.Cache; }
		}

		#region cache manipulation

		/// <summary>
		/// if serialization fails, an exception is thrown.
		/// </summary>
		/// <param name="isFromConfig"></param>
		/// <param name="configFile"></param>
		/// <returns></returns>
		private object LoadConfiguration(bool isFromConfig, string configFile, Type type)
		{
			ObjectXMLSerializer serializer = new ObjectXMLSerializer();

			object config;
            string configAbsolutePathFile = GetConfigurationFile(isFromConfig, configFile);
			
            config = serializer.Load(type, configAbsolutePathFile);			

            AddToCache(configAbsolutePathFile, config, configAbsolutePathFile);

			return config;
		}

		/// <summary>
		/// Add configuration to cache
		/// </summary>
		/// <param name="key">section name defined in the web.config</param>
		/// <param name="value">configuration object</param>
		/// <param name="depedencyFile">config file</param>
		private void AddToCache(string key, object value, string depedencyFile)
		{
			Cache.Add(key, value, new CacheDependency(depedencyFile),
				Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

        /// <summary>
        /// get from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		protected object GetFromCache(string key, Type type)
		{
            return GetFromCache(true, key, type);
		}

        /// <summary>
        /// get from cache
        /// </summary>
        /// <param name="isFromConfig"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected object GetFromCache(bool isFromConfig, string key, Type type)
        {
            object res = Cache.Get(key);
            if (res == null)
            {
                lock (m_SyncObject)
                {
                    res = Cache.Get(key);
                    if (res == null)
                    {
                        res = LoadConfiguration(isFromConfig, key, type);
                    }
                }
            }

            if (res == null)
            {
                throw new Exception("Fail to get from configuration cache. key:" + key);
            }

            return res;
        }

		#endregion // End of cache manipulation
	}
}
