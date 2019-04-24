using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoreLib.Core.DB.Query
{
    public class LoadBalancerQuery
    {
        public CoreLib.Core.DB.Models.LoadBalancerModelDB GetBalancerInfo()
        {
            try
            {
                var _client = new MongoClient(Properties.Settings.Default.ip);
                var _database = _client.GetDatabase(Properties.Settings.Default.database);
               var  _loadBalancerCollection = _database.GetCollection<CoreLib.Core.DB.Models.LoadBalancerModelDB>(Properties.Settings.Default.collection); 

                var filter = Builders<CoreLib.Core.DB.Models.LoadBalancerModelDB>.Filter.Eq("_id", ObjectId.Parse(Properties.Settings.Default.id));
                var data = _loadBalancerCollection.Find(filter).FirstOrDefault();
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
