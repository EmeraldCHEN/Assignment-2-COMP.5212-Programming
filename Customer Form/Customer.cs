using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Form
{
    class Customer
    {
        // 3 auto-implemented properties
        public string FName { get; set; }
        public string LName { get; set; }
        public string Phone { get; set; }

        // Create one more property to display all info of customer when needed
        public string CustomerDetail
        {
            get
            {
                return FName + "\t" + LName + "\t" + Phone;
            }
        }

        // A constructor 
        public Customer(string fn, string ln, string ph)
        {
            FName = fn;
            LName = ln;
            Phone = ph;
        } 
        // GetCustomer method to return all of customers' data
        public string GetCustomer()
        {
            return FName + "\t" + LName + "\t" + Phone;
        }             
    }
}
