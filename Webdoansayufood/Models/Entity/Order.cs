using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webdoansayufood.Models.Entity
{
    [Table("tb_Order")]
    public class Order : Common
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]

        public string Email { get; set; }
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]

        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]

        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]

        public string Address { get; set; }

        public string code { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}