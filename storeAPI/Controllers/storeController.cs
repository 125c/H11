using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using storeAPI.Models;
using System.Data;

namespace storeAPI.Controllers
{
    
    public class storeController : ApiController
    {
        HerLin0030Entities db=new HerLin0030Entities();
        // GET: api/store
        [Route("api/store")]
        public List<Store> Get()
        {
            var stores = db.Store;
            return stores.ToList();
        }
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/store/5
        public Store Get(string StoreIdentifyChineseName)
        {
            var store = db.Store.Where(m=>m.StoreIdentifyChineseName == StoreIdentifyChineseName).FirstOrDefault();
            return store;
        }
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/store
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/store/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/store/5
        public void Delete(int id)
        {
        }
    }
}
