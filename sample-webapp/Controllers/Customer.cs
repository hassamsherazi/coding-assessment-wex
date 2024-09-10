using DTO;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sample_webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer : ControllerBase
    {

        private readonly ILoggingService loggingService; 

        public Customer(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        //api/Customer/Loyal

        // GET: api/v2/<Customer>
        [HttpGet]
        public IActionResult Get()
        {
            List<DayLog> day1 = loggingService.GetLogData("day_1.json");
            List<DayLog> day2 = loggingService.GetLogData("day_2.json");



            /*
             criteria
            1. they came in both days (customerId)
            2. visited atleast 2 unique pages (pageId)
             */

            List<DayLog> visited2Days = new List<DayLog>();
            //getting the list of all customers who visited both days. 
            foreach (var item in day1)
            {
                var cusObj  = day2.Where(x => x.CustomerId == item.CustomerId).ToList();
                if(cusObj!=null && cusObj.Count > 0)
                {
                    visited2Days.Add(item);
                    visited2Days.AddRange(cusObj); 
                }
            }

            var customers = visited2Days.GroupBy(x => x.CustomerId);
            var finalList = new Dictionary<int,List<DayLog>>();
            foreach (var item in customers)
            {
                

                    int page = item.Select(x=>x.PageId).FirstOrDefault();
                    int key = item.Key;
                    foreach (var cus in item)
                    {
                        if(page != cus.PageId){
                            finalList.Add(key, item.ToList());
                            break;
                        }
                    }
                
            }


            return Ok(finalList.ToList());
        }





























        //// GET api/<Customer>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<Customer>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<Customer>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Customer>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
