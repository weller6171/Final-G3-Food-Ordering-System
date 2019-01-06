using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalG3FoodOrderingSystem.Models
{
    public class SalesOrderDetail
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public string FoodName { get; set; }
        public string Status { get; set; }


    }
}
