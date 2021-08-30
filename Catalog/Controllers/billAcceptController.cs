using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Catalog.Controllers
{
    //GET

    [ApiController]
    [Route("[controller]")]
    public class billAcceptController : ControllerBase
    {
        private readonly dataBillRepository repository;
        {
            public moneyController 
            {
                repository = new billAcceptController();
            }

            public acceptController
            {
                repository = new billAcceptController();
            }

            public rejectController
            {
                repository = new billAcceptorControllr();
            }

            [HttpGet]
            public IEnumerable<Money> GetMoney()
            {
                var moneys = repository.GetMoney();
                return moneys;
            }

            [HttpGet ("{id}")]

            public Money GetMoney(int moneyId)
            {
                var money = repository.GetMoney(id);
                return money;
            }
        }
        
    }
}