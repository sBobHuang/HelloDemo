using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageBoardController : ControllerBase
    {
        private readonly ILogger<MessageBoardController> _logger;

        public MessageBoardController(ILogger<MessageBoardController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<MessageBoard> Get()
        {
            List<MessageBoard> list = new List<MessageBoard>();
            //连接数据库
            MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = "server = bobhuang.xyz; userid = root; password = root; database = messageboards; "
            };
            connection.Open();
            //查找数据库里面的表
            MySqlCommand mscommand = new MySqlCommand("select * from messages", connection);
            using (MySqlDataReader reader = mscommand.ExecuteReader())
            {
                //读取数据
                while (reader.Read())
                {
                    list.Add(new MessageBoard()
                    {
                        Name = reader.GetString("name"),
                        Title = reader.GetString("title"),
                        Message = reader.GetString("message"),
                        LeaveDate = reader.GetDateTime("created_at")
                    });
                }
            }
            return list.ToArray();
        }
    }
}